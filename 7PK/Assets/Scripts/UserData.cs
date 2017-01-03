using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData  {
	public int bankMoney; //銀行所有的金額
	public int nowMoney; //持有的金錢數量

	//總共押注多少
	public int totalBet{
		get{
			int _totalBet = 0;
			for (int i = 0; i < 4; i++) {
				_totalBet += roundsBets [i];
			}
			return _totalBet;
		}
	}

	public int[] roundsBets = new int [4]; //每輪押注多少

}
