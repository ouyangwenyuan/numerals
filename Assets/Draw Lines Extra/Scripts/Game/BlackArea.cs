﻿using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class BlackArea : MonoBehaviour
{
		/// <summary>
		/// Black area animator.
		/// </summary>
		private static Animator blackAreaAnimator;

		// Use this for initialization
		void Awake ()
		{
				///Setting up the references
				if (blackAreaAnimator == null) {
						blackAreaAnimator = GetComponent<Animator> ();
				}
		}

		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				///Hide the Black Area
				Hide ();
		}

		///Show the Black Area
		public static void Show ()
		{
				if (blackAreaAnimator == null) {
						return;
				}
				blackAreaAnimator.SetTrigger ("Running");
		}
		///Hide the Black Area
		public static void Hide ()
		{
			if(blackAreaAnimator!=null)
				blackAreaAnimator.SetBool ("Running", false);
		}
}