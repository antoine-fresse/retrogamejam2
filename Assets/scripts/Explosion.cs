using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {


	// Use this for initialization
	void Start () {
		Camera.instance.shake += transform.localScale.x * 3.0f;
		StartCoroutine(Expire());
	}

	IEnumerator Expire() {
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}
	
}
