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

    private GameObject m_Player;

    public float m_DistanceToShoot = 5.0f;

    // Use this for initialization
    void Start() {
        m_lastShoot = 0.0f;
        m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
        m_animator = GetComponent<Animator>();

        m_LaserSpawner = transform.Find("LaserSpawner").gameObject;

        m_Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update() {
    }

    void FixedUpdate() {
        m_lastShoot = m_lastShoot + Time.deltaTime;

        Vector3 directionToPlayer = (m_Player.transform.position - transform.position);
        float dist = directionToPlayer.magnitude;
        if (dist > m_DistanceToShoot) {
            Vector2 direction2D = new Vector2(directionToPlayer.x, directionToPlayer.y);
            if (((direction2D.x != 0) || (direction2D.y != 0)) && (m_CanShotAndWalk || (!m_IsShooting))) {
                direction2D.Normalize();
                m_direction = new Vector3(direction2D.x, direction2D.y, 0);
                transform.position = transform.position + m_direction * m_playerSpeed;

                m_animator.SetBool("walking", true);
            } else {
                m_animator.SetBool("walking", false);
            }
        } else {
            m_animator.SetBool("walking", false);
        }

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
