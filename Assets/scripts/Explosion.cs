using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    private GameObject m_spawner;

	// Use this for initialization
	void Start () {
		Camera.instance.shake += transform.localScale.x * 3.0f;
		StartCoroutine(Expire());
	}

	IEnumerator Expire() {
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}

    public void SetSpawner(GameObject spawner) {
        m_spawner = spawner;
    }

    public GameObject GetSpawner() {
        return m_spawner;
    }
}
