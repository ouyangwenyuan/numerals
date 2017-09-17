using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class UIEvents : MonoBehaviour
{
		public void ChangeMusicLevel (AudioSourceSlider audioSourceSlider)
		{
				if (audioSourceSlider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [0].volume = audioSourceSlider.slider.value;
				audioSourceSlider.SetVolumePercentageText (audioSourceSlider.slider.value);
		}

		public void ChangeEffectsLevel (AudioSourceSlider audioSourceSlider)
		{
				if (audioSourceSlider == null) {
						return;
				}
				GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1].volume = audioSourceSlider.slider.value;
				audioSourceSlider.SetVolumePercentageText (audioSourceSlider.slider.value);
		}

		public void ShowResetGameConfirmDialog ()
		{
				//Show banner advertisment
				AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_RESET_GAME_DIALOG);
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<Dialog> ().Show ();
		}

		public void ShowExitConfirmDialog ()
		{
				//Show banner advertisment
				AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_EXIT_DIALOG);
				GameObject.Find ("ExitConfirmDialog").GetComponent<Dialog> ().Show ();
		}

		public void PointerButtonEvent (Pointer pointer)
		{
				if (pointer == null) {
						return;
				}
				if (pointer.group != null) {
						ScrollSlider scrollSlider = GameObject.FindObjectOfType (typeof(ScrollSlider)) as ScrollSlider;
						if (scrollSlider != null) {
								scrollSlider.DisableCurrentPointer ();
								FindObjectOfType<ScrollSlider> ().currentGroupIndex = pointer.group.Index;
								scrollSlider.GoToCurrentGroup ();
						}
				}
		}

		public void ResetGameConfirmDialogEvent (GameObject value)
		{
				if (value == null) {
						return;
				}

				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
						DataManager.instance.ResetGameData ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Reset Game Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ResetGameConfirmDialog").GetComponent<Dialog> ().Hide ();
				AdsManager.instance.HideAdvertisment ();
		}

		public void ExitConfirmDialogEvent (GameObject value)
		{
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Exit Confirm Dialog : Yes button clicked");
						Application.Quit ();
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Exit Confirm Dialog : No button clicked");
				}
				GameObject.Find ("ExitConfirmDialog").GetComponent<Dialog> ().Hide ();
				AdsManager.instance.HideAdvertisment ();
		}

		public void NeedHelpDialogEvent (GameObject value)
		{
				if (value.name.Equals ("YesButton")) {
						Debug.Log ("Need Help Dialog : Yes button clicked");
						GameObject.FindObjectOfType<GameManager> ().ResetHelpCount (true);
				} else if (value.name.Equals ("NoButton")) {
						Debug.Log ("Need Help : No button clicked");
				}
				GameManager.isRunning = true;
				GameObject.Find ("NeedHelpDialog").GetComponent<Dialog> ().Hide ();
				AdsManager.instance.HideAdvertisment ();
		}

		public void MissionButtonEvent (Mission mission)
		{
				if (mission == null) {
						Debug.Log ("Mission event parameter is undefined");
						return;
				}

				if (mission.isLocked) {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_LOCKED_DIALOG);
						GameObject.Find ("LockedDialog").GetComponent<Dialog> ().Show ();
						return;
				}

				Mission.selectedMission = mission;
				LoadLevelsScene ();
		}

		public void CloseLockedDialog ()
		{
				AdsManager.instance.HideAdvertisment();
				GameObject.Find ("LockedDialog").GetComponent<Dialog> ().Hide ();
		}

		public void LevelButtonEvent (TableLevel tableLevel)
		{
				if (tableLevel == null) {
						Debug.Log ("TableLevel Event parameter is undefined");
						return;
				}

				if (tableLevel.isLocked) {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_LOCKED_DIALOG);
						GameObject.Find ("LockedDialog").GetComponent<Dialog> ().Show ();
						return;
				}

				TableLevel.selectedLevel = tableLevel;
				LoadGameScene ();
		}

		public void RateUsButtonEvent (string appID)
		{
				#if UNITY_ANDROID
					Application.OpenURL("http://play.google.com/store/apps/details?id=" + appID);
				#elif UNITY_IPHONE
					Application.OpenURL("itms-apps://itunes.apple.com/app/"+appID);
				#endif
		}

		public void OpenLink (string link)
		{
				if (string.IsNullOrEmpty (link)) {
						return;
				}
				Application.OpenURL (link);
		}

		public void GameNextButtonEvent ()
		{
				GameObject.FindObjectOfType<GameManager> ().NextLevel ();
		}

		public void GameBackButtonEvent ()
		{
				GameObject.FindObjectOfType<GameManager> ().PreviousLevel ();
		}

		public void GameRefreshButtonEvent ()
		{
				GameObject.FindObjectOfType<GameManager> ().RefreshGrid ();
		}

		public void GameHelpButtonEvent ()
		{
				GameObject.FindObjectOfType<GameManager> ().Help ();
		}

		public void PauseButtonEvent(){
			AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_SHOW_PAUSE_DIALOG);
			GameObject.FindObjectOfType<GameManager> ().timer.Pause ();
			GameManager.isRunning = false;
			GameObject.Find ("PauseDialog").GetComponent<Dialog> ().Show ();
		}

		public void ResumeGameButtonEvent(){
			AdsManager.instance.HideAdvertisment ();
			GameObject.FindObjectOfType<GameManager> ().timer.Resume ();
			GameObject.Find ("PauseDialog").GetComponent<Dialog> ().Hide ();
			GameManager.isRunning = true;
		}

		public void WinDialogNextButtonEvent ()
		{
				if (TableLevel.selectedLevel.ID == LevelsTable.levels.Count) {
						LoadLevelsScene ();
						return;
				}
				BlackArea.Hide ();
				GameObject.FindObjectOfType<WinDialog> ().Hide ();
				GameObject.FindObjectOfType<GameManager> ().NextLevel ();
		}

		public void LoadMainScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("Main"));
		}

		public void LoadHowToPlayScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("HowToPlay"));
		}

		public void LoadMissionsScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("Missions"));
		}

		public void LoadOptionsScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("Options"));
		}

		public void LoadAboutScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("About"));
		}

		public void LoadLevelsScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("Levels"));
		}

		public void LoadGameScene ()
		{
				StartCoroutine (SceneLoader.LoadSceneAsync ("Game"));
		}
}