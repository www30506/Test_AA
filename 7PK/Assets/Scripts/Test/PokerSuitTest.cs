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
		print ("【兩對】牌型檢測");
		string[] _cards = {
			"C_1,H_1,C_2,S_5,H_9,S_9,H_11",
			"C_10,H_1,C_2,S_5,H_9,S_9,H_10"
		};

		string[] _fiveCards = {
			"H_1,C_1,S_9,H_9,H_11",
			"H_10,C_10,S_9,H_9,H_1"
		};
		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.TwoPair) {
				Debug.LogError ("【兩對】判斷錯誤 第" +(i+1) + "組\n"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}


			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【兩對】亮的五張牌不正確 第" + (i + 1) + "組\n" +
				"應該顯示 : " + _fiveCards [i] + "\n" +
				"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestThreeKind(){
		print ("【三條】牌型檢測");
		string[] _cards = {
			"C_1,H_1,S_1,S_5,H_7,S_9,H_11",
			"C_1,H_1,JB_0,S_5,H_9,S_3,H_10",
			"C_1,H_1,JR_0,S_5,H_3,S_9,H_10",
			"C_1,S_2,JR_0,JB_0,H_7,D_8,H_13"
		};

		string[] _fiveCards = {
			"S_1,H_1,C_1,H_11,S_9",
			"H_1,C_1,JB_0,H_10,H_9",
			"H_1,C_1,JR_0,H_10,S_9",
			"C_1,JB_0,JR_0,H_13,D_8"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.ThreeKind) {
				Debug.LogError ("【三條】判斷錯誤 第" +(i+1) + "組  \n" + 
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}
			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【三條】亮的五張牌不正確 第"+(i+1)+"組\n" + 
					"應該顯示 : " + _fiveCards [i] + "\n"+
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestStraight(){
		print ("【順子】牌型檢測");
		string[] _cards = {
			"C_1,H_2,C_3,S_5,H_4,S_9,H_11",
			"C_1,H_2,C_3,S_5,H_4,S_1,H_11",
			"C_10,H_1,C_11,S_12,H_13,S_7,H_5",
			"JB_0,H_1,C_11,S_12,H_13,S_7,H_5",
			"C_10,H_1,JR_0,S_12,H_13,S_7,H_5",
			"C_10,JR_0,C_11,S_12,H_13,S_7,H_5",
			"C_11,H_5,S_12,H_13,JB_0,D_6,JR_0",
			"C_10,D_4,S_1,JB_0,H_13,D_8,JR_0",
			"JR_0,D_8,S_9,H_10,H_13,D_11,S_12",
		};

		string[] _fiveCards = {
			"C_1,S_5,H_4,C_3,H_2",
			"S_1,S_5,H_4,C_3,H_2",
			"H_1,H_13,S_12,C_11,C_10",
			"H_1,H_13,S_12,C_11,JB_0",
			"H_1,H_13,S_12,C_10,JR_0",
			"H_13,S_12,C_11,C_10,JR_0",
			"H_13,S_12,C_11,JB_0,JR_0",
			"S_1,H_13,C_10,JB_0,JR_0",
			"H_13,S_12,D_11,H_10,JR_0",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.Straight) {
				Debug.LogError ("【順子】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【順子】亮的五張牌不正確 第"+(i+1)+"組\n" + 
					"應該顯示 : " + _fiveCards [i] + "\n"+
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestFlush(){
		print ("【同花】牌型檢測");
		string[] _cards = {
			"C_1,C_3,C_2,C_5,C_9,S_9,C_11",
			"C_1,H_1,C_2,C_5,C_9,S_8,JB_0",
			"C_1,H_11,C_2,C_6,JR_0,S_9,JB_0",
			"C_1,C_2,C_13,JR_0,C_9,C_8,JB_0"
		};

		string[] _fiveCards = {
			"C_1,C_11,C_9,C_5,C_3",
			"C_1,C_9,C_5,C_2,JB_0",
			"C_1,C_6,C_2,JB_0,JR_0",
			"C_1,C_13,C_9,C_8,C_2"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards [i]) != SuitType.Flush) {
				Debug.LogError ("【同花】判斷錯誤 第" + (i + 1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【同花】亮的五張牌不正確 第" + (i + 1) + "組\n" +
				"應該顯示 : " + _fiveCards [i] + "\n" +
				"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestFullHourse(){
		print ("【葫蘆】牌型檢測");
		string[] _cards = {
			"C_1,H_1,S_1,S_5,H_9,S_9,H_11",
			"C_1,H_1,S_1,S_5,H_9,S_9,C_9",
			"C_1,H_1,S_2,S_5,H_9,JB_0,C_9",
			"C_1,H_1,S_5,S_5,H_9,JR_0,C_9",
		};

		string[] _fiveCards = {
			"S_1,H_1,C_1,S_9,H_9",
			"S_1,H_1,C_1,S_9,H_9",
			"H_1,C_1,H_9,C_9,JB_0",
			"H_1,C_1,H_9,C_9,JR_0",
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FullHourse) {
				Debug.LogError ("【葫蘆】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【葫蘆】亮的五張牌不正確 第" + (i + 1) + "組\n" +
					"應該顯示 : " + _fiveCards [i] + "\n" +
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private  void TestFourKind(){
		print ("【四條】牌型檢測");
		string[] _cards = {
			"C_1,H_1,S_1,D_1,H_9,S_9,H_11",
			"C_10,H_10,D_10,JB_0,H_9,S_9,C_8",
		};

		string[] _fiveCards = {
			"S_1,H_1,D_1,C_1,H_11",
			"H_10,D_10,C_10,JB_0,S_9"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FourKind) {
				Debug.LogError ("【四條】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【四條】亮的五張牌不正確 第" + (i + 1) + "組\n" +
					"應該顯示 : " + _fiveCards [i] + "\n" +
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestSTRFlush(){
		print ("【同花順】牌型檢測");
		string[] _cards = {
			"C_1,C_2,C_3,H_5,C_5,H_11,C_4",
			"H_10,H_11,H_12,H_13,H_3,S_9,H_9",
			"H_10,H_11,H_12,JB_0,H_3,S_9,H_9",
			"H_10,H_8,H_12,H_5,JB_0,S_9,H_9",
			"JB_0,H_10,H_7,H_6,H_1,S_9,H_9",
			"JB_0,H_11,H_12,H_8,JR_0,S_9,H_9",
			"H_10,H_11,H_9,H_4,H_5,JR_0,JB_0"
		};

		string[] _fiveCards = {
			"C_1,C_5,C_4,C_3,C_2",
			"H_13,H_12,H_11,H_10,H_9",
			"H_12,H_11,H_10,H_9,JB_0",
			"H_12,H_10,H_9,H_8,JB_0",
			"H_10,H_9,H_7,H_6,JB_0",
			"H_12,H_11,H_9,JB_0,JR_0",
			"H_11,H_10,H_9,JB_0,JR_0"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.STRFlush) {
				Debug.LogError ("【同花順】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【同花順】亮的五張牌不正確 第" + (i + 1) + "組\n" +
					"應該顯示 : " + _fiveCards [i] + "\n" +
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestFiveKind(){
		print ("【五條】牌型檢測");
		string[] _cards = {
			"C_1,H_1,D_1,S_1,JB_0,S_9,H_11",
			"C_1,H_1,D_1,S_1,JB_0,JR_0,H_10",
			"C_1,H_1,D_1,S_2,JB_0,JR_0,H_10"	
		};

		string[] _fiveCards = {
			"S_1,H_1,D_1,C_1,JB_0",
			"S_1,H_1,D_1,C_1,JB_0",
			"H_1,D_1,C_1,JB_0,JR_0"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.FiveKind) {
				Debug.LogError ("【五條】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【五條】亮的五張牌不正確 第" + (i + 1) + "組\n" +
					"應該顯示 : " + _fiveCards [i] + "\n" +
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}

	private void TestRoyalFlush(){
		print ("【同花大順】牌型檢測");
		string[] _cards = {
			"H_10,H_11,H_12,H_13,H_1,S_9,H_9",
			"C_10,C_11,C_12,JB_0,C_1,S_9,C_9",
			"H_10,H_11,H_12,H_13,JB_0,S_9,H_9",
			"JB_0,D_11,D_12,D_13,D_1,S_9,D_9",

			"JB_0,H_11,H_12,H_13,JR_0,S_9,H_9",
			"S_10,S_11,S_12,S_13,S_1,JR_0,JB_0",
			"H_10,JB_0,JR_0,H_13,H_1,S_9,H_9",
		};

		string[] _fiveCards = {
			"H_1,H_13,H_12,H_11,H_10",
			"C_1,C_12,C_11,C_10,JB_0",
			"H_13,H_12,H_11,H_10,JB_0",
			"D_1,D_13,D_12,D_11,JB_0",

			"H_13,H_12,H_11,JB_0,JR_0",
			"S_1,S_13,S_12,S_11,S_10",
			"H_1,H_13,H_10,JB_0,JR_0"
		};

		for (int i = 0; i < _cards.Length; i++) {
			if (sever.TestSetPokerSuit (_cards[i]) != SuitType.RoyalFlush) {
				Debug.LogError ("【同花大順】判斷錯誤 第" +(i+1) + "組"+
					"算成 " + sever.TestSetPokerSuit (_cards [i]));
			}

			if (sever.GetSuitFivePokerCards () != _fiveCards [i]) {
				Debug.LogError ("【同花大順】亮的五張牌不正確 第" + (i + 1) + "組\n" +
					"應該顯示 : " + _fiveCards [i] + "\n" +
					"顯示為 : " + sever.GetSuitFivePokerCards ());
			}
		}
	}
}
