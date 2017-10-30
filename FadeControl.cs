using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeControl : MonoBehaviour {
	float alfa;
	public float fadespeed = 0.7f;
	float red,green,blue;
	public static bool sceneLoad = false;

	// Use this for initialization
	void Start () {
		red = GetComponent<Image> ().color.r;
		green = GetComponent<Image> ().color.g;
		blue = GetComponent<Image> ().color.b;
		alfa = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Image>().color = new Color (red, green, blue, alfa);
		if (sceneLoad) {
			if (alfa <= 1.0f) {
				alfa += fadespeed * Time.deltaTime;
			} else {
				switch(SceneControl.now_place){
				case SceneControl.PLACE.MAP:
					SceneManager.LoadScene ("MapScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.TOWN:
					SceneManager.LoadScene ("TownScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.FACTORY:
					SceneManager.LoadScene ("FactoryScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.FOREST:
					SceneManager.LoadScene ("ForestScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.VALLEY:
					SceneManager.LoadScene ("ValleyScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.ROOM:
					SceneManager.LoadScene ("RoomScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.REAL: 
					SceneManager.LoadScene("RealScene");
					sceneLoad = false;
					break;
				case SceneControl.PLACE.END:
					SceneManager.LoadScene ("EndingScene");
					sceneLoad = false;
					break;
				}
			}
		} else {
			if(alfa >= 0.0f)
			alfa -= fadespeed*Time.deltaTime;
		}
	}


}
