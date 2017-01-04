using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour {
	[SerializeField]private StatusType status =  StatusType.ReStart;
	[SerializeField]private MainView mainView;
	[SerializeField]private PokerCardSever sever;
	[SerializeField]private int totalBet;
	[SerializeField]private PokerCard[] pokerCards;
	private ZoneData zoneData;
	[SerializeField]private UserData userData;
	[SerializeField]private bool thisGameLockBet = false;

	private float SendCardTime = 0.15f;

	void Start () {
		#if !Clog
		Debug.logger.logEnabled = false;
		#endif

		zoneData = new ZoneData (1);
		ResetGame ();
	}
	
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			OnOpenScoreBtn ();
		}

		if(Input.GetKeyUp(KeyCode.S)){
			OnDownScoreBtn();
		}

		if (Input.GetKeyUp (KeyCode.D)) {
			OnBetBtn ();
		}

		if(Input.GetKeyUp (KeyCode.F)) {
			OnOpenAllCardsBtn ();
		}

		if(Input.GetKeyUp (KeyCode.Z)) {
			OnGetMoneyBtn ();
		}

		if(Input.GetKeyUp (KeyCode.B)) {
			ResetGame ();
		}

		#if UNITY_EDITOR
		if(Input.GetKeyUp(KeyCode.P)){
			print("User BankMoney : " + userData.bankMoney);
		}
		#endif
	}

	public void OnOpenScoreBtn(){
		if (status == StatusType.Working) return;

		print ("----\t開分\t----");

		userData.bankMoney += zoneData.openScoreOfOne;
		mainView.UpdateBankMoney (userData.bankMoney);
	}

	public void OnDownScoreBtn(){
		if (status == StatusType.Working) return;

		if((userData.bankMoney >= 0 && userData.nowMoney <= zoneData.oneBetMoney) == false) {
			print ("下分指令無效");
			return;
		}

		print ("----\t下分\t----");

		if (userData.bankMoney - zoneData.downScoreOfOne < 0) {
			userData.nowMoney += (int)(userData.bankMoney + userData.bankMoney * (userData.bankMoney/(float)zoneData.downScoreOfOne));
			userData.bankMoney -= userData.bankMoney;
		} 
		else {
			userData.nowMoney += zoneData.downScoreOfOne + zoneData.downScoreBouns;
			userData.bankMoney -= zoneData.downScoreOfOne;
		}

		mainView.UpdateBankMoney (userData.bankMoney);
		mainView.UpdateNowMoney (userData.nowMoney);
	}

	public void OnUpScoreBtn(){
		if (status == StatusType.Working) return;

		print ("----\t上分\t----");
	}

	public void OnBetBtn(){
		if (status == StatusType.Working || userData.nowMoney <= 0 || ((int)status <= 3) == false || thisGameLockBet) {
			print ("指令無效");
			return;
		}

		StartCoroutine (IE_Bet ());
	}

	IEnumerator IE_Bet(){
		print ("----\t押注\t----");

		StatusType _preStatus = status;
		status = StatusType.Working;

		int _betMoney = GetBetMoney ();
		userData.nowMoney -= _betMoney;
		userData.roundsBets [(int)_preStatus] += _betMoney;

		mainView.UpdateRoundBets (userData.roundsBets);
		mainView.UpdateNowMoney (userData.nowMoney);
		mainView.UpdateBounsText (zoneData.magnification ,userData.totalBet);

		yield return StartCoroutine(OpenOncePokerCard (_preStatus));

		status = (StatusType)((int)++_preStatus);

		if (status == StatusType.FourRound) {
			yield return StartCoroutine(EnddingGame ());
		}
	}

	private int GetBetMoney(){
		int _betMoney = 0;
		if (userData.nowMoney >= zoneData.oneBetMoney) {
			_betMoney = zoneData.oneBetMoney;
		} else {
			_betMoney = userData.nowMoney;
			thisGameLockBet = true;
		}

		return _betMoney;
	}

	IEnumerator EnddingGame(){
		SuitType _suitType = sever.GetSuitType ();

		//同花大順是0 高牌是10
		if ((int)_suitType < (int)SuitType.Pair) {
			status = StatusType.Win;
		} 
		else {
			status = StatusType.Lose;
			yield return new WaitForSeconds (1.0f);
			#if !Test
			ResetGame ();
			#endif
		}
	}

	public void OnOpenAllCardsBtn(){
		if (status == StatusType.Working || isBet () == false) {
			print ("指令無效");
			return;
		}

		print ("----\t全開\t----");
		StartCoroutine (IE_OpenAllCards ());
	}

	IEnumerator IE_OpenAllCards(){
		StatusType _preStatus = status;
		status = StatusType.Working;

		int _OpenCardsCount = 4 -(int)_preStatus;

		for (int i = 0; i < _OpenCardsCount; i++) {
			yield return StartCoroutine(OpenOncePokerCard (_preStatus));

			_preStatus = (StatusType)((int)++_preStatus);
		}

		yield return StartCoroutine(EnddingGame ());
	}

	private bool isBet(){
		return userData.totalBet == 0 ? false : true;
	}

	public void OnGetMoneyBtn(){
		if (status == StatusType.Working) return;

		print ("----\t取得金錢\t----");
	}

	private void ResetGame(){
		print ("----\t重製遊戲\t----");

		sever.ResetPokerDeck ();
		ResetAllPokerCards ();
		ResetUserDataRoundBet ();
		mainView.UpdateRoundBets (userData.roundsBets);
		mainView.ReSetBounsText ();
		thisGameLockBet = false;
		status = StatusType.ReStart;
	}

	private void ResetAllPokerCards(){
		print ("重製全部撲克卡");

		for (int i = 0; i < pokerCards.Length; i++) {
			pokerCards [i].Reset ();
		}
	}

	private void ResetUserDataRoundBet(){
		print ("重製玩家每輪押注金額");

		for (int i = 0; i < userData.roundsBets.Length; i++) {
			userData.roundsBets [i] = 0;
		}
	}

	IEnumerator OpenOncePokerCard(StatusType p_nowStatus){
		print ("開啟一次撲克牌");

		switch ((int)p_nowStatus + 1) {
		case (int) StatusType.OneRound:
			yield return StartCoroutine (OpenRoundOneCards ());
			break;
		case (int) StatusType.TwoRound:
			yield return StartCoroutine (OpenRoundTwoCards ());
			break;
		case (int) StatusType.ThreeRound:
			yield return StartCoroutine (OpenRoundThreeCards ());
			break;
		case (int) StatusType.FourRound:
			yield return StartCoroutine (OpenRoundFourCards ());
			break;
		}
	}

	IEnumerator OpenRoundOneCards(){
		//播放音效

		string[] _cardsValue = sever.GetRoundOneCards();
		//翻第一張牌
		pokerCards [0].SetData (_cardsValue [0]);
		pokerCards [0].Turn ();
		yield return new WaitForSeconds (SendCardTime);

		//出第二張牌
		pokerCards [1].SetData (_cardsValue [1]);
		yield return new WaitForSeconds (SendCardTime);

		//翻第三張牌
		pokerCards [2].SetData (_cardsValue [2]);
		pokerCards [2].Turn ();
		yield return new WaitForSeconds (SendCardTime);
	}

	IEnumerator OpenRoundTwoCards(){
		string[] _cardsValue = sever.GetRoundTwoCards();
		//出第四張牌
		pokerCards [3].SetData (_cardsValue [0]);
		yield return new WaitForSeconds (SendCardTime);
		//翻第五張牌
		pokerCards [4].SetData (_cardsValue [1]);
		pokerCards [4].Turn ();
		yield return new WaitForSeconds (SendCardTime);
	}

	IEnumerator OpenRoundThreeCards(){
		string[] _cardsValue = sever.GetRoundThreeCards();
		//翻第六張牌
		pokerCards [5].SetData (_cardsValue [0]);
		pokerCards [5].Turn ();
		yield return new WaitForSeconds (SendCardTime);
	}

	IEnumerator OpenRoundFourCards(){
		string[] _cardsValue = sever.GetRoundFourCards();
		//翻第七張牌
		pokerCards [6].SetData (_cardsValue [0]);
		pokerCards [6].Turn ();
		yield return new WaitForSeconds (SendCardTime);

		//翻第二張牌
		pokerCards [1].Turn ();
		yield return new WaitForSeconds (SendCardTime);

		//翻第四張牌
		pokerCards [3].Turn ();
		yield return new WaitForSeconds (SendCardTime);
	}
}
