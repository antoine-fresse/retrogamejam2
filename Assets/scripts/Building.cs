using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    private LifeManager m_LifeManager;

    public GameObject m_DeathExplosion;
    public GameObject m_ToSpawn;

	private Animator m_anim;

    private ScoreManager m_scoreManager;

	// Use this for initialization
	void Start () {
        m_LifeManager = GetComponent<LifeManager>();
		m_anim = GetComponent<Animator>();
        m_scoreManager = GetComponent<ScoreManager>();
        //m_LifeManager.setDeathCallback(delegate() {
		m_LifeManager.OnDeath += () => {
            if (m_DeathExplosion != null) {
				Instantiate(m_DeathExplosion, transform.position, transform.rotation);
            }
            if (m_ToSpawn != null) {
				Instantiate(m_ToSpawn, transform.position + new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f), transform.rotation);
            }
            //Destroy(GetComponent<SpriteRenderer>());

			foreach (Transform t in transform) {
				float scale = Random.value*0.5f + 0.5f;
				t.localScale = new Vector3(scale, scale, scale);
				t.localEulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
				t.localPosition = new Vector3((Random.value - .5f) * 0.5f, (Random.value - .5f) * 0.5f, 0.0f);
				t.gameObject.SetActive(true);
			}
			gameObject.renderer.enabled = false;

            Destroy(GetComponent<BoxCollider2D>());
            
            if (m_scoreManager != null) {
                GameObject killer = m_LifeManager.killer;
                if (killer != null) {
                    ScoreManager sm = killer.GetComponent<ScoreManager>();
                    if (m_LifeManager.exploded) {
                        sm.addScore(m_scoreManager.m_ExplosionReward);
                    } else if (m_LifeManager.desintegrated) {
                        sm.addScore(m_scoreManager.m_DesintegrationReward);
                    } else {
                        sm.addScore(m_scoreManager.m_DefaultReward);
                    }
                }
            }
			StartCoroutine(Remove());
        };
	}

	void Update() {
		m_anim.SetFloat("damage", 1.0f - (m_LifeManager.getLife()/ m_LifeManager.m_maxLife));
	}

	IEnumerator Remove() {
		yield return new WaitForSeconds(20f);
		Destroy(gameObject);
	}
}
