using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		//プレイヤーの回転に合わせて回転.
		this.transform.rotation = Quaternion.Euler (20, player.transform.localEulerAngles.y, 0);
	}

	public void SetPosition(Vector3 pos){
		transform.localPosition = Vector3.Lerp(transform.localPosition, pos, Time.deltaTime * 3.0f);
	}

	public void SetRotation(Quaternion rot){
		transform.localRotation = Quaternion.Lerp (transform.localRotation, rot, Time.deltaTime * 3.0f);
	}

}
