using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TitleScreen : MonoBehaviour {
	private string instructions;
	private Rect instructionRectangle;
	// Use this for initialization
	void Start () {
		TextAsset instructionText = (TextAsset)Resources.Load ("instructions", typeof(TextAsset));
		instructions = instructionText.text;

			instructionRectangle = new Rect(0f,(float) Screen.height*0.8f,(float) Screen.width, (float) Screen.height*0.2f);
	}
	
	// Update is called once per frame
	void OnGUI () {
		Play ();
	}
	void Play ()
	{
		int playPromptHeight = Mathf.CeilToInt(Screen.height * 0.40f);
		int playPromptWidth = Mathf.CeilToInt(Screen.width * 0.80f);
		
		float halfScreenWidth = Screen.width * 0.5f;
		float halfScreenHeight = Screen.height * 0.5f;
		
		float halfPromptWidth = playPromptWidth * 0.5f;
		float halfPromptHeight = playPromptHeight * 0.5f;
		
		GUI.BeginGroup (new Rect (halfScreenWidth - halfPromptWidth,
		                          halfScreenHeight - halfPromptHeight,
		                          playPromptWidth, playPromptHeight));

		GUIStyle titleStyle = new GUIStyle (GUI.skin.box);
		titleStyle.richText = true;
		titleStyle.wordWrap = true;

		GUI.Box (new Rect (0, 0, playPromptWidth, playPromptHeight),
		         "<b><color=\"red\">-></color>RAAN03<color=\"red\"><-</color></b> 's MMC", titleStyle);
		
		int buttonWidth = 125;
		int buttonHeight = 20;

		if (GUI.Button (new Rect (halfPromptWidth - (buttonWidth / 2f),
		                          halfPromptHeight - (buttonHeight / 2f),
		                          buttonWidth, buttonHeight),
		                "Play"
		                )) {
			Application.LoadLevel (1);
		}
		if (GUI.Button (new Rect (halfPromptWidth - (buttonWidth / 2f),
		                          halfPromptHeight + (buttonHeight / 2f),
		                          buttonWidth, buttonHeight),
		                "Credits"
		                )) {
			Application.LoadLevel(2);
		}
		if (GUI.Button (new Rect (halfPromptWidth - (buttonWidth / 2f),
		                          halfPromptHeight + (buttonHeight * 1.5f),
		                          buttonWidth, buttonHeight),
		                "Quit"
		                )) {
			Application.Quit();
		}
		GUIStyle instructionStyle = new GUIStyle (GUI.skin.box);
		
		int paddenWidth = Mathf.CeilToInt (Screen.width * 0.02f);
		int paddenHeight = Mathf.CeilToInt (Screen.height * 0.02f);
		instructionStyle.wordWrap = true;
		instructionStyle.richText = true;
		instructionStyle.alignment = TextAnchor.LowerCenter;
		instructionStyle.padding = new RectOffset (paddenWidth, paddenWidth,
		                                           paddenHeight, paddenHeight);

		GUI.Box(new Rect(0,0, playPromptWidth, playPromptHeight), instructions, instructionStyle);
		GUI.EndGroup ();
	}
}
