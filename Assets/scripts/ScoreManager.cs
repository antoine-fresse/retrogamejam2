using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public delegate void ScoreChangeCallback();

    public int m_ExplosionReward = 1;
    public int m_DesintegrationReward = 1;
    public int m_DefaultReward = 1;

    private int m_Score = 0;

    public event ScoreChangeCallback OnScoreChange;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void addScore(int s) {
        m_Score = m_Score + s;
        if (OnScoreChange != null) {
            OnScoreChange();
        }
    }

    public int getScore() {
        return m_Score;
    }
}
