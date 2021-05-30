using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PickUpSpawner : MonoBehaviour
{
    [FormerlySerializedAs("pickup")] public GameObject m_pickup;

    [FormerlySerializedAs("spawnedPickups")] public List<GameObject> m_spawnedPickups;

    private float m_timer = 10.0f;

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;

        if(m_timer <= 0.0f)
        {
            //Update this for our area size
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-50, 51), 2.0f, transform.position.z + Random.Range(-50, 51));
            GameObject newclone = Instantiate(m_pickup, pos, Quaternion.Euler(0, 0, 0));
            m_spawnedPickups.Add(newclone);
            m_timer = 10.0f;
        }
    }

    public void DeletePickups()
    {
        for(int i = 0; i < m_spawnedPickups.Count; i++)
        {
            Destroy(m_spawnedPickups[i]);
        }

        m_spawnedPickups.Clear();
    }
}
