using UnityEngine;
using System.Collections;

public class RandomMBExplosion : MonoBehaviour {

    public float m_minDelay = 10.0f;
    public float m_maxDelay = 20.0f;

    public float m_warningDelay = 1.0f;

    private float m_nextShot;

    public MBExplosion m_mbxplosion;
    public ExplosionTooltip m_rightTooltip;
    public ExplosionTooltip m_leftTooltip;
    public ExplosionTooltip m_topTooltip;
    public ExplosionTooltip m_bottomTooltip;

    private GameObject m_player;

	// Use this for initialization
	void Start () {
        m_player = GameObject.FindGameObjectWithTag("Player");
        m_nextShot = Random.value * (m_maxDelay - m_minDelay) + m_minDelay;
        StartCoroutine(warning());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator warning() {
        yield return new WaitForSeconds(m_nextShot);

        int side = Mathf.RoundToInt(Random.value * 3.0f);
        if (side == 0) { // droite
            Instantiate(m_rightTooltip, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            StartCoroutine(shoot(side));
        } else if (side == 1) { // gauche
            Instantiate(m_leftTooltip, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            StartCoroutine(shoot(side));
        } else if (side == 2) { // haut
            Instantiate(m_topTooltip, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            StartCoroutine(shoot(side));
        } else if (side == 3) { // bas
            Instantiate(m_bottomTooltip, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            StartCoroutine(shoot(side));
        }
    }

    IEnumerator shoot(int side) {
        yield return new WaitForSeconds(m_warningDelay);

        Vector3 playerPos = Vector3.zero;
        if (m_player != null) {
            playerPos = m_player.transform.position;
        }

        if (side == 0) { // droite
            MBExplosion expl = Instantiate(m_mbxplosion, playerPos + new Vector3(4.0f, 0.0f, 0.0f), Quaternion.identity) as MBExplosion;
            expl.m_Direction = new Vector3(-1.0f, 0.0f, 0.0f);
        } else if (side == 1) { // gauche
            MBExplosion expl = Instantiate(m_mbxplosion, playerPos + new Vector3(-4.0f, 0.0f, 0.0f), Quaternion.identity) as MBExplosion;
            expl.m_Direction = new Vector3(1.0f, 0.0f, 0.0f);
        } else if (side == 2) { // haut
            MBExplosion expl = Instantiate(m_mbxplosion, playerPos + new Vector3(0.0f, 4.0f, 0.0f), Quaternion.identity) as MBExplosion;
            expl.m_Direction = new Vector3(0.0f, -1.0f, 0.0f);
        } else if (side == 3) { // bas
            MBExplosion expl = Instantiate(m_mbxplosion, playerPos + new Vector3(0.0f, -4.0f, 0.0f), Quaternion.identity) as MBExplosion;
            expl.m_Direction = new Vector3(0.0f, 1.0f, 0.0f);
        }
    }
}
