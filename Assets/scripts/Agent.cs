using UnityEngine;
using System.Collections;

public class Agent : MonoBehaviour {
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

    private Player m_Player;

    public float m_DistanceToShoot = 5.0f;

    private Rigidbody2D m_body;

    private LifeManager m_lifeManager;
    public GameObject m_DeathExplosion;

    // Use this for initialization
    void Start() {
        m_lastShoot = 0.0f;
        m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
        m_animator = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();

        m_LaserSpawner = transform.Find("LaserSpawner").gameObject;

		m_Player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
        m_lifeManager = GetComponent<LifeManager>();

		m_lifeManager.OnDeath += () => {
            Instantiate(m_DeathExplosion, transform.position, transform.rotation);
            //Destroy(GetComponent<SpriteRenderer>());
            print("dead");
            if (m_lifeManager.exploded) {
                m_animator.SetBool("exploded", true);
            } else if (m_lifeManager.desintegrated) {
                m_animator.SetBool("desintegrated", true);
            } else {
                m_animator.SetBool("dead", true);
            }
            Destroy(GetComponent<CircleCollider2D>());
            Destroy(GetComponent<BoxCollider2D>());
			transform.localEulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        };
    }

    // Update is called once per frame
    void Update() {
    }

    void FixedUpdate() {
        if (!m_lifeManager.IsDead()) {
			if (!m_Player.m_lifeManager.IsDead()) {
				m_lastShoot = m_lastShoot + Time.deltaTime;

				Vector3 directionToPlayer = (m_Player.transform.position - transform.position);
				float dist = directionToPlayer.magnitude;
				if (dist > m_DistanceToShoot) {
					Vector2 direction2D = new Vector2(directionToPlayer.x, directionToPlayer.y);
					if (((direction2D.x != 0) || (direction2D.y != 0)) && (m_CanShotAndWalk || (!m_IsShooting))) {
						direction2D.Normalize();
						m_body.velocity = Vector2.zero;
						m_body.AddForce(new Vector2(direction2D.x * m_playerSpeed, direction2D.y * m_playerSpeed));

						m_animator.SetBool("walking", true);
					} else {
						m_body.velocity = Vector2.zero;
						m_animator.SetBool("walking", false);
					}
				} else {
					m_body.velocity = Vector2.zero;
					m_animator.SetBool("walking", false);
				}


				// Rotation

				float ax = directionToPlayer.x;
				float ay = directionToPlayer.y;
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




				if (dist <= m_DistanceToShoot) {
					m_animator.SetBool("firingLaser", true);
					m_IsShooting = true;
					if (canShoot()) {
						shot();
					}
				} else {
					m_IsShooting = false;
					m_animator.SetBool("firingLaser", false);
				}
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
        Vector3 directionToPlayer = (m_Player.transform.position - transform.position);
        float dist = directionToPlayer.magnitude;
        return (m_lastShoot > (1.0f / m_ShotPerSecond)) && (dist <= m_DistanceToShoot);
    }
}
