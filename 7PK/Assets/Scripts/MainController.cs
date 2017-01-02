using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MainController : MonoBehaviour {
	[SerializeField]private StatusType status =  StatusType.ReStart;
	[SerializeField]private MainView mainView;
	[SerializeField]private int totalBet;
	[SerializeField]private PokerCard[] pokerCards;
	private ZoneData zoneData;
	[SerializeField]private UserData userData;

	void Start () {
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

		#if UNITY_EDITOR
		if(Input.GetKeyUp(KeyCode.P)){
			print("User BankMoney : " + userData.bankMoney);
		}
		#endif
	}

	public void OnOpenScoreBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t開分\t----");
		#endif

		userData.bankMoney += zoneData.openScoreOfOne;
		mainView.UpdateBankMoney (userData.bankMoney);
	}

	public void OnDownScoreBtn(){
		if (status == StatusType.Working) return;

		if((userData.bankMoney >= 0 && userData.nowMoney <= zoneData.oneBetMoney) == false) {
			#if Clog
			print ("下分指令無效");
			#endif
			return;
		}

		#if Clog
		print ("----\t下分\t----");
		#endif

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

		#if Clog
		print ("----\t上分\t----");
		#endif

	}

	public void OnBetBtn(){
		if (status == StatusType.Working) return;

		if (((int)status <= 3) == false) {
			#if Clog 
			print ("指令無效"); 
			#endif
			return;	
		}

		StartCoroutine (IE_Bet ());
	}

	IEnumerator IE_Bet(){
		#if Clog
		print ("----\t押注\t----");
		#endif

		StatusType _preStatus = status;
		status = StatusType.Working;

		userData.roundsBets [(int)_preStatus] += zoneData.oneBetMoney;
		mainView.UpdateRoundBets (userData.roundsBets);

		yield return StartCoroutine(OpenOncePokerCard (_preStatus));

		status = (StatusType)((int)++_preStatus);
	}

	public void OnOpenAllCardsBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t全開\t----");
		#endif
	}

	public void OnGetMoneyBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t取得金錢\t----");
		#endif
	}

	private void ResetGame(){
		#if Clog
		print ("----\t重製遊戲\t----");
		#endif

		ResetAllPokerCards ();
		ResetUserDataRoundBet ();
		status = StatusType.ReStart;
	}

	private void ResetAllPokerCards(){
		#if Clog
		print ("重製全部撲克卡");
		#endif

		for (int i = 0; i < pokerCards.Length; i++) {
			pokerCards [i].Reset ();
		}
	}

	private void ResetUserDataRoundBet(){
		#if Clog
		print ("重製玩家每輪押注金額");
		#endif

		for (int i = 0; i < userData.roundsBets.Length; i++) {
			userData.roundsBets [i] = 0;
		}
	}

	IEnumerator OpenOncePokerCard(StatusType p_nowStatus){
		#if Clog
		print ("開啟一次撲克牌");
		#endif

		switch ((int)p_nowStatus + 1) {
		case (int) StatusType.OneRound:
			break;
		case (int) StatusType.TwoRound:
			break;
		case (int) StatusType.ThreeRound:
			break;
		case (int) StatusType.FourRound:
			break;
		}

		yield return new WaitForSeconds(3.0f);
	}
}
