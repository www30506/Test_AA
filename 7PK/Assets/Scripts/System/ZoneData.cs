using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneData {
	public int[] magnification = new int[8]; //倍率 (依序為 同花大順 同花順...)
	public int openScoreOfOne = 1000; //一次開多少分
	public int downScoreOfOne = 500; //一次下多少分
	public int downScoreBouns = 100; //下分時的額外贈分
	public int upScore = 1000; //開分上限
	public int oneBetMoney = 50; //一次押注金額


	public ZoneData(int p_Zone){
		Debug.Log ("----\t設定分區資料\t----");

		SetMagnification();
	}
		

	private void SetMagnification(){
		Debug.Log ("設定倍率資料");

		for(int i=0; i< magnification.Length; i++){
			magnification [i] = (i * 2) + 1;
		}
	}
}
