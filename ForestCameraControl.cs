using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestCameraControl : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	// Update is called once per frame
	void Update () {
		//プレイヤーの回転に合わせて回転.
		this.transform.rotation = Quaternion.Euler (60, player.transform.localEulerAngles.y, 0);
	}
}
