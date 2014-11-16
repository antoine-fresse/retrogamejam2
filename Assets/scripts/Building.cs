using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

    private LifeManager m_LifeManager;

    public GameObject m_DeathExplosion;
    public GameObject m_ToSpawn;

	// Use this for initialization
	void Start () {
        m_LifeManager = GetComponent<LifeManager>();
        //m_LifeManager.setDeathCallback(delegate() {
		m_LifeManager.OnDeath += () => {
            if (m_DeathExplosion != null) {
				Instantiate(m_DeathExplosion, transform.position, transform.rotation);
            }
            if (m_ToSpawn != null) {
				Instantiate(m_ToSpawn, transform.position + new Vector3(Random.value - 0.5f, Random.value - 0.5f, Random.value - 0.5f), transform.rotation);
            }
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<BoxCollider2D>());
        };
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
