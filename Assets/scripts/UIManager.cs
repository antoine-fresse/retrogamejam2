using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {


	public Text textHP;
	public Text textScore;

	public ScoreManager score;
	public LifeManager life;

	
	
	// Update is called once per frame
	void FixedUpdate () {
		textHP.text = "HP " + (int)life.getLife();
		textScore.text = "Score " + score.getScore();
	}
}
