using UnityEngine;
using System.Collections;

public class ExplosionCluster : MonoBehaviour {

	public float delay = 0.01f;
	public float interval = 0.2f;
	public float radius = 1.0f;
	public int count = 5;
	public float damage = 0.0f;
	public float minScale = 1.0f;
	public float maxScale = 1.0f;

	public Transform prefab;

    private GameObject m_spawner;


	// Use this for initialization
	void Start () {
		StartCoroutine(Cluster());
	}

	IEnumerator Cluster() {
		yield return new WaitForSeconds(delay);

		for (int i = 0; i < count; i++ ) {
			Vector3 pos = (new Vector3(Random.value-0.5f, Random.value-0.5f, 0.0f)) * radius;
			Transform t = Instantiate(prefab, transform.position+pos, Quaternion.identity) as Transform;
            Explosion exp = t.gameObject.GetComponent<Explosion>();
            if (exp != null) {
                exp.SetSpawner(m_spawner);
            }
			float scale = Random.Range(minScale, maxScale);
			t.localScale = new Vector3(scale, scale, scale);


			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius * scale);
			foreach (Collider2D collider in colliders) {
				LifeManager manager = collider.gameObject.GetComponent<LifeManager>();
				if (manager != null) {
					manager.DoDamage(damage/count, gameObject, m_spawner);
				}
			}


			yield return new WaitForSeconds(interval);
		}

		Destroy(gameObject);

	}

    public void SetSpawner(GameObject spawner) {
        m_spawner = spawner;
    }

    public GameObject GetSpawner() {
        return m_spawner;
    }
}
