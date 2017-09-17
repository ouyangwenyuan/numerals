using UnityEngine;
using System.Collections;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class AudioSources : MonoBehaviour {

	/// <summary>
	/// The loading canvas instance.
	/// </summary>
	private static AudioSources audioSourcesInstance;
	
	// Use this for initialization
	void Awake ()
	{
		if (audioSourcesInstance == null) {
			audioSourcesInstance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
