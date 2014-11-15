using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Laser m_LaserPrefab;

    public SpriteRenderer m_playerSpriteRenderer;
    public float m_playerSpeed = 0.1f;
    public Vector3 m_direction = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);

	// Use this for initialization
	void Start () {
	    m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        Vector2 direction2D = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        direction2D.Normalize();
        m_direction = new Vector3(direction2D.x, direction2D.y, 0);
        transform.position = transform.position + m_direction * m_playerSpeed;

        float ax = Input.GetAxis("AimX");
        float ay = Input.GetAxis("AimY");
        if ((ax != 0) || (ay != 0)) {
            Vector2 dir2 = new Vector2(ax, ay);
            dir2.Normalize();
            m_AimDirection = new Vector3(dir2.x, dir2.y, 0.0f);
        }
        float angle = Vector3.Angle(m_AimDirection, new Vector3(1.0f, 0.0f, 0.0f));
        if (m_AimDirection.y < 0.0f) {
            angle = 360.0f - angle;
        }
        transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
        if (Input.GetAxis("Fire1") == 1.0f) {
            shoot();
        }
    }

    void shoot() {
        Laser laser = (Laser)Instantiate(m_LaserPrefab, transform.position, new Quaternion());
        laser.m_direction = m_AimDirection;
    }
}
