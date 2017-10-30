using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {
	[SerializeField]
	private Text uiText;	//Textへの参照.
	[SerializeField]
	public GameObject panel;	//Panaelを格納.
	[SerializeField]
	private Button speakbutton;	//話すボタンを参照.
	[SerializeField]
	private Button searchbutton;	//調べるボタンを参照.
	[SerializeField]
	private Button YESbutton,NObutton;
	[SerializeField]
	private GameObject keypad;
	private static bool friend_finEvent,x_finEvent,player1_finEvent;	//イベントが初回の終わったか.
	public string[] strArray_f1,strArray_f2,strArray_f3,strArray_xf,strArray_xs,strArray_pf, strArray_ps; 
	public string[] strArray_o1, strArray_o2, strArray_o3, strArray_o4, strArray_keypad,strArray_o5;
	private PlayerControl pc;
	[SerializeField][Range(0.001f,0.3f)]
	float intervalForCharacterDisplay = 0.05f;	//1文字の表示にかかる時間.
	private bool finKeypad;
	private bool finXEvent;
	private bool press_yes;
	private SceneControl sc;

	private enum STEP	//会話時のステータス.
	{
		NONE = -1,
		Friend1 = 0,
		Friend2,
		Friend3,
		XFirst,
		XSecond,
		Player1First,
		Player1Second,
		Object1,
		Object2,
		Object3,
		Object4,
		Object5,
		Keypad,
		KeypadFin,
		NUM,
	};
	STEP now_step = STEP.NONE;

	private int currentLine = 0;	//現在の行数.
	private string currentText = string.Empty;	//現在の文字列.
	private float timeUntilDisplay = 0;	//表示にかかる時間.
	private float timeElapsed = 1;	//文字列の表示を開始した時間.
	private int lastUpdateCharacter = -1;	//表示中の文字数.

	// Use this for initialization
	void Start () {
		close_display();
		pc = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl> ();
		sc = GameObject.FindGameObjectWithTag("ViewRoot").GetComponent<SceneControl> ();
		currentLine = 0;
	}
	
	// Update is called once per frame
	void Update () {
			//update_display ();

		if (IsCompleteDisplayText) {
			switch (now_step) {	//ボタンが押されたら次のセリフ。最後のセリフがおわったらパネルを閉じる.
			case STEP.NONE:
				break;
			case STEP.Friend1:
				update_display (strArray_f1);
				break;
			case STEP.Friend2:
				update_display (strArray_f2);
				break;
			case STEP.Friend3:
				update_display (strArray_f3);
				break;
			case STEP.XFirst:
				if (currentLine < strArray_xf.Length && Input.GetMouseButtonDown (0)) {
					setString (strArray_xf [currentLine]);
				}
				if (currentLine == strArray_xf.Length && Input.GetMouseButtonDown (0)) {
					close_display ();
					sc.ChangeScene (SceneControl.PLACE.END);
				}
				break;
			case STEP.XSecond:
				if (currentLine < 5 && Input.GetMouseButtonDown (0)) {
					setString (strArray_xs [currentLine]);
				} else if (currentLine == 5 && Input.GetMouseButtonDown (0)) {
					panel.gameObject.SetActive (false);
					YESbutton.gameObject.SetActive (true);
					NObutton.gameObject.SetActive (true);
				}
				if (currentLine > 5 && Input.GetMouseButtonDown (0) && finXEvent) {
					close_display ();
					press_yes = false;
					finXEvent = false;
				}
				break;
			case STEP.Player1First:
				update_display (strArray_pf);
				break;
			case STEP.Player1Second:
				update_display (strArray_ps);
				break;
			case STEP.Object1:
				update_display (strArray_o1);
				break;
			case STEP.Object2:
				update_display (strArray_o2);
				break;
			case STEP.Object3:
				update_display (strArray_o3);
				break;
			case STEP.Object4:
				update_display (strArray_o4);
				break;
			case STEP.Object5:
				update_display (strArray_o5);
				break;
			case STEP.Keypad:
				if (currentLine < 2 && Input.GetMouseButtonDown (0)) {
					setString (strArray_keypad [currentLine]);
				} else if (currentLine == 2 && Input.GetMouseButtonDown (0)) {
					panel.gameObject.SetActive (false);
					keypad.gameObject.SetActive (true);
				}
				if (currentLine  >2 && Input.GetMouseButtonDown (0) && finKeypad == true) {
					close_display ();
				}
				break;
			}
		} else {
			if (Input.GetMouseButtonDown (0))
				timeUntilDisplay = 0;
		}

		int n = (int)(Mathf.Clamp01 ((Time.time - timeElapsed) / timeUntilDisplay) *
			currentText.Length);	//経過時間の%分の文字数.
		int displayCharacterCount = 0;
		if (n > 0)
			displayCharacterCount = n;
			

		if (displayCharacterCount != lastUpdateCharacter) {	//文字数が更新されてたら.
			uiText.text = currentText.Substring (0, displayCharacterCount);//表示文字数を更新.
			lastUpdateCharacter = displayCharacterCount;	//現在の文字数を保管.
		}
	}

	void update_display(string[] strArray){
		if (currentLine < strArray.Length && Input.GetMouseButtonDown (0)) {
			setString (strArray [currentLine]);
		} else if (currentLine == strArray.Length && Input.GetMouseButtonDown (0)) {
			close_display ();
		}
	}

	void set_display(){	//パネルを表示.
		panel.gameObject.SetActive (true);
		speakbutton.interactable = false;
		searchbutton.interactable = false;
	}

	void setString(string str){	//セリフを次に変更.
		currentText = str;
		currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;//文字列の表示にかかる時間を保管.
		timeElapsed = Time.time;	//現在の時間を保管.

		lastUpdateCharacter = -1;
	}

	void close_display(){	//パネルを非表示.
		panel.gameObject.SetActive (false);
		speakbutton.interactable = true;
		searchbutton.interactable = true;
	}

	public void On_Speak_Button(){	//話すボタンが押されたら、
		switch (pc.closest) {	//近くの人が誰か,初回かどうかによってイベントを発生させる.
		case PlayerControl.CLOSEST.NONE:
			break;
		case PlayerControl.CLOSEST.FRIEND1:
			friend1Event ();
			break;
		case PlayerControl.CLOSEST.FRIEND2:
				friend2Event ();
			break;
		case PlayerControl.CLOSEST.FRIEND3:
			friend3Event ();
			break;
		case PlayerControl.CLOSEST.X:
			if (!x_finEvent) {
				xFirstEvent ();
				x_finEvent = true;
			} else {
				xSecondEvent ();
			}
			break;
		case PlayerControl.CLOSEST.PLAYER1:
			if (!player1_finEvent) {
				player1FirstEvent ();
				player1_finEvent = true;
			} else {
				player1SecondEvent ();
			}
			break;
		}
	}

	public void On_Search_Button(){
		set_display ();
		currentLine = 0;
		switch (pc.closest) {
		case PlayerControl.CLOSEST.OBJECT1:
			now_step = STEP.Object1;
			setString (strArray_o1 [currentLine]);
			break;
		case PlayerControl.CLOSEST.OBJECT2:
			now_step = STEP.Object2;
			setString (strArray_o2 [currentLine]);
			break;
		case PlayerControl.CLOSEST.OBJECT3:
			now_step = STEP.Object3;
			setString (strArray_o3 [currentLine]);
			break;
		case PlayerControl.CLOSEST.OBJECT4:
			now_step = STEP.Object4;
			setString (strArray_o4 [currentLine]);
			break;
		case PlayerControl.CLOSEST.OBJECT5:
			now_step = STEP.Object5;
			setString (strArray_o5 [currentLine]);
			break;
		case PlayerControl.CLOSEST.KEYPAD:
			now_step = STEP.Keypad;
			setString (strArray_keypad [currentLine]);
			break;
		}
	}

	void friend1Event(){	//友達の初回イベント発生.
		set_display ();	//パネル表示.
		now_step = STEP.Friend1;	//現在の状態を保管.
		currentLine = 0;
		setString (strArray_f1 [currentLine]);	//文字列を表示.
	}

	void friend2Event(){
		set_display ();
		now_step = STEP.Friend2;
		currentLine = 0;
		setString (strArray_f2 [currentLine]);
	}

	void friend3Event(){
		set_display ();
		now_step = STEP.Friend3;
		currentLine = 0;
		setString (strArray_f3 [currentLine]);
	}

	void xFirstEvent(){
		set_display ();
		now_step = STEP.XFirst;
		currentLine = 0;
		setString (strArray_xf [currentLine]);
	}

	void xSecondEvent(){
		set_display ();
		now_step = STEP.XSecond;
		currentLine = 0;
		setString (strArray_xs[currentLine]);
	}

	void player1FirstEvent(){
		set_display ();
		now_step = STEP.Player1First;
		currentLine = 0;
		setString (strArray_pf [currentLine]);
	}

	void player1SecondEvent(){
		set_display ();
		now_step = STEP.Player1Second;
		currentLine = 0;
		setString (strArray_ps [currentLine]);
	}

	bool IsCompleteDisplayText{	//全ての文字を表示したかどうか.
		get{ return Time.time > timeElapsed + timeUntilDisplay; }
	}

	public void displayScriptKeypad(bool password_correct){
		panel.gameObject.SetActive (true);
		finKeypad = true;
		if (password_correct) {
			currentLine++;
		}
		setString (strArray_keypad [currentLine]);
	}

	void displayScriptYESNOButton(){
		panel.gameObject.SetActive (true);
		finXEvent = true;
		if (press_yes) {
			currentLine++;
			sc.ChangeScene (SceneControl.PLACE.REAL);
		}
		YESbutton.gameObject.SetActive (false);
		NObutton.gameObject.SetActive (false);
		setString (strArray_xf [currentLine]);
	}

	public void On_YesButton(){
		press_yes = true;
		finXEvent = true;
		displayScriptYESNOButton ();
	}

	public void On_NoButton(){
		press_yes = false;
		finXEvent = true;
		displayScriptYESNOButton ();
	}
}
