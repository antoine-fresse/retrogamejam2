using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public float m_speed = 0.25f;
    public float m_damage = 1.0f;
    public Vector3 m_direction;

    private GameObject m_spawner;

    public int m_LifeTime = 100;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = transform.position + m_direction * m_speed;
        m_LifeTime--;
        if (m_LifeTime <= 0) {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D hit) {
        if ((hit.gameObject != m_spawner) && (!hit.gameObject.name.Equals("Laser(Clone)"))) {
            LifeManager manager = hit.gameObject.GetComponent<LifeManager>();
            if (manager != null) {
                manager.DoDamage(m_damage);
            }
            Destroy(gameObject);
        }
    }

    public void SetSpawner(GameObject spawner) {
        m_spawner = spawner;
    }
}
