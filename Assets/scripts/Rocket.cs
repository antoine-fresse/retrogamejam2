using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {


	public float accel = 2.0f;
	private float speed = 0.0f;
	public float fuse = 2.0f;

    private GameObject m_spawner;

    public float m_damage;

	public ExplosionCluster prefabExplosion;

	public Vector3 m_direction = Vector3.up;

	// Use this for initialization
	void Start () {
        StartCoroutine(ExplodeCoroutine());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speed += accel * Time.deltaTime;
		transform.position = transform.position + m_direction*speed*Time.deltaTime;
	}

    void OnTriggerEnter2D(Collider2D hit) {
        if ((hit.gameObject != m_spawner) && (!hit.gameObject.name.Equals("Rocket(Clone)"))) {
            LifeManager manager = hit.gameObject.GetComponent<LifeManager>();
            if (manager != null) {
                manager.DoDamage(m_damage);
            }
            fuse = 0;
            Explode();
        }
    }

    IEnumerator ExplodeCoroutine() {
        yield return new WaitForSeconds(fuse);
        Explode();
    }

	void Explode() {
		ExplosionCluster exp = Instantiate(prefabExplosion, transform.position, Quaternion.identity) as ExplosionCluster;

		exp.count = 5;
		exp.delay = 0.0f;
		exp.interval = 0.05f;
		exp.radius = 0.7f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.7f);
        foreach (Collider2D collider in colliders) {
            LifeManager manager = collider.gameObject.GetComponent<LifeManager>();
            if (manager != null) {
                manager.DoDamage(m_damage);
            }
        }

		Destroy(gameObject);
	}


    public void SetSpawner(GameObject spawner) {
        m_spawner = spawner;
    }
}
