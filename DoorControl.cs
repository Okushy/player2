using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {
	public GameObject door_left, door_right;
	public static bool door_open;
	private bool door_sound;
	// Use this for initialization
	void Start () {
		door_sound = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (door_open) {
			if(door_left.transform.localPosition.z > 20)
				door_left.transform.Translate (new Vector3(1.0f*Time.deltaTime,0.0f,0.0f));
			if(door_right.transform.localPosition.z < 33.4)
				door_right.transform.Translate (new Vector3(-1.0f*Time.deltaTime,0.0f,0.0f));
			if (door_sound) {
				GetComponent<AudioSource> ().Play ();
				door_sound = false;
			}
		}
	}
}
