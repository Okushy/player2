using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
	[SerializeField]private float destroyTime = 0.1f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
