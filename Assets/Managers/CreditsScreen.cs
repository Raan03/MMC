using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;

public class CreditsScreen : MonoBehaviour
{		
		// we want to know what percentage to use for padding
		public int PaddingPercentage = 5;
		
		// calculate the absolute pixels
		private int paddingHeight = Mathf.CeilToInt (Screen.height * 0.05f);
		private int paddingWidth = Mathf.CeilToInt (Screen.width * 0.05f);
		
		//
		private List<List<string>> credits = new List<List<string>> ();
		private List<Rect> creditRectangle = new List<Rect> ();
		
		// the speed of scrolling our credits
		public float creditSpeed;
		
		// Use this for initialization
		/// <summary>
		/// On startup we need to load our credits in memory, position them and create rectangles for eye sight
		/// </summary>
		void Start ()
		{
				// load our creditsFile and put it in a list
				TextAsset creditsFile = (TextAsset)Resources.Load ("credits", typeof(TextAsset));
				List<string> tempList = creditsFile.text.Split (';').ToList ();
				tempList.RemoveAll(x => string.IsNullOrEmpty(x));
				foreach (string x in tempList) {
						List<string> y = x.Split ('|').ToList ();
						if (!y.All (z => string.IsNullOrEmpty (z))) {
								credits.Add (y);
						}
				}
				
				// additionally also add some rectangles for better looking credits
				for (int i = 0; i<credits.Count; i++) {
						creditRectangle.Add (new Rect (0, (Screen.height / 2) + (30 * i), Screen.width, Screen.height));
				}
		}
	
		/// <summary>
		/// This will scroll our credits
		/// </summary>
		void OnGUI ()
		{
				// add styles to it, let it scroll upwards
				for (int i = 0; i < credits.Count; i++) {

						GUIStyle leftStyle = new GUIStyle (GUI.skin.label);
						leftStyle.alignment = TextAnchor.MiddleLeft;
						leftStyle.padding = new RectOffset (paddingWidth, paddingWidth, paddingHeight, paddingHeight);

						GUIStyle rightStyle = new GUIStyle (GUI.skin.label);
						rightStyle.alignment = TextAnchor.MiddleRight;
						rightStyle.richText = true;
						rightStyle.padding = new RectOffset (paddingWidth, paddingWidth, paddingHeight, paddingHeight);

						GUI.Label (creditRectangle [i], credits [i] [0].ToUpper (), leftStyle);
						GUI.Label (creditRectangle [i], credits [i] [1].ToUpperInvariant (), rightStyle);
						
						Rect tempRect = creditRectangle [i];
						tempRect.y = tempRect.y - creditSpeed;
						creditRectangle [i] = tempRect;
				}
		var last = creditRectangle.Last ();
		if (creditRectangle.Last ().y < 0 - (Screen.height))
						Application.LoadLevel (0);
		}
}
