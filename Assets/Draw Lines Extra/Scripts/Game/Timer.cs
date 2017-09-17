using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class Timer : MonoBehaviour
{
		/// <summary>
		/// Text Component
		/// </summary>
		public Text uiText;

		/// <summary>
		/// The time in seconds.
		/// </summary>
		public int timeInSeconds;

		/// <summary>
		/// Whether the Timer is running
		/// </summary>
		private bool isRunning;

		/// <summary>
		/// Whether the timer is paused or not.
		/// </summary>
		private bool isPaused;

		void Awake ()
		{
				if (uiText == null) {
						uiText = GetComponent<Text> ();
				}
				///Start the Timer
				Start ();
		}

		/// <summary>
		/// Start the Timer.
		/// </summary>
		public void Start ()
		{
				if (!isRunning) {
						isRunning = true;
						timeInSeconds = 0;
						StartCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Stop the Timer.
		/// </summary>
		public void Stop ()
		{
				if (isRunning) {
						isRunning = false;
						StopCoroutine ("Wait");
				}
		}

		/// <summary>
		/// Pause the Timer.
		/// </summary>
		public void Pause ()
		{
				isPaused = true;
		}

		/// <summary>
		/// Resume the Timer.
		/// </summary>
		public void Resume ()
		{
				isPaused = false;
		}

		/// <summary>
		/// Wait Coroutine.
		/// </summary>
		private IEnumerator Wait ()
		{
				while (isRunning) {
						ApplyTime ();
						yield return new WaitForSeconds (1);
						if (!isPaused) {
								timeInSeconds++;
						}
				}
		}

		/// <summary>
		/// Applies the time into TextMesh Component.
		/// </summary>
		private void ApplyTime ()
		{
				if (uiText == null) {
						return;
				}
				uiText.text = timeInSeconds.ToString ();
		}
}