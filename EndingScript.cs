using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour {
	[SerializeField][Range(0.001f,0.3f)]
	float intervalForCharacterDisplay = 0.05f;	//1文字の表示にかかる時間.
	private int currentLine = 0;	//現在の行数.
	private string currentText = string.Empty;	//現在の文字列.
	private float timeUntilDisplay = 0;	//表示にかかる時間.
	private float timeElapsed = 1;	//文字列の表示を開始した時間.
	private int lastUpdateCharacter = -1;	//表示中の文字数.
	[SerializeField]
	private Text endText;	//テキストを表示するオブジェクトを格納.
	public string[] endString;	//テキストを格納.	

	float alfa;
	public float fadespeed = 0.7f;
	float red,green,blue;
	private bool sceneLoad = false;
	[SerializeField]
	private GameObject fadePanel;
	[SerializeField] 
	private GameObject scriptZone;
	private SceneControl sc;
	// Use this for initialization
	void Start () {
		setString(endString[currentLine]);
		red = fadePanel.GetComponent<Image> ().color.r;
		green = fadePanel.GetComponent<Image> ().color.g;
		blue = fadePanel.GetComponent<Image> ().color.b;
		alfa = 1.0f;
		sc = GetComponent<SceneControl> ();
		sceneLoad = false;
	}

	// Update is called once per frame
	void Update () {
		fadePanel.GetComponent<Image>().color = new Color (red, green, blue, alfa);	//パネルを更新.
		if (IsCompleteDisplayText) {
			if (currentLine < endString.Length && Input.GetMouseButtonDown (0)) {
				setString (endString [currentLine]);
			} else if (currentLine == endString.Length && Input.GetMouseButtonDown (0)) {
				scriptZone.gameObject.SetActive (false);
				sceneLoad = true;
				red = 0.0f;
				blue = 0.0f;
				green = 0.0f;
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
			endText.text = currentText.Substring (0, displayCharacterCount);//表示文字数を更新.
			lastUpdateCharacter = displayCharacterCount;	//現在の文字数を保管.
		}	

		if (sceneLoad) {
				sc.ChangeScene(SceneControl.PLACE.ROOM);
			
		}/*else{
			if(alfa >= 0.0f)
			//	alfa -= fadespeed*Time.deltaTime;
		}*/
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
