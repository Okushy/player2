using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebornScript : MonoBehaviour {
	public static bool isDead;
	private bool IsDead;
	[SerializeField]private GameObject panel;
	[SerializeField]private Text rebornText;
	[SerializeField][Range(0.001f,0.3f)]
	float intervalForCharacterDisplay = 0.08f;	//1文字の表示にかかる時間.
	private int currentLine = 0;	//現在の行数.
	private string currentText = string.Empty;	//現在の文字列.
	private float timeUntilDisplay = 0;	//表示にかかる時間.
	private float timeElapsed = 1;	//文字列の表示を開始した時間.
	private int lastUpdateCharacter = -1;	//表示中の文字数.
	public string[] rebornString;	//テキストを格納.	

	void Start () {
		if (isDead)
			IsDead = true;	//startでtrueにすることで１度だけtrueにする.
	}
	
	// Update is called once per frame
	void Update () {
		if (IsDead) {
			setString (rebornString [currentLine]);
			panel.gameObject.SetActive (true);
			IsDead = false;
			isDead = false;
		} else {
			panel.gameObject.SetActive (false);
		}

		if (IsCompleteDisplayText) {
			if (currentLine < rebornString.Length && Input.GetMouseButtonDown (0)) {
				setString (rebornString [currentLine]);
			} else if (currentLine == rebornString.Length && Input.GetMouseButtonDown (0)) {
				panel.gameObject.SetActive (false);
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
			rebornText.text = currentText.Substring (0, displayCharacterCount);//表示文字数を更新.
			lastUpdateCharacter = displayCharacterCount;	//現在の文字数を保管.
		}	
	}

	bool IsCompleteDisplayText{	//全ての文字を表示したかどうか.
		get{ return Time.time > timeElapsed + timeUntilDisplay; }
	}

	void setString(string str){	//セリフを次に変更.
		currentText = str;
		currentLine++;

		timeUntilDisplay = currentText.Length * intervalForCharacterDisplay;//文字列の表示にかかる時間を保管.
		timeElapsed = Time.time;	//現在の時間を保管.

		lastUpdateCharacter = -1;
	}
}
