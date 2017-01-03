using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerCardSever : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	public string[] GetRoundOneCards(){
		string[] _cardsValue = new string[3]{"C_1", "C_1","C_1"};
		return _cardsValue;
	}

	public string[] GetRoundTwoCards(){
		string[] _cardsValue = new string[2]{"C_1","C_1"};
		return _cardsValue;
	}

	public string[] GetRoundThreeCards(){
		string[] _cardsValue = new string[1]{"C_1"};
		return _cardsValue;
	}

	public string[] GetRoundFourCards(){
		string[] _cardsValue = new string[1]{"C_1"};
		return _cardsValue;
	}

	public bool IsWin(){
		return false;
	}
}
