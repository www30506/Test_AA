using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokerCard : MonoBehaviour {
	[SerializeField]private string value;
	private RawImage thisRawImage;
	private bool isTurn = false;

	void Awake(){
		thisRawImage = this.GetComponent<RawImage> ();
	}

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void SetData(string p_Value){
		thisRawImage.enabled = true;
		value = p_Value;
		isTurn = false;
		thisRawImage.texture = Resources.Load ("Textures/PokerCards/PokerBack") as Texture;
	}

	public void Turn(){
		if (isTurn == false) {
			thisRawImage.texture = Resources.Load ("Textures/PokerCards/" + value) as Texture;
		}
	}

	public void Reset(){
		thisRawImage.enabled = false;
	}
}
