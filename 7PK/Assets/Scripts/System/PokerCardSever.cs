using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class PokerCardSever : MonoBehaviour {
	private List<string> pokerDeckBase = new List<string>{
		"C_1","C_2","C_3","C_4","C_5","C_6","C_7","C_8","C_9","C_10","C_11","C_12","C_13",
		"D_1","D_2","D_3","D_4","D_5","D_6","D_7","D_8","D_9","D_10","D_11","D_12","D_13",
		"H_1","H_2","H_3","H_4","H_5","H_6","H_7","H_8","H_9","H_10","H_11","H_12","H_13",
		"S_1","S_2","S_3","S_4","S_5","S_6","S_7","S_8","S_9","S_10","S_11","S_12","S_13",
		"JB_0","JR_0"
	};

	[SerializeField]private List<string> pokerDeck;
	[SerializeField]private List<string> pokerSuit;
	[SerializeField]private int[] suit_Colors = new int[4];
	[SerializeField]private int[] suit_Numbers = new int[13];
	[SerializeField]private int[,] suit_cards = new int[4, 13];
	[Header("------")]
	[SerializeField]private int suit_JokerCount;

	void Start () {
	}

	void Update () {		
	}

	public string[] GetRoundOneCards(){
		string[] _cardsValue = new string[3];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			pokerSuit.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundTwoCards(){
		string[] _cardsValue = new string[2];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			pokerSuit.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundThreeCards(){
		string[] _cardsValue = new string[1];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			pokerSuit.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundFourCards(){
		string[] _cardsValue = new string[1];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			pokerSuit.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	private string GetOneCard(){
		string _cardValue;
		int _random = Random.Range (0, pokerDeck.Count);
		return pokerDeck [_random];
	}

	public void ResetPokerDeck(){
		print ("Sever-洗牌庫");
		pokerDeck = new List<string> (pokerDeckBase.ToArray());
		pokerSuit = new List<string> ();

		ClearJudgmentSuitData ();
	}

	private void ClearJudgmentSuitData(){
		for (int i = 0; i < suit_Colors.Length; i++) {
			suit_Colors [i] = 0;
		}

		for (int i = 0; i < suit_Numbers.Length; i++) {
			suit_Numbers [i] = 0;
		}

		for (int i = 0; i < 4; i++) {
			for (int j = 0; j < 13; j++) {
				suit_cards [i, j] = 0;
			}
		}

		suit_JokerCount = 0;
	}

	public SuitType GetSuitType(){
		SuitType _suitType = SuitType.HightCard;
	
		SetJudgmentSuitData (pokerSuit);

//		print ("牌 : " + pokerSuit [0] + "," + pokerSuit [1] + "," + pokerSuit [2] + "," 
//			+ pokerSuit [3] + ","+ pokerSuit [4] + "," + pokerSuit [5] + "," + pokerSuit [6]);

		if (IsRoyalFlush ()) {
			_suitType = SuitType.RoyalFlush;
		} else if (IsFiveKind ()) {
			_suitType = SuitType.FiveKind;
		} else if (IsSTRFlush ()) {
			_suitType = SuitType.STRFlush;
		} else if (IsFourKind ()) {
			_suitType = SuitType.FourKind;
		} else if (IsFullHourse ()) {
			_suitType = SuitType.FullHourse;
		} else if (IsFlush ()) {
			_suitType = SuitType.Flush;
		} else if (IsStraight ()) {
			_suitType = SuitType.Straight;
		} else if (IsThreeKind ()) {
			_suitType = SuitType.ThreeKind;
		} else if (IsTwoPair ()) {
			_suitType = SuitType.TwoPair;
		} else {
			_suitType = SuitType.Pair;
		}
//		print ("牌型 : " + _suitType.ToString());
		return _suitType;
	}
		
	private void SetJudgmentSuitData(List<string> p_pokerSuit){
		for (int i = 0; i < p_pokerSuit.Count; i++) {
			string[] _value = Regex.Split (p_pokerSuit [i], "_");
			if (_value[0] == "JB" || _value[0] == "JR") {
				suit_JokerCount++;
			}
			else{
				SetSuitColorsData (_value[0]);
				SetSuitNumbersData (_value [1]);
				SetSuitCardsData (_value [0], _value [1]);
			}
		}
	}

	private void SetSuitColorsData(string p_color){
		if (p_color == "C") {
			suit_Colors [0]++;
		}
		else if (p_color == "D") {
			suit_Colors [1]++;
		}
		else if (p_color == "H") {
			suit_Colors [2]++;
		}
		else if (p_color == "S") {
			suit_Colors [3]++;
		}
	}

	private void SetSuitNumbersData(string p_number){
		suit_Numbers[int.Parse(p_number)-1]++;
	}

	private void SetSuitCardsData(string p_color, string p_number){
		int _colorNumber = 0;

		if (p_color == "C") {
			_colorNumber = 0;
		}
		else if (p_color == "D") {
			_colorNumber = 1;
		}
		else if (p_color == "H") {
			_colorNumber = 2;
		}
		else if (p_color == "S") {
			_colorNumber = 3;
		}

		suit_cards [_colorNumber, int.Parse(p_number)] ++;
	}
		
	private bool IsRoyalFlush(){
		return false;
	}

	private bool IsFiveKind(){
		return false;
	}

	private bool IsSTRFlush(){
		return false;
	}

	private bool IsFourKind(){
		return false;
	}

	private bool IsFullHourse(){
		return false;
	}

	private bool IsFlush(){
		return false;
	}

	private bool IsStraight(){
		return false;
	}

	private bool IsThreeKind(){
		return false;
	}

	private bool IsTwoPair(){
		int _pairCoung = 0;
		bool _hasNumberMoreTen = false;

		for (int i = 0; i < 13; i++) {
			if (suit_Numbers [i] >= 2) {
				_pairCoung++;
				if (i == 0 || i > 8) {
					_hasNumberMoreTen = true;
				}
			}
		}

		return (_pairCoung>=2 && _hasNumberMoreTen);
	}

	public SuitType TestSetPokerSuit(string p_cardsValue){
		ClearJudgmentSuitData ();
		string[] _cardsValue = Regex.Split(p_cardsValue,",");
		pokerSuit = new List<string> (_cardsValue);

		return GetSuitType ();
	}
}
