using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameOver : MonoBehaviour {

	public Text textScore;
	public Text textRecord;

	// Use this for initialization
	void Start () {
		textScore.text = "Score " + PlayerPrefs.GetInt("score");
		textRecord.text = "Record " + PlayerPrefs.GetInt("bestscore");
	}

	public void Restart() {
		Application.LoadLevel(1);
	}

	public void Quit() {
		Application.Quit();
	}
}
