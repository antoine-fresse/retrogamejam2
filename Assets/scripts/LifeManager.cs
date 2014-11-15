using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public float m_maxLife = 100.0f;
    private float m_currentLife = 0.0f;

	// Use this for initialization
	void Start () {
        m_currentLife = m_maxLife;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void DoDamage(float damage) {
        m_currentLife = Mathf.Max(0.0f, m_currentLife - damage);
    }

    public bool IsDead() {
        return m_currentLife <= 0.0f;
    }
}
