using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class AdMob : MonoBehaviour
{
		private BannerView bannerView;
		private InterstitialAd interstitial;
		private RewardBasedVideoAd rewardBasedVideo;
		public string androidBannerAdUnitID;
		public string IOSBannerAdUnitID;
		public string androidInterstitialAdUnitID;
		public string IOSInterstitialAdUnitID;
		public string androidRewardBasedVideoAdUnitID;
		public string IOSRewardBasedVideoAdUnitID;
	
		public void RequestBanner (AdPosition adPostion)
		{
				#if UNITY_EDITOR
				string adUnitId = "unused";
				#elif UNITY_ANDROID
				string adUnitId = androidBannerAdUnitID;
				#elif UNITY_IPHONE
				string adUnitId = IOSBannerAdUnitID;
				#else
				string adUnitId = "unexpected_platform";
				#endif

				// Create a 320x50 banner at the top of the screen.
				bannerView = new BannerView (adUnitId, AdSize.Banner, adPostion);
				// Create an empty ad request.
				AdRequest request = new AdRequest.Builder ().Build ();
				// Load the banner with the request.
				bannerView.LoadAd (request);
		}
	
		public void RequestInterstitial ()
		{
				#if UNITY_EDITOR
				string adUnitId = "unused";
				#elif UNITY_ANDROID
				string adUnitId = androidInterstitialAdUnitID;
				#elif UNITY_IPHONE
				string adUnitId = IOSInterstitialAdUnitID;
				#else
				string adUnitId = "unexpected_platform";
				#endif
		
				// Initialize an InterstitialAd.
				interstitial = new InterstitialAd (adUnitId);
				// Create an empty ad request.
				AdRequest request = new AdRequest.Builder ().Build ();
				// Load the interstitial with the request.
				interstitial.LoadAd (request);
		}
	
		// Returns an ad request with custom ad targeting.
		private AdRequest createAdRequest ()
		{
				return new AdRequest.Builder ()
				.AddTestDevice (AdRequest.TestDeviceSimulator) // Simulator.
				.AddTestDevice ("DeviceID") // Your device ID.
				.Build ();
		}
	
		public void RequestRewardBasedVideo ()
		{
				#if UNITY_EDITOR
				string adUnitId = "unused";
				#elif UNITY_ANDROID
				string adUnitId = androidRewardBasedVideoAdUnitID;
				#elif UNITY_IPHONE
				string adUnitId = IOSRewardBasedVideoAdUnitID;
				#else
				string adUnitId = "unexpected_platform";
				#endif
		
				rewardBasedVideo = RewardBasedVideoAd.Instance;
				rewardBasedVideo.OnAdRewarded += OnAdRewarded;
				AdRequest request = new AdRequest.Builder ().Build ();
				rewardBasedVideo.LoadAd (request, adUnitId);
		}

		public void OnAdRewarded (object sender, Reward args)
		{
			//OnAdRewarded callback
		}

		public void ShowInterstitial ()
		{
				StartCoroutine ("ShowInterstitialCoroutine");
		}

		private IEnumerator ShowInterstitialCoroutine ()
		{
				if (interstitial == null) {
						yield return 0;
				}
		
				while (!interstitial.IsLoaded ()) {
						yield return new WaitForSeconds (0.1f);
				} 
				interstitial.Show ();
		}

		public void ShowRewardBasedVideo ()
		{
				StartCoroutine ("ShowRewardBasedVideoCoroutine");
		}

		private IEnumerator ShowRewardBasedVideoCoroutine ()
		{
				if (rewardBasedVideo == null) {
						yield return 0;
				}
		
				while (!rewardBasedVideo.IsLoaded ()) {
						yield return new WaitForSeconds (0.1f);
				} 
				rewardBasedVideo.Show ();
		}

		public void ShowBanner ()
		{
				if (bannerView == null) {
						return;
				}
				bannerView.Show ();
		}

		public void HideBanner ()
		{
				if (bannerView == null) {
						return;
				}

				bannerView.Hide ();
		}

		public void DestroyBanner ()
		{
				if (bannerView == null) {
						return;
				}
				bannerView.Destroy ();
		}

		public void DestroyInterstitial ()
		{
				if (interstitial == null) {
						return;
				}
				interstitial.Destroy ();
		}
}