using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateControl : MonoBehaviour {
	public static bool ClearForest, ClearFactory;
	[SerializeField]private GameObject gateForest, gateValley;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (ClearForest && gateValley.activeSelf)
			gateValley.SetActive (false);

		if (ClearFactory && gateForest.activeSelf)
			gateForest.SetActive (false);
	}
}
