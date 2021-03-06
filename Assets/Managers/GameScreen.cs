﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CA = Classes.Card;
using SH = Classes.Shared;

namespace Managers
{
    public class GameScreen : MonoBehaviour
    {
        // how many columns we want?
        public int columns = 4;
        // how many rows do we want?
        public int rows = 4;
        // how many cards do we want to display?
        public int totalCards = 16;
        // how many cards needed for a complete match?
        public int matchesPerCard = 2;
        // how many matches do we need for a win?
        public int matchesNeedToWin = 8;
        // the name of our backsidePicture
        public string backSideCard = "picture";
        // how many matches did we already make?
        int _matchesMade = 0;

        // how many sets do we have? Known limitation of Free Unity -- you
        // cannot access the resource folder at runtime as
        // it's been compiled to 1 file
        public int totalSets = 4;

        // all cards
        List<CA> _aCards = new List<CA>();

        // cards in the grid
        CA[,] _aGrid = null;

        // All cards that have been flipped
        List<CA> _cardsFlipped = new List<CA>();

        // can the Player already click?
        public bool playerCanClick = false;

        // Player won the game
        public bool playerHasWon = false;
        
        // Use this for initialization
        void Start()
        {
            playerCanClick = true;
            _aGrid = new CA[rows, columns];

            BuildDeck();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int someNumber = Random.Range(0, _aCards.Count);
                    _aGrid [i, j] = _aCards [someNumber];
                    _aCards.RemoveAt(someNumber);
                }
            }
        }

        /// <summary>
        /// Raises the GUI event.
        /// </summary>
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            BuildGrid();
            if (playerHasWon)
            {
                BuildWinPrompt();       
            }
            GUILayout.EndArea();
        }

        /// <summary>
        /// Builds the grid.
        /// </summary>
        /// <returns>The grid.</returns>
        void BuildGrid()
        {
            int cardWidth = Screen.width / (columns + 1);
            int cardHeight = Screen.height / (rows + 1);
                
            SH.RenderBackGround();

            GUILayout.BeginVertical();
            // Flexible space enables us to use dynamic spaces between cards,
            // dependent on available screen space
            GUILayout.FlexibleSpace();

            for (int i = 0; i < rows; i++)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                for (int j = 0; j < columns; j++)
                {
                    CA card = _aGrid [i, j];
                    string img;
                    if (card.isMatched)
                    {
                        // replace our string image number with 
                        // MatchPerCard + 1
                        string image = card.img.Substring(0, card.img.Length - 1)
                            + (matchesPerCard + 1);
                        img = image;
                    } else
                    {
                        if (card.isFaceUp)
                        {
                            img = card.img;
                        } else
                        {
                            img = backSideCard;
                        }
                    }

                    GUI.enabled = !card.isMatched;
                    if (GUILayout.Button((Texture2D)Resources.Load(img, 
							                   typeof(Texture2D)),
						                   GUILayout.Width(cardWidth), 
						                   GUILayout.Height(cardHeight)))
                    {
                        if (playerCanClick)
                        {
                            StartCoroutine(FlipCardFaceUp(card));
                        }
                    }
                                
                    GUI.enabled = true;
                }

                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
        }

        IEnumerator FlipCardFaceUp(CA card)
        {
            card.isFaceUp = true;

            if (_cardsFlipped.IndexOf(card) < 0)
            {
                _cardsFlipped.Add(card);

                if (_cardsFlipped.Count == 2)
                {
                    playerCanClick = false;

                    yield return new WaitForSeconds(1.5f);

                    if (MatchingCards(_cardsFlipped [1].id, _cardsFlipped [0].id))
                    {
                        _cardsFlipped [0].isMatched = true;
                        _cardsFlipped [1].isMatched = true;

                        _matchesMade++;
                        if (_matchesMade >= matchesNeedToWin)
                        {
                            playerHasWon = true;
                        }
                    } else
                    {
                        _cardsFlipped [0].isFaceUp = false;
                        _cardsFlipped [1].isFaceUp = false;                 
                    }

            
                    _cardsFlipped = new List<CA>();
                    playerCanClick = true;
                }
            }
        }

        /// <summary>
        /// Does the 2 strings match eachother in terms of the Cards?
        /// e.g. we don't want xxx1 and xxx1 but xxx1 and xxx2
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        bool MatchingCards(string left, string right)
        {
            bool leftMatch = left.StartsWith(
				                          right.Substring(0, 2), 
				                          System.StringComparison.CurrentCultureIgnoreCase
            );

            if (leftMatch)
            {
                bool rightMatch = left.EndsWith(
					                              right.Substring(right.Length - 1, 1),
					                              System.StringComparison.CurrentCultureIgnoreCase
                );
                if (!rightMatch)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Builds the deck.
        /// </summary>
        /// <returns>The deck.</returns>
        void BuildDeck()
        {   
                
            CA card = null;
            int id = 0;

            for (int i = 0; i < totalSets; i++)
            {
                List<string> pictureParts = new List<string>();
                for (int j = 0; j < matchesPerCard; j++)
                {
                    string partName = string.Format("_{0}", j + 1);
                    pictureParts.Add(partName);
                }

                for (int j = 0; j < matchesPerCard; j++)
                {
                    int someNumber = Random.Range(0, pictureParts.Count);
                    string missingPart = pictureParts [someNumber];

                    pictureParts.RemoveAt(someNumber);

                    string imageName = string.Format("picture-{0}{1}",
						                                  (i + 1), 
						                                  missingPart);
                    string imageId = string.Format("{0}{1}", 
						                                i, 
						                                missingPart);

                    card = new CA(imageName, imageId);
                    _aCards.Add(card);

                    card = new CA(imageName, imageId);
                    _aCards.Add(card);

                    id++;
                }
            }
        }

        /// <summary>
        /// Builds the Winner prompt.
        /// </summary>
        /// <returns>The winner prompt.</returns>
        void BuildWinPrompt()
        {
            int winPromptHeight = 90;
            int winPromptWidth = 120;

            float halfScreenWidth = Screen.width / 2;
            float halfScreenHeight = Screen.height / 2;

            float halfPromptWidth = winPromptWidth / 2;
            float halfPromptHeight = winPromptHeight / 2;

            GUI.BeginGroup(new Rect(halfScreenWidth - halfPromptWidth,
					halfScreenHeight - halfPromptHeight,
					winPromptWidth, winPromptHeight));
            GUI.Box(new Rect(0, 0, winPromptWidth, winPromptHeight),
				"We have a winner!");

            int buttonWidth = 80;
            int buttonHeight = 20;

            if (GUI.Button(new Rect(halfPromptWidth - (buttonWidth / 2),
					             halfPromptHeight - (buttonHeight / 2),
					             buttonWidth, buttonHeight),
				             "Credits"
            ))
            {
                Application.LoadLevel(2);
            }
            GUI.EndGroup();
        }

    }
}