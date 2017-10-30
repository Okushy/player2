using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilenControl : MonoBehaviour {
	[SerializeField]private GameObject player;
	[SerializeField]private float MOVESPEED;
	[SerializeField]private GameObject shot;
	[SerializeField]private GameObject muzzle;
	[SerializeField]
	private float shotIntervalMax = 3.0f;
	[SerializeField] private GameObject muzzleFlash;
	private Animator animator;
	AudioSource gunAudio;

	private float shotInterval = 0;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		AudioSource[] audioSources = this.GetComponents<AudioSource> ();
		gunAudio = audioSources [0];
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.LookAt (player.transform);
		if ((this.transform.position - player.transform.position).magnitude > 5.0f)
		this.transform.position += this.transform.forward * MOVESPEED * Time.deltaTime;

		shotInterval += Time.deltaTime;
		if (shotInterval > shotIntervalMax) {
			Instantiate (shot, muzzle.transform.position, muzzle.transform.rotation);
			shotInterval = 0;
			animator.SetBool ("shot", true);
			gunAudio.PlayOneShot (gunAudio.clip);
			//マズルフラッシュを表示.
			Instantiate(muzzleFlash,muzzle.transform.position,muzzle.transform.rotation);
		}
	}
}
