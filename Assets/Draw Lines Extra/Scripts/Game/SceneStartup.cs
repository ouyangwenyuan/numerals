using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class SceneStartup : MonoBehaviour
{
		public string sceneName;

		void Awake(){
			AdsManager.instance.HideAdvertisment ();
		}	

		// Use this for initialization
		void Start ()
		{
				if (string.IsNullOrEmpty (sceneName)) {
						return;
				}

				if (sceneName == "Logo") {
					AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_LOGO_SCENE);
				} else if (sceneName == "Main") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_MAIN_SCENE);
				} else if (sceneName == "Options") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_OPTIONS_SCENE);
				} else if (sceneName == "HowToPlay") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_HTP_SCENE);
				} else if (sceneName == "Missions") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_MISSIONS_SCENE);
				} else if (sceneName == "Levels") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_LEVELS_SCENE);
				} else if (sceneName == "Game") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_GAME_SCENE);
				} else if (sceneName == "About") {
						AdsManager.instance.ShowAdvertisment (AdsManager.AdEvent.Event.ON_LOAD_ABOUT_SCENE);
				}
		}

		void OnDestroy(){
			AdsManager.instance.HideAdvertisment ();
		}
}
