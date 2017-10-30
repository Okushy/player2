using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PrologueScript : MonoBehaviour {
	[SerializeField]
	private GameObject fadePanel;
	[SerializeField]
	private Text prologueText;
	private float red,green,blue,alfa;
	[SerializeField]
	private float ScrollSpeed = 0,ChangeSceneTime = 0;

	// Use this for initialization
	void Start () {
		red = fadePanel.GetComponent<Image> ().color.r;
		green = fadePanel.GetComponent<Image> ().color.g;
		blue = fadePanel.GetComponent<Image> ().color.b;
		alfa = 0.0f;
	}

	// Update is called once per frame
	void Update () {
		fadePanel.GetComponent<Image> ().color = new Color (red, green, blue, alfa);
		prologueText.transform.position = new Vector3(505,prologueText.transform.position.y+ScrollSpeed * Time.deltaTime,0);
		if (prologueText.transform.position.y > ChangeSceneTime) {
			if (alfa <= 1.0f) {
				alfa += 0.7f * Time.deltaTime;
			} else {
				SceneManager.LoadScene ("StartScene");
			}
		}
	}
}
