using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waveManager : MonoBehaviour
{
    public int maxWaves;
    public int maxWaveTimer;
    public float restWaveTimer;
    public GameObject enemies;
    public PickUpSpawner pickUpSpawner;
    bool restWave;
    public float waveTimer;
    int wave;
    bool end = false;

    public GameObject []boids;

    public Waves[] waveControl;

    // Start is called before the first frame update
    void Start()
    {
        wave = 0;
        waveTimer = (float)maxWaveTimer;
        waveStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (restWave)
        {
            waveTimer -= Time.deltaTime;

            if (wave > maxWaves)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameOver();
            }

            

            else if(waveTimer <0.0f)
            {
                end = false;
               // waveTimer -= Time.deltaTime;
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
                restWave = true;
                waveTimer = (float)maxWaveTimer;
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

        for (int i = 0; i < waveControl[wave].enemies.Length; i++)
        {
            count++;
        }

        boids = new GameObject[count];

        for (int i = 0; i < waveControl[wave].enemies.Length; i++)
        {
            boids[i] = Instantiate(waveControl[wave].enemies[i], GetRandomLocation(), Quaternion.identity, null).gameObject;
        }

        pickUpSpawner.deletePickups();

        return count;
    }

    void WaveEnd()
    {
        pickUpSpawner.deletePickups();
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
