using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;


public class PokerCardSever : MonoBehaviour {
	private List<string> pokerDeckBase = new List<string>{
		"JR_0","JB_0",
		"C_2","D_2","H_2","S_2",
		"C_3","D_3","H_3","S_3",
		"C_4","D_4","H_4","S_4",
		"C_5","D_5","H_5","S_5",
		"C_6","D_6","H_6","S_6",
		"C_7","D_7","H_7","S_7",
		"C_8","D_8","H_8","S_8",
		"C_9","D_9","H_9","S_9",
		"C_10","D_10","H_10","S_10",
		"C_11","D_11","H_11","S_11",
		"C_12","D_12","H_12","S_12",
		"C_13","D_13","H_13","S_13",
		"C_1","D_1","H_1","S_1"
	};

	[SerializeField]private List<string> pokerDeck;
	[SerializeField]private List<string> haveSevenPokerCards = new List<string>(); //擁有的七張牌
	[SerializeField]private int[] suit_Colors = new int[4];
	[SerializeField]private int[] suit_Numbers = new int[13];
	[SerializeField]private int[,] suit_cards = new int[4, 13];
	[Header("------")]
	[SerializeField]private int suit_JokerCount;
	[SerializeField]private List<string> suitPokerCards = new List<string>(); //最後牌型的幾張牌

	void Start () {
	}

	void Update () {		
	}

