using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class WinDialog : MonoBehaviour
{
		/// <summary>
		/// Number of stars for the WinDialog.
		/// </summary>
		private StarsNumber starsNumber;

		/// <summary>
		/// Star sound effect.
		/// </summary>
		public AudioClip starSoundEffect;

		/// <summary>
		/// Win dialog animator.
		/// </summary>
		public Animator WinDialogAnimator;

		/// <summary>
		/// First star fading animator.
		/// </summary>
		public Animator firstStarFading;

		/// <summary>
		/// Second star fading animator.
		/// </summary>
		public Animator secondStarFading;

		/// <summary>
		/// Third star fading animator.
		/// </summary>
		public Animator thirdStarFading;

		/// <summary>
		/// The level title text.
		/// </summary>
		public Text levelTitle;
		
		/// <summary>
		/// The level score text.
		/// </summary>
		public Text levelScore;

		/// <summary>
		/// The level best score text.
		/// </summary>
		public Text levelBestScore;

		/// <summary>
		/// The timer reference.
		/// </summary>
		public Timer timer;

		/// <summary>
		/// The effects audio source.
		/// </summary>
		private AudioSource effectsAudioSource;

		// Use this for initialization
		void Awake ()
		{
				///Setting up the references
				if (WinDialogAnimator == null) {
						WinDialogAnimator = GetComponent<Animator> ();
				}

				if (firstStarFading == null) {
						firstStarFading = transform.Find ("Stars").Find ("FirstStarFading").GetComponent<Animator> ();
				}

				if (secondStarFading == null) {
						secondStarFading = transform.Find ("Stars").Find ("SecondStarFading").GetComponent<Animator> ();
				}

				if (thirdStarFading == null) {
						thirdStarFading = transform.Find ("Stars").Find ("ThirdStarFading").GetComponent<Animator> ();
				}

				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
				
				if (levelTitle == null) {
						levelTitle = transform.Find ("Level").GetComponent<Text> ();
				}

				if (levelScore == null) {
						levelScore = transform.Find ("Score").Find ("Value").GetComponent<Text> ();
				}

				if (levelBestScore == null) {
						levelBestScore = transform.Find ("BestScore").Find ("Value").GetComponent<Text> ();
				}

				if (timer == null) {
						timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				//Hide the Win Dialog
				Hide ();
		}

		/// <summary>
		/// Show the Win Dialog.
		/// </summary>
		public void Show ()
		{
				if (WinDialogAnimator == null) {
						return;
				}
				WinDialogAnimator.SetTrigger ("Running");
		}

		/// <summary>
		/// Hide the Win Dialog.
		/// </summary>
		public void Hide ()
		{
				StopAllCoroutines ();
				WinDialogAnimator.SetBool ("Running", false);
				firstStarFading.SetBool ("Running", false);
				secondStarFading.SetBool ("Running", false);
				thirdStarFading.SetBool ("Running", false);
		}

		/// <summary>
		/// Fade stars Coroutine.
		/// </summary>
		/// <returns>The stars.</returns>
		public IEnumerator FadeStars ()
		{
				starsNumber = StarsRating.GetWinDialogStarsRating (timer.timeInSeconds, GameManager.movements, Mission.selectedMission.rowsNumber * Mission.selectedMission.colsNumber);

				float delayBetweenStars = 0.5f;
				if (starsNumber == StarsNumber.ONE) {//Fade with One Star
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						firstStarFading.SetTrigger ("Running");
				} else if (starsNumber == StarsNumber.TWO) {//Fade with Two Stars
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						secondStarFading.SetTrigger ("Running");
				} else if (starsNumber == StarsNumber.THREE) {//Fade with Three Stars
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						firstStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						secondStarFading.SetTrigger ("Running");
						yield return new WaitForSeconds (delayBetweenStars);
						if (effectsAudioSource != null)
								CommonUtil.PlayOneShotClipAt (starSoundEffect, Vector3.zero, effectsAudioSource.volume);
						thirdStarFading.SetTrigger ("Running");
				}
				yield return 0;
		}

		/// <summary>
		/// Set the level title.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetLevelTitle (string value)
		{
				if (string.IsNullOrEmpty (value) || levelTitle == null) {
						return;
				}
				levelTitle.text = value;
		}

		/// <summary>
		/// Set the score.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetScore (float value)
		{
				if (levelScore == null) {
						return;
				}

				if (value == Mathf.Infinity) {
						levelScore.text = "Score : -";
				} else {
						levelScore.text = "Score : " + value;
				}
		}
		
		/// <summary>
		/// Set the best score.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetBestScore (float value)
		{
				if (levelBestScore == null) {
						return;
				}

				if (value == Mathf.Infinity) {
						levelBestScore.text = "Best Score : -";
				} else {
						levelBestScore.text = "Best Score : " + value;
				}
		}
		
		public enum StarsNumber
		{
				ONE,
				TWO,
				THREE
		}
}