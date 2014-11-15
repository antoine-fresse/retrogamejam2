using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public static Camera instance;
	public float shake = 0.0f;

	private Vector3 m_position;

    private GameObject m_Player;

	// Use this for initialization
	void Awake () {
		instance = this;
		m_position = transform.position;

        m_Player = GameObject.FindGameObjectsWithTag("Player")[0];
	}

    void Update() {
        Vector3 pos = m_Player.transform.position;
        m_position.Set(pos.x, pos.y, m_position.z);
        transform.position = m_position;
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		float t = Time.time;
		shake = Mathf.Max(shake - shake*0.5f, 0.0f);

		transform.position = m_position + new Vector3(Mathf.Cos(20 * t) * shake * 0.1f, Mathf.Cos(50 * t + 3) * shake * 0.1f, 0.0f);
	}
}
