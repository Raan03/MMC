using UnityEngine;
using GS = Managers.GameScreen;
using SH = Classes.Shared;

namespace Managers
{
    public class ClockScreen : MonoBehaviour
    {
        Texture2D _spareTime;
        Texture2D _moderateTime;
        Texture2D _shortTime;
        bool _isPaused = false;
        // start time in seconds
        float _startTime;
        // remaining time in seconds
        float _remainingTime;
        float _remainingPercentage;
        // clockWidth in pixels
        float _clockWidth;
        // clockHeight in pixels
        float _clockHeight;

        /// <summary>
        /// What to do when we fire up for first time?
        /// </summary>
        void Start()
        {
            guiText.color = Color.black;
            // startTime can be 120 for now, for debugging purposes
            _startTime = 120.0f;

            // we assign the images of our bar
            // spareTime = more than enough time left (>80%)
            // moderateTime = we might hurry up a little
            // shortTime = faster, faster! (<20%)
            _spareTime = (Texture2D)Resources.Load("white", 
				typeof(Texture2D));
            _moderateTime = (Texture2D)Resources.Load("black", 
				typeof(Texture2D));
            _shortTime = (Texture2D)Resources.Load("red", 
				typeof(Texture2D));

        }

        /// <summary>
        /// Ticks on every tick
        /// </summary>
        void Update()
        {
            if (!_isPaused)
            {
                // making sure we add legit pressure
                DoCountDown();
            }
        }

        /// <summary>
        /// On each tick that needs a renderer
        /// </summary>
        void OnGUI()
        {
            RenderClockBar();
        }

        /// <summary>
        /// our CountDown module
        /// </summary>
        void DoCountDown()
        {
            // 1) we need to subtract time & update the percentage
            // 2) if time is up, we stop counting down
            // 3) we need to show the time

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

        /// <summary>
        /// Switch the clock countdown
        /// </summary>
        void ToggleClock()
        {
            _isPaused = !_isPaused;
        }

        /// <summary>
        /// Renders our time in hh:mm:ss
        /// </summary>
        void ShowTime()
        {
            int _minutes;
            int _seconds;
            string timeString;
            _minutes = (int)_remainingTime / 60;
            _seconds = (int)_remainingTime % 60;
            // D2 = will show us 2 digits
            timeString = string.Format("{0:D2}:{1:D2}", _minutes, _seconds);
            guiText.text = timeString;

        }

        /// <summary>
        /// Renders the pictures.
        /// </summary>
        void RenderClockBar()
        {
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

            //TODO eliminate the "magic" numbers
            //TODO textcolor must be put somewhere with the magic 
            // numbers as well
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
            
            // Logic:
            // 1) What is our actual top/actual height?
            // 2) Draw our bar, bottom end is fixed
            // top of the bar will ofcourse be decreasing
            float actualHeight = (_remainingPercentage * imageHeight);
            float actualTop = (actualBottom - actualHeight);

            GUI.DrawTexture(new Rect(Screen.width - maxBarWidth,
				actualTop,
				maxBarWidth,
				actualHeight)
                            , actualImage);

            // also add a nice percentage of what is "left"
            GUI.Label(new Rect(Screen.width - maxBarWidth,
				actualTop,
				maxBarWidth,
				actualHeight)
                      , string.Format("{0}%", _remainingPercentage), labelStyle);
                            
        }

        /// <summary>
        /// Logic for when time is up
        /// </summary>
        void TimeUp()
        {
            // Display message about Pebos firing your ass
            // Game ends here
            Debug.Log("TimeUp!");
        }
    }
}