using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

///Developed by Indie Games Studio
///Email : freelance.art2014@gmail.com
///www.indiegstudio.com
///copyright © 2016 IGS. All rights reserved
 
public class Logo : MonoBehaviour {

	public float sleepTime = 5;

	// Use this for initialization
	void Start () {
		Invoke ("LoadMainScene", sleepTime);
	}

	private void LoadMainScene(){
		StartCoroutine(SceneLoader.LoadSceneAsync("Main"));
	}
	
}