	public string[] GetRoundOneCards(){
		string[] _cardsValue = new string[3];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			haveSevenPokerCards.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundTwoCards(){
		string[] _cardsValue = new string[2];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			haveSevenPokerCards.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundThreeCards(){
		string[] _cardsValue = new string[1];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			haveSevenPokerCards.Add (_cardsValue [i]);
			pokerDeck.Remove (_cardsValue [i]);
		}

		return _cardsValue;
	}

	public string[] GetRoundFourCards(){
		string[] _cardsValue = new string[1];

		for (int i = 0; i < _cardsValue.Length; i++) {
			_cardsValue [i] = GetOneCard ();
			haveSevenPokerCards.Add (_cardsValue [i]);
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
		haveSevenPokerCards.Clear();
		suitPokerCards.Clear();

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
		SortPokerSuit ();
		SetJudgmentSuitData (haveSevenPokerCards);

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

		suit_cards [_colorNumber, int.Parse(p_number)-1] ++;
	}
		
	private bool IsRoyalFlush(){
		ClearSuitPokerCards ();
		for (int i = 0; i < 4; i++) {
			int j = 9;
			int _tempJokerCount = suit_JokerCount;
			int _comboCount = 0;

			for (int k = 0; k < 5; k++) {
				int _number = (j + k == 13) ? 0 : j + k;
				if (suit_cards[i,_number] > 0) {
					_comboCount++;
				} else if (_tempJokerCount > 0) {
					_tempJokerCount--;
					_comboCount++;
				} 
	
				if (k == 4 && _comboCount >= 5) {
					//儲存卡片 用來知道是哪幾張牌是最後牌型
					string[] _cards = SearchSTRFlushCards ((ColorType)i,j + 1);
					SaveSuitPokerCards (_cards);
					SaveJokerCardsToSuitPokerCards ();
					return true;
				}
			}
		}
		return false;
	}

	private bool IsFiveKind(){
		ClearSuitPokerCards ();

		for (int i = 13; i >0; i--) {
			int _number = i == 13 ? 0 : i;
			if (suit_Numbers [_number] +suit_JokerCount >= 5) {
				//儲存卡片 用來知道是哪幾張牌是最後牌型
				string[] _cards = SearchPairCards (_number+1);
				SaveSuitPokerCards (_cards);
				SaveJokerCardsToSuitPokerCards ();
				return true;
			}
		}
		return false;
	}

	private bool IsSTRFlush(){
		ClearSuitPokerCards ();
		for (int i = 0; i < 4; i++) {
			for (int j = 9; j >= 0; j--) {
				int _tempJokerCount = suit_JokerCount;
				int _comboCount = 0;

				for (int k = 0; k < 5; k++) {
					int _number = (j + k == 13) ? 0 : j + k;
					if (suit_cards[i,_number] > 0) {
						_comboCount++;
					} else if (_tempJokerCount > 0) {
						_tempJokerCount--;
						_comboCount++;
					} 

					if (k == 4 && _comboCount >= 5) {
						//儲存卡片 用來知道是哪幾張牌是最後牌型
						string[] _cards = SearchSTRFlushCards ((ColorType)i,j + 1);
						SaveSuitPokerCards (_cards);
						SaveJokerCardsToSuitPokerCards ();
						return true;
					}
				}
			}
		}
		return false;
	}

	private string[] SearchSTRFlushCards(ColorType p_color, int p_startCardNumber){
		List<string> _cards = new List<string> ();
		int[] _fiveCardNumber = new int[5];

		if (p_startCardNumber == 1) {
			_fiveCardNumber [0] = 1;
			for (int i = 4; i > 0; i--) {
				_fiveCardNumber [4 - i + 1] = (i + p_startCardNumber) == 14 ? 1 : (i + p_startCardNumber);
			}
		} else {
			for (int i = 4; i >= 0; i--) {
				_fiveCardNumber [4 - i] = (i + p_startCardNumber) == 14 ? 1 : (i + p_startCardNumber);
			}
		}

		for (int i = 0; i < _fiveCardNumber.Length; i++) {
			for (int j = 0; j < haveSevenPokerCards.Count; j++) {
				string _cardvalue = ((ColorType)p_color).ToString() + "_" + _fiveCardNumber[i]; 
					if (haveSevenPokerCards[j] == _cardvalue) {
					_cards.Add (haveSevenPokerCards [j]);
					break;
				}
			}
		}

		return _cards.ToArray ();
	}

	private bool IsFourKind(){
		ClearSuitPokerCards ();

		for (int i = 13; i >0; i--) {
			int _number = i == 13 ? 0 : i;
			if (suit_Numbers [_number] +suit_JokerCount >= 4) {
				//儲存卡片 用來知道是哪幾張牌是最後牌型
				string[] _cards = SearchPairCards (_number+1);
				SaveSuitPokerCards (_cards);
				SaveJokerCardsToSuitPokerCards ();
				return true;
			}
		}
		return false;
	}

	private bool IsFullHourse(){
		ClearSuitPokerCards ();

		//因為不可能有2張鬼牌的 葫蘆牌型
		//有鬼牌的葫蘆 鬼牌一定是當最大的那個對子湊成三條
		for (int i = 13; i >0; i--) {
			int _number = i == 13 ? 0 : i;

			if (suit_Numbers [_number] + suit_JokerCount >= 3) {
				for (int k = 13; k > 0; k--) {
					int _number_II = k == 13 ? 0 : k;
					if (suit_Numbers [_number_II] >= 2 && i != k) {
						//儲存卡片 用來知道是哪幾張牌是最後牌型
						string[] _cards = SearchPairCards (_number+1);
						SaveSuitPokerCards (_cards);
						string[] _cards_II = SearchPairCards (_number_II+1);
						SaveSuitPokerCards (_cards_II);
						SaveJokerCardsToSuitPokerCards ();
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool IsFlush(){
		for (int i = 0; i < suit_Colors.Length; i++) {
			if (suit_Colors [i] + suit_JokerCount >= 5) {
				//儲存卡片 用來知道是哪幾張牌是最後牌型
				string[] _cards = SearchColorCards((ColorType)i);
				SaveSuitPokerCards (_cards);
				SaveJokerCardsToSuitPokerCards ();

				return true;
			}
		}
		return false;
	}

	private string[] SearchColorCards(ColorType p_colorType){
		List<string> _cards = new List<string> ();
		for (int i = 0; i < haveSevenPokerCards.Count; i++) {
			if (_cards.Count >= 5) {
				break;
			}

			string[] _cardValue = Regex.Split (haveSevenPokerCards [i], "_");
			if (_cardValue [0] == p_colorType.ToString ()) {
				_cards.Add (haveSevenPokerCards [i]);
			}
		}
		return _cards.ToArray();
	}

	private bool IsStraight(){
		ClearSuitPokerCards ();
		for (int i = 9; i >=0; i--) {
			int _tempJokerCount = suit_JokerCount;
			int _comboCount = 0;

			for (int j=0 ; j < 5; j++) {
				int _number = (i+j == 13) ? 0 : i+j;
				if (suit_Numbers [_number] >0) {
					_comboCount++;
				} else if (_tempJokerCount > 0) {
					_tempJokerCount--;
					_comboCount++;
				} 

				if (j==4 && _comboCount >= 5) {
					//儲存卡片 用來知道是哪幾張牌是最後牌型
					string[] _cards = SearchStraightCards (i+1);
					SaveSuitPokerCards (_cards);
					SaveJokerCardsToSuitPokerCards ();
					return true;
				}
			}
		}
		return false;
	}

	private string[] SearchStraightCards(int p_startCardNumber){
		List<string> _cards = new List<string> ();
		int[] _fiveCardNumber = new int[5];

		if (p_startCardNumber == 1) {
			_fiveCardNumber [0] = 1;
			for (int i = 4; i > 0; i--) {
				_fiveCardNumber [4 - i + 1] = (i + p_startCardNumber) == 14 ? 1 : (i + p_startCardNumber);
			}
		} else {
			for (int i = 4; i >= 0; i--) {
				_fiveCardNumber [4 - i] = (i + p_startCardNumber) == 14 ? 1 : (i + p_startCardNumber);
			}
		}

		for (int i = 0; i < _fiveCardNumber.Length; i++) {
			for (int j = 0; j < haveSevenPokerCards.Count; j++) {
				string[] _value = Regex.Split (haveSevenPokerCards [j], "_");
				if (int.Parse (_value [1]) == _fiveCardNumber [i]) {
					_cards.Add (haveSevenPokerCards [j]);
					break;
				}
			}
		}

		return _cards.ToArray ();
	}

	private bool IsThreeKind(){
		ClearSuitPokerCards ();
		for (int i = 13; i >0; i--) {
			int _number = i == 13 ? 0 : i;
			if (suit_Numbers [_number] +suit_JokerCount >= 3) {
				
				//儲存卡片 用來知道是哪幾張牌是最後牌型
				string[] _cards = SearchPairCards (_number+1);
				SaveSuitPokerCards (_cards);
				SaveJokerCardsToSuitPokerCards ();
				return true;
			}
		}

		return false;;
	}

	private bool IsTwoPair(){
		ClearSuitPokerCards ();
		int _pairCoung = 0;
		bool _hasNumberMoreTen = false;

		//從A -> K -> Q ...
		for (int i = 13; i >0; i--) {
			int _number = i == 13 ? 0 : i;
			if (suit_Numbers [_number] >= 2) {
				_pairCoung++;

				if (i == 0 || i > 8) {
					_hasNumberMoreTen = true;
				}

				//儲存卡片 用來知道是哪幾張牌是最後牌型
				string[] _cards = SearchPairCards (_number+1);
				SaveSuitPokerCards (_cards);
			}
		}

		return (_pairCoung>=2 && _hasNumberMoreTen);
	}

	private string[] SearchPairCards(int p_number){
		List<string> _cards = new List<string> ();
		for (int i = 0; i < haveSevenPokerCards.Count; i++) {
			if (_cards.Count + suitPokerCards.Count >= 5) {
				break;
			}

			string[] _value = Regex.Split (haveSevenPokerCards [i], "_");
			if (int.Parse(_value [1]) == p_number) {
				_cards.Add (haveSevenPokerCards [i]);
			}
		}

		return _cards.ToArray ();
	}

	public SuitType TestSetPokerSuit(string p_cardsValue){
		ClearJudgmentSuitData ();
		string[] _cardsValue = Regex.Split(p_cardsValue,",");
		haveSevenPokerCards = new List<string> (_cardsValue);

		return GetSuitType ();
	}

	private void SortPokerSuit(){
		List<string> _tempPokerSuit = new List<string>(haveSevenPokerCards);
		haveSevenPokerCards.Clear();

		for(int i=pokerDeckBase.Count-1; i>=0; i--){
			for(int j=0 ; j<_tempPokerSuit.Count; j++){
				if (pokerDeckBase [i] == _tempPokerSuit [j]) {
					haveSevenPokerCards.Add (_tempPokerSuit [j]);
					_tempPokerSuit.RemoveAt(j);
				}	
			}
		}
	}

	private void SaveSuitPokerCards(string[] p_string){
		for(int i=0; i<p_string.Length;i++){
			suitPokerCards.Add(p_string[i]);
		}
	}

	private void SaveJokerCardsToSuitPokerCards(){
		for (int i = 0; i < haveSevenPokerCards.Count; i++) {
			if (suitPokerCards.Count >= 5) {
				break;
			}

			if (haveSevenPokerCards [i] == "JB_0") {
				suitPokerCards.Add("JB_0");
			}

			if (haveSevenPokerCards [i] == "JR_0") {
				suitPokerCards.Add("JR_0");
			}
		}
	}

	private void ClearSuitPokerCards(){
		for(int i=0; i<suitPokerCards.Count;i++){
			suitPokerCards.Clear();
		}
	}

	public string GetSuitFivePokerCards(){
		string _cards = "";
		for (int i = 0; i < suitPokerCards.Count; i++) {
			if (i != 0) {
				_cards += ",";
			}
			_cards += suitPokerCards [i];
		}

		for (int i = suitPokerCards.Count; i < 5; i++) {
			if (i != 0) {
				_cards += ",";
			}
			_cards += SearchOtherHightCard ();
		}
		return _cards;
	}

	private string SearchOtherHightCard(){
		for (int i = 0; i < haveSevenPokerCards.Count; i++) {
			bool _isRepeatCard = false;
			for (int j = 0; j < suitPokerCards.Count; j++) {
				if (haveSevenPokerCards [i] == suitPokerCards [j]) {
					_isRepeatCard = true;
					break;
				}
			}

			if (_isRepeatCard == false) {
				suitPokerCards.Add (haveSevenPokerCards [i]);
				return haveSevenPokerCards [i];
			}
		}
		return "";
	}
}
