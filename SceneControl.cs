using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour {
	public enum PLACE
	{
		NONE = -1,
		START = 0,
		TOWN,
		MAP,
		FOREST,
		VALLEY,
		FACTORY,
		ROOM,
		REAL,
		END,
	};



	public static PLACE pre_place = PLACE.START;
	public static PLACE now_place = PLACE.TOWN;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		setScenePosition ();
	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void setScenePosition(){
		if (pre_place == PLACE.START && now_place == PLACE.TOWN) {
			//player.transform.position = new Vector3 (11.0f, 0.0f, -12.0f);
			player.transform.Rotate (0.0f, -90.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.MAP && now_place == PLACE.TOWN) {
			player.transform.position = new Vector3 (8.5f, 0.0f, 16.0f);
			player.transform.Rotate (0.0f, 180.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.TOWN && now_place == PLACE.MAP) {
			player.transform.position = new Vector3 (18.0f, 0.0f, 16.5f);
			player.transform.Rotate (0.0f, -90.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.VALLEY && now_place == PLACE.MAP) {
			player.transform.position = new Vector3 (2.4f, 0.0f, 30.0f);
			player.transform.Rotate (0.0f, 180.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.FOREST && now_place == PLACE.MAP) {
			player.transform.position = new Vector3 (-4.0f, 0.0f, 2.5f);
			player.transform.Rotate (0.0f, 0.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.FACTORY && now_place == PLACE.MAP) {
			player.transform.position = new Vector3 (-16.0f, 0.0f, 24.0f);
			player.transform.Rotate (0.0f, 90.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.MAP && now_place == PLACE.FACTORY) {
			player.transform.position = new Vector3 (1.0f, 0.0f, -28.0f);
			player.transform.Rotate (0.0f, 0.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.MAP && now_place == PLACE.VALLEY) {
			player.transform.position = new Vector3 (0.0f, 0.0f, -28.0f);
		} else if (pre_place == PLACE.VALLEY && now_place == PLACE.VALLEY) {
			player.transform.position = new Vector3 (0.0f, 0.0f, -28.0f);
		} else if (pre_place == PLACE.MAP && now_place == PLACE.FOREST) {
			player.transform.position = new Vector3 (-3.0f, 0.0f, -24.0f);
			player.transform.Rotate (0.0f, 0.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.FOREST && now_place == PLACE.FOREST) {
			player.transform.position = new Vector3 (-3.0f, 0.0f, -24.0f);
			player.transform.Rotate (0.0f, 0.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.VALLEY && now_place == PLACE.ROOM) {
			player.transform.position = new Vector3 (0.0f, 0.0f, -10.0f);
		} else if (pre_place == PLACE.ROOM && now_place == PLACE.VALLEY) {
			player.transform.position = new Vector3 (-16.0f, 0.0f, 30.0f);
			player.transform.Rotate (0.0f, 90.0f, 0.0f, Space.World);
		} else if (pre_place == PLACE.END && now_place == PLACE.ROOM) {
			player.transform.position = new Vector3 (0.0f, 0.0f, -1.5f);
		}
	}

	public void ChangeScene(PLACE new_place){
		switch (now_place) {
		case PLACE.TOWN:
			pre_place = PLACE.TOWN;
			break;
		case PLACE.MAP:
			pre_place = PLACE.MAP;
			break;
		case PLACE.FOREST:
			pre_place = PLACE.FOREST;
			break;
		case PLACE.FACTORY:
			pre_place = PLACE.FACTORY;
			break;
		case PLACE.VALLEY:
			pre_place = PLACE.VALLEY;
			break;
		case PLACE.ROOM:
			pre_place = PLACE.ROOM;
			break;
		case PLACE.REAL:
			pre_place = PLACE.REAL;
			break;
		case PLACE.END:
			pre_place = PLACE.END;
			break;
		}
		now_place = new_place;
		FadeControl.sceneLoad = true;
	}
}
