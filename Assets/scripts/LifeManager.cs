using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public delegate void DeathCallback();

    public float m_maxLife = 100.0f;
    private float m_currentLife = 0.0f;

	public event DeathCallback OnDeath;

	// Use this for initialization
	void Start () {
        m_currentLife = m_maxLife;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void DoDamage(float damage) {
        float oldLife = m_currentLife;
        m_currentLife = Mathf.Max(0.0f, m_currentLife - damage);
        if ((oldLife > 0.0f) && (m_currentLife == 0.0f)) {
			if (OnDeath != null) {
				OnDeath();
            }
        }
    }

    public bool IsDead() {
        return m_currentLife <= 0.0f;
    }

}
