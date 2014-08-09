﻿
namespace Classes
{
	public class Card
	{
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
}