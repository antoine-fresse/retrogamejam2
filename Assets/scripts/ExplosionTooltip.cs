using UnityEngine;
using System.Collections;

public class ExplosionTooltip : MonoBehaviour {

    public float m_duration = 2.5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(disapear());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator disapear() {
        yield return new WaitForSeconds(m_duration);

        Destroy(gameObject);
    }
}
