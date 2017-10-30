using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackCheck : MonoBehaviour {
	private AudioSource damageaudio;
	private Animator playerAnimator;
	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = this.GetComponents<AudioSource> ();
		damageaudio = audioSources [2];
		playerAnimator = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		GameObject other_go = other.gameObject; 
		if (other_go.CompareTag ("Enemy")) {
			FadeControl.sceneLoad = true;
			damageaudio.PlayOneShot (damageaudio.clip);
			playerAnimator.SetBool ("damage", true);
			RebornScript.isDead = true;
		}
	}
}
