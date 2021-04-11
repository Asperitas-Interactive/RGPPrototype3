using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waveManager : MonoBehaviour
{
    public int maxWaves;
    public int maxWaveTimer;
    public GameObject enemies;

    bool restWave;
    float waveTimer;
    int wave;

  public GameObject []boids;

    // Start is called before the first frame update
    void Start()
    {

        int count = 0;
        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            for (int j = 0; j < enemies.transform.GetChild(i).GetComponent<EnemyControl>().spawnNum; j++)
            {
                count++;
            }
        }

        boids = new GameObject[count];
        wave = 1;
        waveTimer = (float)maxWaveTimer;
        waveStart();



    }

    // Update is called once per frame
    void Update()
    {
        waveTimer -= Time.deltaTime;

        if(waveTimer <= 0.0f)
        {
            if(wave > maxWaves)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameOver();
            }

            if(!restWave)
            {
                waveTimer = (float)maxWaveTimer;
                restWave = true;
                WaveEnd();
            }

            else
            {
                waveTimer = (float)maxWaveTimer;
                restWave = false;
                wave++;
                waveStart();
            }

        }

        if (!restWave)
        {
            bool flag = false;

            foreach (GameObject boid in boids)
            {
                if(boid == null)
                {
                    continue;
                }

                else
                {
                    flag = true;
                    break;
                }
            }

            if (!flag)
            {
                waveTimer = 0f;
            }

        }


        else
        {
            if (Input.GetButtonDown("Skip"))
            {
                waveTimer = 0f;
            }
        }



    }

    int waveStart()
    {
        int count = 0;
        for (int i = 0; i < enemies.transform.childCount; i++)
        { 
            for(int j = 0; j< enemies.transform.GetChild(i).GetComponent<EnemyControl>().spawnNum; j++)
            {
                
                 boids[count] = Instantiate(enemies.transform.GetChild(i), GetRandomLocation(), Quaternion.identity, null).gameObject;

                count++;
            }
        }
        return count;
    }

    void WaveEnd()
    {
        foreach(GameObject boid in boids)
        {
            if(boid!=null)
                Destroy(boid);
        }
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
