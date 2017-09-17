using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved

public class SceneLoader  {

	/// <summary>
	/// Loads the scene Async.
	/// </summary>
	public static IEnumerator LoadSceneAsync (string sceneName)
	{
		if (!string.IsNullOrEmpty (sceneName)) {
			#if UNITY_PRO_LICENSE
			AsyncOperation async = SceneManager.LoadSceneAsync (sceneName);
			while (!async.isDone) {
				yield return 0;
			}
			#else
			SceneManager.LoadScene (sceneName);
			yield return 0;
			#endif
		}
	}
}
