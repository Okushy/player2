using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAudioControl : MonoBehaviour {
	public static bool is_startScene;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
		if (is_startScene)
			Destroy (this.gameObject);
	}
}
