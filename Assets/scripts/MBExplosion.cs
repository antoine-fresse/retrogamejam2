using UnityEngine;
using System.Collections;

public class MBExplosion : MonoBehaviour {


	public int count = 5;
	public float interval = 0.25f;

    public Vector3 m_Direction;
    private Vector3 m_CrossDirection;

	public ExplosionCluster prefabExplosion;


	// Use this for initialization
	void Start () {
		StartCoroutine(Explosion());

        m_CrossDirection = new Vector3(m_Direction.y, -m_Direction.x, 0.0f);
        m_CrossDirection.Normalize();

        m_Direction.Normalize();
	}

	IEnumerator Explosion() {

		for (int i = 0; i < count; i++) {

            var exp = Instantiate(prefabExplosion, transform.position + (m_Direction * i) + m_CrossDirection, Quaternion.identity) as ExplosionCluster;

			exp.interval = 0.05f;
			exp.count = 3;
			exp.radius = 0.5f;

            exp = Instantiate(prefabExplosion, transform.position + (m_Direction * i) - m_CrossDirection, Quaternion.identity) as ExplosionCluster;

			exp.interval = 0.05f;
			exp.count = 3;
			exp.radius = 0.5f;

			yield return new WaitForSeconds(interval);
		}
	}
}
