using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDisplay : MonoBehaviour {
	[SerializeField]private GameObject panel;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void closeDisplay(){
			panel.SetActive (false);
	}
}
