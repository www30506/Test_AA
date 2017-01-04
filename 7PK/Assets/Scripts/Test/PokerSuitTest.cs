using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerSuitTest : MonoBehaviour {
	[SerializeField]private PokerCardSever sever;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.F1)) {
			print ("【單元測試 - 牌型比較】");
			TestTwoPair ();
			TestThreeKind ();
			TestStraight ();
			TestFlush ();
			TestFullHourse ();
			TestFourKind ();
			TestSTRFlush ();
			TestFiveKind ();
			TestRoyalFlush ();
		}	
	}

	private void TestTwoPair(){
		string[] _cards = 
			{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.TwoPair) {
				Debug.LogError ("【兩對】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestThreeKind(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.ThreeKind) {
				Debug.LogError ("【三條】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestStraight(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.Straight) {
				Debug.LogError ("【順子】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestFlush(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.Flush) {
				Debug.LogError ("【同花】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestFullHourse(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FullHourse) {
				Debug.LogError ("【葫蘆】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private  void TestFourKind(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FourKind) {
				Debug.LogError ("【四條】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestSTRFlush(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.STRFlush) {
				Debug.LogError ("【同花順】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestFiveKind(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FiveKind) {
				Debug.LogError ("【五條】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}

	private void TestRoyalFlush(){
		string[] _cards = 
		{"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.RoyalFlush) {
				Debug.LogError ("【同花大順】判斷錯誤 第" +(i+1) + "組");
			}
		}
	}
}
