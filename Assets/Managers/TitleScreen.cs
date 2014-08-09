using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SH = Classes.Shared;

namespace Managers
{
	public class TitleScreen : MonoBehaviour
	{
		string _instructions;
		Rect _instructionRectangle;

		int _playPromptHeight;
		int _playPromptWidth;
        
		float _halfScreenWidth;
		float _halfScreenHeight;
        
		float _halfPromptWidth;
		float _halfPromptHeight;

		// Use this for initialization
		void GetInstructions()
		{
			TextAsset instructionText = (TextAsset)Resources.Load("instructions", typeof(TextAsset));
			_instructions = instructionText.text;
            
			_instructionRectangle = new Rect(0f, (float)Screen.height * 0.8f, 
				(float)Screen.width, 
				(float)Screen.height * 0.2f);
		}

		void Start()
		{
			GetInstructions();

			_playPromptHeight = Mathf.CeilToInt(Screen.height * 0.40f);
			_playPromptWidth = Mathf.CeilToInt(Screen.width * 0.80f);
            
			_halfScreenWidth = Screen.width * 0.5f;
			_halfScreenHeight = Screen.height * 0.5f;
            
			_halfPromptWidth = _playPromptWidth * 0.5f;
			_halfPromptHeight = _playPromptHeight * 0.5f;

		}
	
		// Update is called once per frame
		void OnGUI()
		{
			Play();
		}

		void Play()
		{
			SH.RenderBackGround();

		
			GUI.BeginGroup(new Rect(_halfScreenWidth - _halfPromptWidth,
					_halfScreenHeight - _halfPromptHeight,
					_playPromptWidth, _playPromptHeight));

			RenderTitle(_playPromptHeight, _playPromptWidth);


			RenderButtons(_halfPromptWidth, _halfPromptHeight);
			RenderInstructions(_playPromptWidth, _playPromptHeight);
       
			GUI.EndGroup();
		}

		void RenderButtons(float halfPromptWidth, float halfPromptHeight)
		{
			int buttonWidth = 125;
			int buttonHeight = 20;
        
			if (GUI.Button(new Rect(halfPromptWidth - (buttonWidth / 2f),
					             halfPromptHeight - (buttonHeight / 2f),
					             buttonWidth, buttonHeight),
				             "Play"
			             ))
			{
				Application.LoadLevel(1);
			}
			if (GUI.Button(new Rect(halfPromptWidth - (buttonWidth / 2f),
					             halfPromptHeight + (buttonHeight / 2f),
					             buttonWidth, buttonHeight),
				             "Credits"
			             ))
			{
				Application.LoadLevel(2);
			}
			if (GUI.Button(new Rect(halfPromptWidth - (buttonWidth / 2f),
					             halfPromptHeight + (buttonHeight * 1.5f),
					             buttonWidth, buttonHeight),
				             "Quit"
			             ))
			{
				Application.Quit();
			}

		}

		/// <summary>
		/// Seperated for better reading
		/// </summary>
		/// <param name="playPromptWidth">Play prompt width.</param>
		/// <param name="playPromptHeight">Play prompt height.</param>
		void RenderInstructions(int playPromptWidth, int playPromptHeight)
		{
			GUIStyle instructionStyle = new GUIStyle(GUI.skin.box);         

			int paddenWidth = Mathf.CeilToInt(Screen.width * 0.02f);
			int paddenHeight = Mathf.CeilToInt(Screen.height * 0.02f);

			instructionStyle.wordWrap = true;
			instructionStyle.richText = true;
			instructionStyle.alignment = TextAnchor.LowerCenter;
			instructionStyle.padding = new RectOffset(paddenWidth, paddenWidth,
				paddenHeight, paddenHeight);

			GUI.Box(new Rect(0, 0, playPromptWidth, playPromptHeight), 
				_instructions, 
				instructionStyle);
		}

		/// <summary>
		/// Seperated for better reading
		/// </summary>
		/// <param name="playPromptHeight">Play prompt height.</param>
		/// <param name="playPromptWidth">Play prompt width.</param>
		static void RenderTitle(int playPromptHeight, int playPromptWidth)
		{
			int paddenWidth = Mathf.CeilToInt(Screen.width * 0.02f);
			int paddenHeight = Mathf.CeilToInt(Screen.height * 0.02f);

			GUIStyle titleStyle = new GUIStyle(GUI.skin.box);
			titleStyle.font = Resources.Load<Font>("AlexBrush");
			titleStyle.fontSize = 50;
			titleStyle.richText = true;
			titleStyle.wordWrap = true;
			titleStyle.alignment = TextAnchor.UpperCenter;
			titleStyle.focused.textColor = Color.yellow;
			titleStyle.padding = new RectOffset(paddenWidth, paddenWidth,
				paddenHeight, paddenHeight);

			GUI.Box(new Rect(0, 0, playPromptWidth, playPromptHeight), 
				"<i><b><color=\"red\">-></color><color=\"white\">Raan03</color><color=\"red\"><-</color><color=\"white\"> 's mmc</color></b></i>", 
				titleStyle);
		}
	}
}
