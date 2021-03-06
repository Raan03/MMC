﻿namespace Classes
{
	/// <summary>
	/// Card object
	/// </summary>
	public class Card
	{
		public bool isFaceUp = false;
		public bool isMatched = false;
		public string img = null;
		public string id = "";

		/// <summary>
		/// Initializes empty card object
		/// </summary>
		public Card()
		{
			img = "picture";
		}

		/// <summary>
		/// Initializes existing card object
		/// </summary>
		/// <param name="img">ImageName.</param>
		/// <param name="id">stringID for matching.</param>
		public Card(string img, string id)
		{
			this.img = img;
			this.id = id;
		}
	}
}