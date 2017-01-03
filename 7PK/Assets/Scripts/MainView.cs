using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainView : MonoBehaviour {
	[SerializeField]private Text bankMoenyText;
	[SerializeField]private Text[] bounds;
	[SerializeField]private Text[] boundsValue;
	[SerializeField]private Text[] roundBetsText;
	[SerializeField]private Text nowMoneyText;
	[SerializeField]private Text UpMoneyText;
		
	void Start () {
		
	}
	
	void Update () {
		
	}

	public void ReSetBounsText(){
		print("UI-重設獎勵倍率");

		for (int i = 0; i < boundsValue.Length; i++) {
			boundsValue [i].text = "1";
		}
	}

	public void UpdateBounsText(int[] p_magnification, int p_totalBet){
		print("UI-設定獎勵倍率");

		if (p_totalBet < 1) {
			Debug.LogError ("壓注金額 不應該低於1");
			return;
		}

		for (int i = 0; i < boundsValue.Length; i++) {
			boundsValue [i].text = (p_magnification[i] * p_totalBet).ToString();
		}
	}

	public void UpdateBankMoney(int p_bankMoneyValue){
		print("UI-更新銀行金錢");

		bankMoenyText.text = p_bankMoneyValue.ToString ();
	}

	public void UpdateNowMoney(int p_nowMoneyValue){
		print ("UI-更新現有金錢");

		nowMoneyText.text = p_nowMoneyValue.ToString ();
	}

	public void UpdateRoundBets(int[] p_roundBets){
		print("UI-更新每輪押注");

		for (int i = 0; i < roundBetsText.Length; i++) {
			roundBetsText [i].text = p_roundBets [i].ToString();
		}
	}
}