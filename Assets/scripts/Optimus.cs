using UnityEngine;
using System.Collections;

public class Optimus : MonoBehaviour {
    public Laser m_LaserPrefab;
    public Rocket m_RocketPrefab;

    public float m_playerSpeed = 0.1f;
    public Vector3 m_direction = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);

    private Animator m_animator;
    private GameObject m_LaserSpawner;
    private GameObject m_RocketSpawner;

    public float m_LaserPerSecond = 100.0f;
    private float m_lastLaser;
    public float m_RocketPerSecond = 10.0f;
    private float m_lastRocket;


    public bool m_CanShotAndWalk = false;
    private bool m_IsShooting = false;

    private Player m_Player;

    public float m_DistanceToShoot = 5.0f;

    private Rigidbody2D m_body;

    private LifeManager m_lifeManager;

    public GameObject m_DeathExplosion;

    // Use this for initialization
    void Start() {
        m_lastLaser = 0.0f;
        m_lastRocket = 0.0f;

        m_direction = new Vector3(1.0f, 0.0f, 0.0f);
        m_AimDirection = new Vector3(1.0f, 0.0f, 0.0f);
        m_animator = GetComponent<Animator>();
        m_body = GetComponent<Rigidbody2D>();

        m_LaserSpawner = transform.Find("LaserSpawner").gameObject;
        m_RocketSpawner = transform.Find("RocketSpawner").gameObject;
        m_lifeManager = GetComponent<LifeManager>();

        m_Player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();

        m_lifeManager.OnDeath += () => {
            Instantiate(m_DeathExplosion, transform.position, transform.rotation);
            Destroy(GetComponent<SpriteRenderer>());
        };

        ScoreManager sm = GetComponent<ScoreManager>();
        sm.OnScoreChange += () => {
            print(sm.getScore().ToString());
        };
    }

    // Update is called once per frame
    void Update() {
    }

    void FixedUpdate() {
        if (!m_lifeManager.IsDead()) {
            if (!m_Player.getLifeManager().IsDead()) {
                m_lastLaser = m_lastLaser + Time.deltaTime;
                m_lastRocket = m_lastRocket + Time.deltaTime;

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
                    if (canShootLaser()) {
                        m_animator.SetBool("firingLaser", true);
                        m_IsShooting = true;
                        shootLaser();
                    } else if (canShootRocket()) {
                        m_animator.SetBool("firingLaser", false);
                        m_animator.SetTrigger("launchRocket");
                        StartCoroutine(shootRocket());
                    }
                } else {
                    m_IsShooting = false;
                    m_animator.SetBool("firingLaser", false);
                }
            } 
        }   else {

			

			Destroy(gameObject);
            m_body.velocity = Vector2.zero;
            m_animator.SetBool("walking", false);
            m_animator.SetBool("firingLaser", false);
        }
    }

    void shootLaser() {
        m_lastLaser = 0.0f;
        Vector3 position = transform.position;
        if (m_LaserSpawner) {
            position = m_LaserSpawner.transform.position;
        }
        Laser laser = (Laser)Instantiate(m_LaserPrefab, position, gameObject.transform.rotation);
        laser.m_direction = m_AimDirection;
        laser.SetSpawner(this.gameObject);
    }

    bool canShootLaser() {
        return m_lastLaser > (1.0f / m_LaserPerSecond);
    }
    IEnumerator shootRocket() {
        m_lastRocket = 0.0f;
        yield return new WaitForSeconds(0.57f);
        Vector3 position = transform.position;
        if (m_RocketSpawner) {
            position = m_RocketSpawner.transform.position;
        }
        Rocket rocket = (Rocket)Instantiate(m_RocketPrefab, position, gameObject.transform.rotation);
        rocket.m_direction = m_AimDirection;
        rocket.SetSpawner(this.gameObject);
    }

    bool canShootRocket() {
        return m_lastRocket > (1.0f / m_RocketPerSecond);
    }
}
