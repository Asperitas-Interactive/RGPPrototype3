using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class waveManager : MonoBehaviour
{
    public int boidCount = 0;
    public int maxWaves;
    public int maxWaveTimer;
    public float restWaveTimer;
    public GameObject enemies;
    public PickUpSpawner pickUpSpawner;
   public bool restWave;
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
        int t = 0;
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
                    t++;
                    flag = true;
                    
                }
            }
            boidCount = t;

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
            boids[i] = Instantiate(waveControl[wave].enemies[i], GetRandomLocationAir(), Quaternion.identity, null).gameObject;
        }

        pickUpSpawner.deletePickups();

        return count;
    }

    void WaveEnd()
    {
        pickUpSpawner.deletePickups();
    }


    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }

        
        return finalPosition;
    }

    public Vector3 GetRandomLocation()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();
        
        int t = Random.Range(0, data.indices.Length - 3);


        
        
        Vector3 point = Vector3.Lerp(data.vertices[data.indices[t]], data.vertices[data.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, data.vertices[data.indices[t + 2]], Random.value);
        
        if(point.y > 19.0f && point.y < 21f)
        {
            Vector3 _point = GetRandomLocation();
            return _point;
        }

        else return point;

    }

    public Vector3 GetRandomLocationAir()
    {
        NavMeshTriangulation data = NavMesh.CalculateTriangulation();

        int t = Random.Range(0, data.indices.Length - 3);




        Vector3 point = Vector3.Lerp(data.vertices[data.indices[t]], data.vertices[data.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, data.vertices[data.indices[t + 2]], Random.value);

        if (point.y < 19.0f || point.y > 21f)
        {
            Vector3 _point = GetRandomLocationAir();
            return _point;
        }

        else return point;

    }


}
