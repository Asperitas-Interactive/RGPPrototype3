using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waveManager : MonoBehaviour
{
    public int boidCount = 0;
    public int maxWaves;
    int wave;

    public GameObject[] boids;

    public Waves[] waveControl;

    public bool m_CombatEnded = false;

    public RankingSystem rankSys;

    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        waveStart();
        rankSys = GameObject.FindGameObjectWithTag("Player").GetComponent<RankingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (wave > maxWaves)
            {
                //Insert new way to change the Scene
                m_CombatEnded = true;
                WaveEnd();
            }

            int t = 0;
            bool flag = false;

            foreach (GameObject boid in boids)
            {
                if (boid == null)
                {
                    continue;
                }

                else
                {
                    t++;
                    flag = true;

                }
            }
            boidCount = t;

            if (!flag)
            {
                wave++;
            }
        }
    }

    int waveStart()
    {
        int count = 0;

        for (int i = 0; i < waveControl[wave].enemies.Length; i++)
        {
            count++;
        }

        boids = new GameObject[count];

        for (int i = 0; i < waveControl[wave].enemies.Length; i++)
        {
            boids[i] = Instantiate(waveControl[wave].enemies[i], GetRandomLocation(), Quaternion.identity, null).gameObject;
        }

        return count;
    }

    void WaveEnd()
    {
        //Its use was removed in mini prod
        
    }


    public Vector3 GetRandomLocation()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();

        int t = Random.Range(0, data.indices.Length - 3);

        Vector3 point = Vector3.Lerp(data.vertices[data.indices[t]], data.vertices[data.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, data.vertices[data.indices[t + 2]], Random.value);

        return point;
    }
}
