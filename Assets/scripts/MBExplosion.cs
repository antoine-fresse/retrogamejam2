using UnityEngine;
using System.Collections;

public class MBExplosion : MonoBehaviour {


	public int count = 5;
	public float interval = 0.25f;

	public ExplosionCluster prefabExplosion;


	// Use this for initialization
	void Start () {
		StartCoroutine(Explosion());
	}

	IEnumerator Explosion() {

		for (int i = 0; i < count; i++) {

			var exp = Instantiate(prefabExplosion, transform.position + new Vector3(-1,i,0), Quaternion.identity) as ExplosionCluster;

			exp.interval = 0.05f;
			exp.count = 3;
			exp.radius = 0.5f;

			exp = Instantiate(prefabExplosion, transform.position + new Vector3(1, i, 0), Quaternion.identity) as ExplosionCluster;

			exp.interval = 0.05f;
			exp.count = 3;
			exp.radius = 0.5f;

			yield return new WaitForSeconds(interval);
		}
	}
}
