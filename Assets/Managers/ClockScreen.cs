﻿using UnityEngine;
using System.Collections;
using SH = Classes.Shared;
using GS = Managers.GameScreen;

namespace Managers
{
    public class ClockScreen : MonoBehaviour
    {
        Texture2D _spareTime;
        Texture2D _moderateTime;
        Texture2D _shortTime;
        bool _isPaused = false;
        // in seconds
        float _startTime;
        // in seconds
        float _remainingTime;
        float _remainingPercentage;
        float _clockWidth;
        float _clockHeight;
        // Use this for initialization
        void Start()
        {
            guiText.color = Color.black;
            _startTime = 120.0f;

            _spareTime = (Texture2D)Resources.Load("white", 
                                                   typeof(Texture2D));
            _moderateTime = (Texture2D)Resources.Load("black", 
                                                      typeof(Texture2D));
            _shortTime = (Texture2D)Resources.Load("red", 
                                                   typeof(Texture2D));

        }
	
        // Update is called once per frame
        void Update()
        {
            if (!_isPaused)
            {
                // making sure we add legit pressure
                DoCountDown();
            }
        }

        void OnGUI()
        {
            RenderPictures();
        }
        void DoCountDown()
        {
            _remainingTime = _startTime - Time.time;
            _remainingPercentage = Mathf.CeilToInt((_remainingTime / _startTime) * 100);
            if (_remainingTime < 0)
            {
                _remainingTime = 0;
                ToggleClock();
                TimeUp();
            }
            ShowTime();
        }

        void ToggleClock()
        {
            _isPaused = !_isPaused;
        }

        void ShowTime()
        {
            int _minutes;
            int _seconds;
            string timeString;
            _minutes = (int)_remainingTime / 60;
            _seconds = (int)_remainingTime % 60;
            timeString = string.Format("{0:D2}:{1:D2}", _minutes, _seconds);
            guiText.text = timeString;

        }

        void RenderPictures()
        {
            Debug.Log(string.Format("RenderPictures: percentage: {0} ", 
                                    _remainingPercentage));
            int heightMargin = 5;
            // 5% of Screen width
            float maxBarWidth = Screen.width * 0.05f;
            // 90% of screen height
            float maxBarHeight = Screen.height * (1 - (2 * heightMargin * 0.01f));
            // bottom = screenwidh - (barheight + margin)
            float actualBottom = Screen.height - (Screen.height * heightMargin * 0.01f);


            // get 1/100th for picture
            float imageHeight = maxBarHeight * 0.01f;

            Texture2D actualImage;

            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.alignment = TextAnchor.MiddleCenter;

            if (_remainingPercentage > 80)
            {
                actualImage = _spareTime;
                labelStyle.normal.textColor = Color.black;
            } else
            {
                if (_remainingTime > 20)
                {
                    actualImage = _moderateTime;
                    labelStyle.normal.textColor = Color.white;
                } else
                {
                    actualImage = _shortTime;
                    labelStyle.normal.textColor = Color.black;
                }
            }
            
            // We need screen height (=bottom)
            // minus the image loop we already displayed
            // e.g. first one (at bottom:
            // screenheight - 1 times the height of image

            // note: we want the bar occupying 90% of height
            // means: at 100 percent, 90% height
            // but the bar must shrink DOWN
            // so top position will change
            // at 100%, we must be at 5% from top
            // at 0% we must be at 95% from top

            float actualHeight = (_remainingPercentage * imageHeight);
            float actualTop = (actualBottom - actualHeight);

            GUI.DrawTexture(new Rect(Screen.width - maxBarWidth,
                                     actualTop,
                                     maxBarWidth,
                                     actualHeight)
                            , actualImage);
            GUI.Label(new Rect(Screen.width - maxBarWidth,
                               actualTop,
                               maxBarWidth,
                               actualHeight)
                      , string.Format("{0}%", _remainingPercentage), labelStyle);

            // I can imagine this (old) code being horribly inefficient/slow!
            /*for (int i = 0; i<_remainingPercentage; i++)
            {
                Texture2D actualImage;

                GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
                labelStyle.stretchWidth = true;
                labelStyle.stretchHeight = true;
                labelStyle.alignment = TextAnchor.MiddleCenter;

                Color oldColor = GUI.color;
                if (i > 80)
                {
                    actualImage = _spareTime;
                    GUI.color = Color.black;
                } else
                {
                    if (i > 20)
                    {
                        actualImage = _moderateTime;
                        GUI.color = Color.white;
                    } else
                    {
                        actualImage = _shortTime;
                        GUI.color = Color.black;
                    }
                }

                // We need screen height (=bottom)
                // minus the image loop we already displayed
                // e.g. first one (at bottom:
                // screenheight - 1 times the height of image
                //FIXME: bar stop resizing after a while?
                float actualHeight = (_remainingPercentage * barHeight);

                GUI.DrawTexture(new Rect(Screen.width - barWidth,
                                         Screen.height - barHeight,
                                         barWidth,
                                         actualHeight)
                                , actualImage);
                GUI.Label(new Rect(Screen.width - barWidth,
                                   Screen.height - barHeight,
                                   barWidth,
                                   actualHeight)
                          , _remainingPercentage.ToString());

                // restore our default colorscheme
                GUI.color = oldColor;
            }*/
            
        }
        void TimeUp()
        {
            Debug.Log("TimeUp!");
        }
    }
}