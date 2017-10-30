using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotControl : MonoBehaviour {
	[SerializeField]private GameObject explosion;
	// Use this for initialization
	void Start () {
		Destroy (this.gameObject,5.0f);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += this.transform.forward * Time.deltaTime * 10;
	}

	private void OnCollisionEnter(Collision collider){
		if(collider.gameObject.CompareTag("Player")){
		FadeControl.sceneLoad = true;
			RebornScript.isDead = true;
		}else if(collider.gameObject.name == "Terrain"){
			Instantiate(explosion,this.transform.position,this.transform.rotation);
		}
		Destroy (this.gameObject);
	}
}
