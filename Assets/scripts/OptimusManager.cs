using UnityEngine;
using System.Collections;

public class OptimusManager : MonoBehaviour {

	public GameObject optimus;


	// Use this for initialization
	void Start () {
		StartCoroutine(spawner());
	}

	IEnumerator spawner() {

		while (true) {
			yield return new WaitForSeconds(10f);

			if (GameObject.FindGameObjectWithTag("Optimus") == null) {
				Instantiate(optimus, Building.lastBuildingPosition, Quaternion.identity);
				yield return null;
			}

			while (GameObject.FindGameObjectWithTag("Optimus") != null)
				yield return null;
			
		}


	}
}
