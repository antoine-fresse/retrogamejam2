using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public Laser m_LaserPrefab;

    public float m_playerSpeed = 0.1f;
    public Vector3 m_direction = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);

    private Animator m_animator;
    private GameObject m_LaserSpawner;

    public float m_ShotPerSecond = 100.0f;
    private float m_lastShoot;

    public bool m_CanShotAndWalk = false;
    private bool m_IsShooting = false;

    private Rigidbody2D m_body;

    private LifeManager m_lifeManager;

	// Use this for initialization
	void Start () {
        m_lastShoot = 0.0f;
	    m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
        m_animator = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();

        m_LaserSpawner = transform.Find("LaserSpawner").gameObject;
        m_lifeManager = GetComponent<LifeManager>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate() {
        if (!m_lifeManager.IsDead()) {
            m_lastShoot = m_lastShoot + Time.deltaTime;

            Vector2 direction2D = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (((direction2D.x != 0) || (direction2D.y != 0)) && (m_CanShotAndWalk || (!m_IsShooting))) {
                direction2D.Normalize();
                m_direction = new Vector3(direction2D.x, direction2D.y, 0);
                m_body.velocity = Vector2.zero;
                m_body.AddForce(new Vector2(direction2D.x * m_playerSpeed, direction2D.y * m_playerSpeed));

                m_animator.SetBool("walking", true);
            } else {
                m_body.velocity = Vector2.zero;
                m_animator.SetBool("walking", false);
            }

            float ax = Input.GetAxis("AimX");
            float ay = Input.GetAxis("AimY");
            if ((ax != 0) || (ay != 0)) {
                Vector2 dir2 = new Vector2(ax, ay);
                dir2.Normalize();
                m_AimDirection = new Vector3(dir2.x, dir2.y, 0.0f);
            }
            float angle = Vector3.Angle(m_AimDirection, new Vector3(0.0f, 1.0f, 0.0f));
            if (m_AimDirection.x > 0.0f) {
                angle = 360.0f - angle;
            }
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, angle);
            if (Input.GetAxis("Fire1") == 1.0f) {
                m_animator.SetBool("firingLaser", true);
                m_IsShooting = true;
                if (canShoot()) {
                    shot();
                }
            } else {
                m_IsShooting = false;
                m_animator.SetBool("firingLaser", false);
            }
        } else {
            m_body.velocity = Vector2.zero;
            m_animator.SetBool("walking", false);
            m_animator.SetBool("firingLaser", false);
        }
    }

    void shot() {
        m_lastShoot = 0.0f;
        Vector3 position = transform.position;
        if (m_LaserSpawner) {
            position = m_LaserSpawner.transform.position;
        }
        Laser laser = (Laser)Instantiate(m_LaserPrefab, position, gameObject.transform.rotation);
        laser.m_direction = m_AimDirection;
        laser.SetSpawner(this.gameObject);
    }

    bool canShoot() {
        return m_lastShoot > (1.0f / m_ShotPerSecond);
    }

    void OnTriggerEnter2D(Collider2D hit) {
        if (!hit.gameObject.name.Equals("Agent")) {
            LifeManager manager = hit.gameObject.GetComponent<LifeManager>();
            if (manager != null) {
                //manager.DoDamage(m_damage);
            }
        }
    }
}
