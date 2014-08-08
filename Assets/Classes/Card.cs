using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card {
	public bool isFaceUp = false;
	public bool isMatched = false;
	public string img = null;
	public string id = "";
	public Card()
	{
		img = "picture";
	}
	public Card(string img, string id)
	{
		this.img = img;
		this.id = id;
	}
}
