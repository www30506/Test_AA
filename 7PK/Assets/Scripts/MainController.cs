using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class MainController : MonoBehaviour {
	[SerializeField]private StatusType status =  StatusType.ReStart;
	[SerializeField]private MainView mainView;
	[SerializeField]private PokerCardSever sever;
	[SerializeField]private PokerCard[] pokerCards;
	private ZoneData zoneData;
	[SerializeField]private UserData userData;
	[SerializeField]private bool thisGameLockBet = false;
	[SerializeField]private SuitType endSuitType;

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

		if (Input.GetKeyUp (KeyCode.X)) {
			OnUpScoreBtn ();
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
		if (status == StatusType.Working || CanUpScore () == false) {
			print("上分指令無效");
			return;
		}

		print ("----\t上分\t----");
		StartCoroutine (IE_UpScore ());
	}

	IEnumerator IE_UpScore(){
		status = StatusType.Working;
		while(userData.nowMoney >= GlobalData.UpScoreValueByOnce){
			userData.nowMoney -= GlobalData.UpScoreValueByOnce;

			userData.bankMoney += GlobalData.UpScoreValueByOnce;
			mainView.UpdateNowMoney (userData.nowMoney);
			mainView.UpdateBankMoney (userData.bankMoney);
			yield return new WaitForSeconds(GlobalData.UpScoreIntervalsTime);
		}

		status = StatusType.ReStart;
	}
	private bool CanUpScore(){
		return userData.nowMoney >= zoneData.upScore;
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
		 endSuitType = sever.GetSuitType ();
		print ("結束牌型 :　" +endSuitType);
		//同花大順是0 高牌是10
		if ((int)endSuitType < (int)SuitType.Pair) {
			status = StatusType.Win;
			OtherPokerCardsToDark ();
			mainView.OpenWinBoundsEffect (endSuitType);
		} 
		else {
			status = StatusType.Lose;
			AllPokerCardsToDark ();
			yield return new WaitForSeconds (1.0f);

			ResetGame ();
		}
	}

	private void AllPokerCardsToDark(){
		for (int i = 0; i < pokerCards.Length; i++) {
			pokerCards [i].ToDark ();
		}
	}

	private void OtherPokerCardsToDark(){
		string[] _fiveSuitCards = Regex.Split (sever.GetSuitFivePokerCards (), ",");

		for (int i = 0; i < pokerCards.Length; i++) {
			for (int j = 0; j < _fiveSuitCards.Length; j++) {
				if (pokerCards [i].GetValue () == _fiveSuitCards [j]) {
					break;
				}
				if (j == _fiveSuitCards.Length - 1) {
					pokerCards [i].ToDark ();
				}
			}
		}
	}

	public void OnOpenAllCardsBtn(){
		if (status == StatusType.Working || isBet () == false || status == StatusType.Win) {
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
		if (status == StatusType.Working || status != StatusType.Win) return;

		print ("----\t取得金錢\t----") ;
		userData.winMoney = zoneData.magnification[(int)endSuitType] * userData.totalBet;
		StartCoroutine (IE_GetMoney ());
	}


	IEnumerator IE_GetMoney(){
		status = StatusType.Working;
		while (userData.winMoney > 0) {
			userData.winMoney -= GlobalData.GetMoneySpeed;
			int _offset = 0;
			if (userData.winMoney - GlobalData.GetMoneySpeed < 0) {
				_offset = 0 - userData.winMoney;
				userData.winMoney = 0;
			}

			userData.nowMoney += GlobalData.GetMoneySpeed - _offset;
			mainView.SetBounsValue (endSuitType, userData.winMoney);
			mainView.UpdateNowMoney (userData.nowMoney);
			yield return null;
		}

		ResetGame ();
	}

	private void ResetGame(){
		print ("----\t重製遊戲\t----");

		sever.ResetPokerDeck ();
		ResetAllPokerCards ();
		ResetUserDataRoundBet ();
		mainView.UpdateRoundBets (userData.roundsBets);
		mainView.ReSetBounsText ();
		mainView.ReSetBounsColor ();
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
