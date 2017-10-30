using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButtonControl : MonoBehaviour {
	float alfa;
	public float fadespeed = 0.1f;
	public static bool sceneLoad = false;
	[SerializeField]
	private GameObject fadePanel;
	float red,green,blue;
	[SerializeField]
	private GameObject startButton;
	[SerializeField]
	private GameObject text;


	// Use this for initialization
	void Start () {
		red = fadePanel.GetComponent<Image> ().color.r;
		green = fadePanel.GetComponent<Image> ().color.g;
		blue = fadePanel.GetComponent<Image> ().color.b;
		alfa = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		fadePanel.GetComponent<Image>().color = new Color (red, green, blue, alfa);
		if (sceneLoad) {
			if (alfa <= 1.0f) {
				alfa += fadespeed * Time.deltaTime;
			} else {
				SceneManager.LoadScene ("PrologueScene");
			}
		} 
	}

	public void On_Click_StartButton(){
		sceneLoad = true;
		startButton.gameObject.GetComponent<Image> ().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
		text.gameObject.GetComponent<Text> ().color = new Color (0.0f, 0.0f, 0.0f, 0.0f);
	}

}
