using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class AudioSourceSlider : MonoBehaviour
{
		/// <summary>
		/// The name of the audio source holder.
		/// </summary>
		public string audioSourceHolderName;

		/// <summary>
		/// The slider component reference.
		/// </summary>
		public Slider slider;
	
		/// <summary>
		/// The volume percentage text
		/// </summary>
		public Text volumePercentageText;

		[Range(0,1)]
		/// <summary>
		/// The index of the audio source.
		/// </summary>
		public int audioSourceIndex;

		// Use this for initialization
		void Start ()
		{
				if (string.IsNullOrEmpty (audioSourceHolderName)) {
						return;
				}

				if (slider == null) {
						slider = GetComponent<Slider> ();
				}

				///Find the audio source holder
				GameObject auidoSourceHolder = GameObject.Find (audioSourceHolderName);
				if (auidoSourceHolder != null) {
						//Get the slider reference
						if (slider != null) {
								AudioSource [] audioSources = auidoSourceHolder.GetComponents<AudioSource> ();
								///Apply the volume of the audio source on the slider
								if (audioSourceIndex >= 0 && audioSourceIndex < audioSources.Length) {
										SetSliderValue (audioSources [audioSourceIndex].volume);
								}
						}
				} else {
						Debug.Log ("AudioSources holder is not found");
				}
		}

		/// <summary>
		/// Set the slider value.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetSliderValue (float value)
		{
				if (slider == null) {
						return;
				}
				slider.value = value;
		}

		/// <summary>
		/// Set the volume percentage text.
		/// </summary>
		/// <param name="value">Value.</param>
		public void SetVolumePercentageText (float value)
		{
				if (volumePercentageText == null) {
						return;
				}
				volumePercentageText.text = "(" + ((int)(value * 100)) + " %)";
		}
}