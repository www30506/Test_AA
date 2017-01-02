using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {
	private enum StatusType {ReStart, Working};
	private StatusType status =  StatusType.ReStart;
	[SerializeField]private MainView mainView;
	[SerializeField]private int totalBet;
	[SerializeField]private PokerCard[] pokerCards;
	private ZoneData zoneData;
	[SerializeField]private UserData userData;

	void Start () {
		zoneData = new ZoneData (1);
	}
	
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			OnOpenScoreBtn ();
		}

		if(Input.GetKeyUp(KeyCode.S)){
			OnDownScoreBtn();
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
		mainView.SetBankMoney (userData.bankMoney);
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

		mainView.SetBankMoney (userData.bankMoney);
		mainView.SetNowMaony (userData.nowMoney);
	}

	public void OnUpScoreBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t上分\t----");
		#endif

	}

	public void OnBetBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t押注\t----");
		#endif
	}

	public void OnOpenAllCardsBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t全開\t----");
		#endif
	}

	public void OnOpenOneCardBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t開一張牌\t----");
		#endif
	}

	public void OnGetMoneyBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t取得金錢\t----");
		#endif
	}
}
