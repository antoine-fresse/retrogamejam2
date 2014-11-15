using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public float m_speed = 0.1f;
    public Vector3 m_direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = transform.position + m_direction * m_speed;
	}

    void hitSomething() {
        //TODO
    }
}
