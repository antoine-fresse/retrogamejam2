using UnityEngine;
using System.Collections;

public class ExplosionTooltip : MonoBehaviour {

    public int m_duration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator disapear() {
        yield return new WaitForSeconds(m_duration);

        Destroy(gameObject);
    }
}
