using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

[DisallowMultipleComponent]
public class Destroy : MonoBehaviour
{
		/// <summary>
		/// Destroy time.
		/// </summary>
		public float time;

		// Use this for initialization
		void Start ()
		{
				///Destry the current gameobject
				Destroy (gameObject, time);
		}
}
