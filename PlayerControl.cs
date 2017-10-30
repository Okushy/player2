using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {
	private CharacterController playerController;
	public float MOVESPEED;
	public float ROTATESPEED;
	public float GRAVITY;
	private float move_z,move_x,move_y;
	private Animator playerAnimator;
	private float moveforward;
	[SerializeField]
	private Button speakbutton,searchbutton,YESbutton,NObutton;
	private SceneControl sc;
	private AudioSource footsteps,displayButton;

	public enum CLOSEST
	{
		NONE = -1,
		FRIEND1 = 0,
		FRIEND2,
		FRIEND3,
		X,
		PLAYER1,
		OBJECT1,
		OBJECT2,
		OBJECT3,
		OBJECT4,
		OBJECT5,
		KEYPAD,
	};
		
	public CLOSEST closest;

	// Use this for initialization
	void Start () {
		playerController = this.GetComponent<CharacterController> ();
		playerAnimator = this.GetComponent<Animator> ();
		hideSpeakButton ();
		hideSearchButton ();
		hideYESButton ();
		hideNOButton ();
		sc = GameObject.FindGameObjectWithTag("ViewRoot").GetComponent<SceneControl> ();
		AudioSource[] audioSources = this.GetComponents<AudioSource> ();
		footsteps = audioSources [0];
		displayButton = audioSources [1];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Vertical") > 0) {	//上矢印で前進量を取得.
			move_z = Input.GetAxis ("Vertical");
		} else {	//上矢印が押されてなかったら動かない.
			move_z = 0.0f;
		}
		move_x = Input.GetAxis ("Horizontal");	//回転量を取得.
		move_z *= MOVESPEED * Time.deltaTime;	//速さをかける.

		if (!playerController.isGrounded) {	//接地してなかったら落ちる.
			move_y -= GRAVITY * Time.deltaTime;
		}
		if (this.transform.position.y < -25.0f)	//落ちすぎてたら-25で止める.
			move_y = 0.0f;
		
		//相対的に移動. 
		//this.transform.Translate (new Vector3 (0, move_y, move_z));
		playerController.Move(new Vector3(0,move_y,0));
		playerController.Move (this.transform.forward * move_z);
		//相対回転.
		this.transform.Rotate(new Vector3 (0, 1, 0), move_x * Time.deltaTime * ROTATESPEED);
		playerAnimator.SetBool ("run", move_z > 0);	//前に進んでいたらrunアニメーションをtrue.

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			footsteps.Play();
		}
		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			footsteps.Stop ();
		}
	}

	void displaySpeakButton(){	//話すボタンを表示.
		speakbutton.gameObject.SetActive (true);
	}

	void hideSpeakButton(){	//話すボタンを非表示.
		speakbutton.gameObject.SetActive (false);
	}

	void displaySearchButton(){
		searchbutton.gameObject.SetActive (true);
	}

	void hideSearchButton(){
		searchbutton.gameObject.SetActive (false);
	}

	void displayYESButton(){
		YESbutton.gameObject.SetActive (true);
	}

	void displayNOButton(){
		NObutton.gameObject.SetActive (true);
	}

	void hideYESButton(){
		YESbutton.gameObject.SetActive (false);
	}

	void hideNOButton(){
		NObutton.gameObject.SetActive (false);
	}
	void OnTriggerEnter(Collider other){
		GameObject other_go = other.gameObject;
		//物のトリガーを検知.
		NPCTrigger(other_go);
		SceneTrigger (other_go);
		ObjectTrigger (other_go);
	}

	void ObjectTrigger(GameObject other_go){
		if (this.isFront (other_go)) {
			if (other_go.CompareTag ("Object1")) {
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.OBJECT1;
				displaySearchButton ();
			} else if (other_go.CompareTag ("Object2")) {
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.OBJECT2;
				displaySearchButton ();
			} else if (other_go.CompareTag ("Object3")) {
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.OBJECT3;
				displaySearchButton ();
			} else if (other_go.CompareTag ("Object4")) {
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.OBJECT4;
				displaySearchButton ();
			}else if(other_go.CompareTag("Object5")){
					displayButton.PlayOneShot (displayButton.clip);
					closest = CLOSEST.OBJECT5;
				GateControl.ClearFactory = true;
					displaySearchButton();
			}else if(other_go.CompareTag("KEYPAD")){
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.KEYPAD;
				displaySearchButton();
			}else{
				hideSearchButton();
			}
		}
	}

	void SceneTrigger(GameObject other_go){
		if (other_go.CompareTag ("MapSceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.MAP);
		} else if (other_go.CompareTag ("ForestSceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.FOREST);
		} else if (other_go.CompareTag ("FactorySceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.FACTORY);
		} else if (other_go.CompareTag ("TownSceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.TOWN);
		} else if (other_go.CompareTag ("ValleySceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.VALLEY);
		} else if (other_go.CompareTag ("RoomSceneTrigger")) {
			sc.ChangeScene (SceneControl.PLACE.ROOM);
			other_go.gameObject.GetComponent<AudioSource> ().Play ();
		} 
	}

	void NPCTrigger(GameObject other_go){
		if (this.isFront (other_go)) {
			if (other_go.CompareTag ("Friend")) {	//話すボタンを表示.
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.FRIEND1;
				displaySpeakButton ();
			}else if(other_go.CompareTag("Friend2")){
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.FRIEND2;
				displaySpeakButton ();
			}else if(other_go.CompareTag("Friend3")){
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.FRIEND3;
				displaySpeakButton ();
			} else if (other_go.CompareTag ("X")) {
				displayButton.PlayOneShot (displayButton.clip);
				closest = CLOSEST.X;
				displaySpeakButton ();
			} else if (other_go.CompareTag ("Player1")) {
				displayButton.PlayOneShot (displayButton.clip);
				GateControl.ClearForest = true;
				closest = CLOSEST.PLAYER1;
				displaySpeakButton ();
			} else {
				hideSpeakButton ();
			}
		} 
	}
		
	void OnTriggerExit(){
		hideSpeakButton ();
		hideSearchButton ();
		closest = CLOSEST.NONE;
	}

	bool isFront(GameObject go){
		bool ret = false;
		do{
			//向いてる方向を取得.
			Vector3 heading = this.transform.TransformDirection(Vector3.forward);
			//ゲームオブジェクトの方向を取得.
			Vector3 to_go = go.transform.position - this.transform.position;
			heading.y = 0.0f;
			to_go.y = 0.0f;
			heading.Normalize();
			to_go.Normalize();
			float dotProduct = Vector3.Dot(heading, to_go);
			if(dotProduct < Mathf.Cos(45.0f)){
				//２つの方向が45度以上なら抜ける.
				break;
			}
			ret = true;
		}while(false);
		return(ret);
	}
}
