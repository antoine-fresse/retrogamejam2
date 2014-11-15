using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public static Camera instance;
	public float shake = 0.0f;

	private Vector3 position;


	// Use this for initialization
	void Awake () {
		instance = this;
		position = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float t = Time.time;
		shake = Mathf.Max(shake - shake*0.5f, 0.0f);

		transform.position = position + new Vector3(Mathf.Cos(20 * t) * shake * 0.1f, Mathf.Cos(50 * t + 3) * shake * 0.1f, 0.0f);
	}
}
