using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    public delegate void DeathCallback();

    public float m_maxLife = 100.0f;
    private float m_currentLife = 0.0f;

	public event DeathCallback OnDeath;

    public bool exploded = false;
    public bool desintegrated = false;
    public GameObject killer;

	// Use this for initialization
	void Start () {
        m_currentLife = m_maxLife;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void DoDamage(float damage, GameObject hitter, GameObject spawner) {
        float oldLife = m_currentLife;
        m_currentLife = Mathf.Max(0.0f, m_currentLife - damage);
        if ((oldLife > 0.0f) && (m_currentLife == 0.0f)) {
            if (hitter.GetComponent<Laser>() != null) {
                desintegrated = true;
            } else if ((hitter.GetComponent<Rocket>() != null) || (hitter.GetComponent<Explosion>() != null)
                || (hitter.GetComponent<ExplosionCluster>() != null)) {
                exploded = true;
            }
            killer = spawner;
			if (OnDeath != null) {
				OnDeath();
            }
        }
    }

    public bool IsDead() {
        return m_currentLife <= 0.0f;
    }

    public void SetLife(float newLife) {
        float oldLife = m_currentLife;
        m_currentLife = Mathf.Max(0.0f, newLife);
        if ((oldLife > 0.0f) && (m_currentLife == 0.0f)) {
            killer = null;
            if (OnDeath != null) {
                OnDeath();
            }
        }
    }

    public float getLife() {
        return m_currentLife;
    }

}
