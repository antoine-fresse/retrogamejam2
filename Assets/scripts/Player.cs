using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Laser m_LaserPrefab;

    public float m_playerSpeed = 0.1f;
    public Vector3 m_direction = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);

    private Animator m_animator;
    private GameObject m_LaserSpawner;

	// Use this for initialization
	void Start () {
	    m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
        m_animator = GetComponent<Animator>();

        m_LaserSpawner = transform.Find("LaserSpawner").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        Vector2 direction2D = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        if ((direction2D.x != 0) || (direction2D.y != 0)) {
            direction2D.Normalize();
            m_direction = new Vector3(direction2D.x, direction2D.y, 0);
            transform.position = transform.position + m_direction * m_playerSpeed;

            m_animator.SetBool("walking", true);
        } else {
            m_animator.SetBool("walking", false);
        }

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
        Vector3 position = transform.position;
        if (m_LaserSpawner) {
            position = m_LaserSpawner.transform.position;
        }
        Laser laser = (Laser)Instantiate(m_LaserPrefab, position, new Quaternion());
        laser.m_direction = m_AimDirection;
        laser.SetSpawner(this.gameObject);
    }
}
