using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {
	private enum StatusType {ReStart, Working};
	private StatusType status =  StatusType.ReStart;
	[SerializeField]private MainView mainView;
	[SerializeField]private int totalBet;
	private ZoneData zoneData;

	void Start () {
		zoneData = new ZoneData (1);
	}
	
	void Update () {
		if (Input.GetKeyUp (KeyCode.A)) {
			OnOpenScoreBtn ();
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
}
