using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneData {
	public int[] magnification = new int[8]; //倍率 (依序為 同花大順 同花順...)
	public int openScoreOfOne = 1000; //一次開多少分

	public ZoneData(int p_Zone){
		#if Clog
		Debug.Log ("----\t設定分區資料\t----");
		#endif

		SetMagnification();
	}
		

	private void SetMagnification(){
		#if Clog
		Debug.Log ("設定倍率資料");
		#endif

		for(int i=0; i< magnification.Length; i++){
			magnification [i] = (i * 2) + 1;
		}
	}
}
