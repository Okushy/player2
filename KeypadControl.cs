using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadControl : MonoBehaviour {
	[SerializeField]
	private  Button b0,b1,b2,b3,b4,b5,b6,b7,b8,b9,bClear,bGo;
	[SerializeField]
	public Text displayedText;
	private int[] text = new int[4];
	private int textCount;
	[SerializeField]
	private GameObject uiroot;
	private DoorControl doorcontrol;

	// Use this for initialization
	void Start () {
		this.gameObject.SetActive (false);
		text = new int[]{0,0,0,0};
		textCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		displayedText.text = text [0].ToString() + text [1].ToString() + text [2].ToString() + text [3].ToString(); 

	}

	public void On_b1(){
		onNumberButton (1);
	}

	public void On_b2(){
		onNumberButton (2);
	}

	public void On_b3(){
		onNumberButton (3);
	}

	public void On_b4(){
		onNumberButton (4);
	}

	public void On_b5(){
		onNumberButton (5);
	}

	public void On_b6(){
		onNumberButton (6);
	}

	public void On_b7(){
		onNumberButton (7);
	}

	public void On_b8(){
		onNumberButton (8);
	}

	public void On_b9(){
		onNumberButton (9);
	}

	public void On_b0(){
		onNumberButton (0);
	}

	public void On_bClear(){
		textCount = 0;
		text [0] = 0;
		text [1] = 0;
		text [2] = 0;
		text [3] = 0;
	}

	public void On_bGo(){
		bool password_correct = false;
		this.gameObject.SetActive (false);
		if (text [0] == 0 && text [1] == 9 && text [2] == 4 && text [3] == 1) {
			password_correct = true;
			DoorControl.door_open = true;
		} else {
			On_bClear ();
		}
		uiroot.gameObject.GetComponent<UIControl>().displayScriptKeypad (password_correct);
	}

	void onNumberButton(int number){
		if (textCount < 4) {
			text [textCount] = number;
			textCount++;

		} 
	}
}
