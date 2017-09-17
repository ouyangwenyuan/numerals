using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[ExecuteInEditMode]
[RequireComponent(typeof(AdMob))]
[DisallowMultipleComponent]
public class AdsManager : MonoBehaviour
{
		private static AdMob admob;
		public static AdsManager instance;
		public List<AdEvent> adEvents = new List<AdEvent> ();

		void Awake ()
		{
				if (Application.isPlaying) {
						Init ();
				}
		}

		void Update ()
		{
				if (!Application.isPlaying) {
						BuildAdEvents ();
				}
		}

		public void ShowAdvertisment (AdEvent.Event evt)
		{
				if (adEvents == null) {
						return;
				}

				foreach (AdEvent adEvent in adEvents) {
						if (adEvent.evt == evt) {
								if (adEvent.isEnabled) {
										if (adEvent.type == AdEvent.Type.BANNER) {
												//Show banner advertisment
												admob.RequestBanner (adEvent.adPostion);
												admob.ShowBanner();
										} else if (adEvent.type == AdEvent.Type.INTERSTITIAL) {
												//Show Interstitial Advertisment
												admob.RequestInterstitial ();
												admob.ShowInterstitial ();
										} else if (adEvent.type == AdEvent.Type.RewardBasedVideo) {
												//Show RewardBasedVideo Advertisment
												admob.RequestRewardBasedVideo ();
												admob.ShowRewardBasedVideo ();
										}
								}
								break;
						}
				}
		}

		public void HideAdvertisment ()
		{
			admob.HideBanner ();
		}

		private void Init ()
		{
				if (instance == null) {
						instance = this;
						DontDestroyOnLoad (gameObject);
						if (admob == null)
								admob = GetComponent<AdMob> ();
				} else {
						Destroy (gameObject);
				}
		}

		private void BuildAdEvents ()
		{
				System.Array eventsEnum = System.Enum.GetValues (typeof(AdEvent.Event));
				foreach (AdEvent.Event e in eventsEnum) {
						if (!InAdEventsList (adEvents, e)) {
								adEvents.Add (new AdEvent (){ evt = e});
						}
				}
		}

		[System.Serializable]
		public class AdEvent
		{
				public Event evt;
				public Type type = Type.BANNER;
				public GoogleMobileAds.Api.AdPosition adPostion = GoogleMobileAds.Api.AdPosition.Bottom;
				public bool isEnabled = true;

				public enum Event
				{
						ON_SHOW_WIN_DIALOG,
						ON_SHOW_PAUSE_DIALOG,
						ON_SHOW_RESET_GAME_DIALOG,
						ON_SHOW_LOCKED_DIALOG,
						ON_SHOW_NEED_HELP_DIALOG,
						ON_SHOW_EXIT_DIALOG,
						ON_RENEW_HELP_COUNT,
						ON_LOAD_LOGO_SCENE,
						ON_LOAD_MAIN_SCENE,
						ON_LOAD_OPTIONS_SCENE,
						ON_LOAD_HTP_SCENE,
						ON_LOAD_MISSIONS_SCENE,
						ON_LOAD_LEVELS_SCENE,
						ON_LOAD_GAME_SCENE,
						ON_LOAD_ABOUT_SCENE,
				}

				public enum Type
				{
						BANNER,
						INTERSTITIAL,
						RewardBasedVideo
				}
		}

		private bool InAdEventsList (List<AdEvent> adEvents, AdEvent.Event evt)
		{
				if (adEvents == null) {
						return false;
				}

				foreach (AdEvent adEvent in adEvents) {
						if (adEvent.evt == evt) {
								return true;
						}
				}
				return false;
		}
}