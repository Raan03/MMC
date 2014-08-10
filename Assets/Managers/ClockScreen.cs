using UnityEngine;
using System.Collections;
using SH = Classes.Shared;
using GS = Managers.GameScreen;

namespace Managers
{
	public class ClockScreen : MonoBehaviour
	{
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

		void DoCountDown()
		{
			_remainingTime = _startTime - Time.time;
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

		void TimeUp()
		{
			Debug.Log("TimeUp!");
		}
	}
}