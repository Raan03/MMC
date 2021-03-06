﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SH = Classes.Shared;

namespace Managers
{
    /// <summary>
    /// Our self made credits screen
    /// </summary>
    public class CreditsScreen : MonoBehaviour
    {
        // we want to know what percentage to use for padding
        public int paddingPercentage = 5;
		
        // calculate the absolute pixels
        Dictionary<string,int> _paddingResolution;
		
        //the text ofcourse
        List<List<string>> _credits = new List<List<string>>();
        //used for the credits layout
        List<Rect> _creditRectangle = new List<Rect>();
		
        // the speed of scrolling our credits
        public float creditSpeed;
		
        // Use this for initialization
        /// <summary>
        /// On startup we need to load our credits in memory, position them and create rectangles for eye sight
        /// </summary>
        void Start()
        {
            // load our creditsFile and put it in a list
            TextAsset creditsFile = (TextAsset)Resources.Load("credits", typeof(TextAsset));
            List<string> tempList = creditsFile.text.Split(';').ToList();
            tempList.RemoveAll(x => string.IsNullOrEmpty(x));

            _paddingResolution = SH.CalculatePaddingInPixels(paddingPercentage);
            foreach (string x in tempList)
            {
                List<string> y = x.Split('|').ToList();
                if (!y.All(z => string.IsNullOrEmpty(z)))
                {
                    _credits.Add(y);
                }
            }
				
            // additionally also add some rectangles for better looking credits
            for (int i = 0; i < _credits.Count; i++)
            {
                _creditRectangle.Add(new Rect(0, (Screen.height / 2) + (30 * i), Screen.width, Screen.height));
            }
        }

        /// <summary>
        /// This will scroll our credits
        /// </summary>
        void OnGUI()
        {
            SH.RenderBackGround();
            // add styles to it, let it scroll upwards
            for (int i = 0; i < _credits.Count; i++)
            {

                GUIStyle leftStyle = new GUIStyle(GUI.skin.label);
                LayoutLeftStyle(leftStyle);

                GUIStyle rightStyle = new GUIStyle(GUI.skin.label);
                LayoutRightStyle(rightStyle);

                GUI.Label(_creditRectangle [i], _credits [i] [0].ToUpper(), leftStyle);
                GUI.Label(_creditRectangle [i], _credits [i] [1].ToLower(), rightStyle);
						
                Rect tempRect = _creditRectangle [i];
                tempRect.y = tempRect.y - creditSpeed * Time.deltaTime;
                _creditRectangle [i] = tempRect;
            }
            var last = _creditRectangle.Last();
            if (_creditRectangle.Last().y < 0 - (Screen.height))
                Application.LoadLevel(0);
        }

        /// <summary>
        /// Seperated for better reading
        /// </summary>
        /// <param name="leftStyle">Left style.</param>
        void LayoutLeftStyle(GUIStyle leftStyle)
        {
            leftStyle.alignment = TextAnchor.MiddleLeft;
            leftStyle.normal.textColor = Color.black;
            leftStyle.padding = new RectOffset(
				_paddingResolution ["Left"], 
				_paddingResolution ["Right"], 
				_paddingResolution ["Top"], 
				_paddingResolution ["Bottom"]);
        }

        /// <summary>
        /// Seperated for better reading
        /// </summary>
        /// <param name="rightStyle">Right style.</param>
        void LayoutRightStyle(GUIStyle rightStyle)
        {
            rightStyle.alignment = TextAnchor.MiddleRight;
            rightStyle.font = Resources.Load<Font>("AlexBrush");
            rightStyle.fontSize = 25;
            rightStyle.richText = true;
            rightStyle.normal.textColor = Color.black;
            rightStyle.padding = new RectOffset(
				_paddingResolution ["Left"], 
				_paddingResolution ["Right"], 
				_paddingResolution ["Top"], 
				_paddingResolution ["Bottom"]);
        }
    }
}
