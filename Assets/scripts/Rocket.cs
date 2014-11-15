using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {


	public float accel = 2.0f;
	private float speed = 0.0f;
	public float fuse = 2.0f;


	public ExplosionCluster prefabExplosion;

	public Vector3 direction = Vector3.up;

	// Use this for initialization
	void Start () {
		StartCoroutine(Explode());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		speed += accel * Time.deltaTime;
		transform.position = transform.position + direction*speed*Time.deltaTime;
	}

	IEnumerator Explode() {
		yield return new WaitForSeconds(fuse);

		ExplosionCluster exp = Instantiate(prefabExplosion, transform.position, Quaternion.identity) as ExplosionCluster;

		exp.count = 5;
		exp.delay = 0.0f;
		exp.interval = 0.05f;
		exp.radius = 0.7f;

		Destroy(gameObject);
	}
}
