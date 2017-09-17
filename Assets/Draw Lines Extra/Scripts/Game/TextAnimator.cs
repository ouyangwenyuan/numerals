using UnityEngine;
using System.Collections;
using UnityEngine.UI;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class TextAnimator : MonoBehaviour
{
		/// <summary>
		/// The text component reference.
		/// </summary>
		public Text textComponent;
		[Range(0,1.0f)]
		/// <summary>
		/// The delay time.
		/// </summary>
		public float delayTime = 0.01f;

		/// <summary>
		/// Whether to show the text on start or not.
		/// </summary>
		public bool runOnStart = true;

		// Use this for initialization
		void Start ()
		{
				if (runOnStart) {
						ShowText ();
				}
		}

		/// <summary>
		/// Show the text.
		/// </summary>
		public void ShowText ()
		{
				if (textComponent == null) {
						return;
				}
				string text = textComponent.text;
				textComponent.text = "";
				if (string.IsNullOrEmpty (text)) {
						return;
				}
				StartCoroutine (ShowText (text));
		}

		/// <summary>
		/// Show text coroutine.
		/// </summary>
		/// <returns>The text.</returns>
		/// <param name="text">Text.</param>
		private IEnumerator ShowText (string text)
		{
				int textLength = text.Length;
	
				for (int i = 0; i < textLength; i++) {
						textComponent.text+= text [i];
						yield return new WaitForSeconds (delayTime);
				}
		}
}
