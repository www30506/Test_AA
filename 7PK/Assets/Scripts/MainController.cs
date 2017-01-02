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

	void Start () {
		zoneData = new ZoneData (1);
	}
	
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			OnOpenScoreBtn ();
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			for (int i = 0; i < pokerCards.Length; i++) {
				pokerCards [i].SetData ("C_1");
				pokerCards [i].Turn ();
			}
		}

		#if UNITY_EDITOR
		if(Input.GetKeyUp(KeyCode.P)){
			print("User BankMoney : " + UserData.bankMoney);
		}
		#endif
	}

	public void OnOpenScoreBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t開分\t----");
		#endif

		UserData.bankMoney += zoneData.openScoreOfOne;
		mainView.SetBankMoney (UserData.bankMoney);
	}

	public void OnDownScoreBtn(){
		if (status == StatusType.Working) return;

		#if Clog
		print ("----\t下分\t----");
		#endif
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
