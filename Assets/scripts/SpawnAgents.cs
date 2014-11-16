using UnityEngine;
using System.Collections;

public class SpawnAgents : MonoBehaviour {

    public float m_DelayBetweenSpawn = 1.0f;
    public int m_Limit = 50;

    public Agent m_AgentToSpawn;

    private GameObject[] m_Spawners;

	// Use this for initialization
	void Start () {
        m_Spawners = GameObject.FindGameObjectsWithTag("AgentSpawner");
        StartCoroutine(spawn());
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    IEnumerator spawn() {
        yield return new WaitForSeconds(m_DelayBetweenSpawn);

        if ((GameObject.FindGameObjectsWithTag("AgentSpawner").Length < m_Limit) && (m_Spawners.Length > 0)) {
            int spawnerIndex = Mathf.RoundToInt(Random.value * (m_Spawners.Length - 1));
            Instantiate(m_AgentToSpawn, m_Spawners[spawnerIndex].transform.position, Quaternion.identity);
        }
        StartCoroutine(spawn());
    }
}
