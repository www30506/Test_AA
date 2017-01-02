using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData  {
	public int bankMoney; //銀行所有的金額
	public int nowMoney; //持有的金錢數量
	public int totalBet; //總共押注多少
	public int[] roundsBets = new int [4]; //每輪押注多少

}
