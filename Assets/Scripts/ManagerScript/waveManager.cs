using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class waveManager : MonoBehaviour
{
    public int boidCount = 0;
    public int maxWaves;
    int wave;

    public GameObject[] boids;

    private Waves[] waveControl;

    public bool m_CombatEnded = false;

    public RankingSystem rankSys;

    public bool isActive = false;

    public EncounterThreshold encounterController;

    public ScoreUI meter;

    // Start is called before the first frame update
    void Start()
    {
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

            if (rankSys.getCombo() <= encounterController.Star2Combo)
            {
                meter.rank = 1;
            }
            else if (rankSys.getCombo() > encounterController.Star2Combo && rankSys.getCombo() <= encounterController.Star3Combo)
            {
                meter.rank = 2;
            }
            else if (rankSys.getCombo() > encounterController.Star3Combo && rankSys.getCombo() <= encounterController.Star4Combo)
            {
                meter.rank = 3;
            }
            else if (rankSys.getCombo() > encounterController.Star4Combo && rankSys.getCombo() <= encounterController.Star5Combo)
            {
                meter.rank = 4;
            }
            else if (rankSys.getCombo() > encounterController.Star5Combo)
            {
               meter.rank = 5;
            }
        }
    }

    public int waveStart(EncounterThreshold _encounter)
    {
        if (isActive == false)
        {
            int count = 0;

            encounterController = _encounter;

            waveControl = _encounter.waves;

            maxWaves = _encounter.waves.Length;

            wave = 0;

            for (int i = 0; i < waveControl[wave].enemies.Length; i++)
            {
                count++;
            }

            boids = new GameObject[count];

            for (int i = 0; i < waveControl[wave].enemies.Length; i++)
            {
                boids[i] = Instantiate(waveControl[wave].enemies[i], GetRandomLocation(), Quaternion.identity, null).gameObject;
            }

            isActive = true;

            meter.ShowImages();

            return count;
        }

        return 0;
    }

    void WaveEnd()
    {
        meter.HideImages();
        isActive = false;
        waveControl = new Waves[0];

        //Rewards
        if (rankSys.getCombo() <= encounterController.Star2Combo)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>().ReceiveMoney(encounterController.Star1Money);
        }
        else if (rankSys.getCombo() > encounterController.Star2Combo && rankSys.getCombo() <= encounterController.Star3Combo)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>().ReceiveMoney(encounterController.Star2Money);
        }
        else if (rankSys.getCombo() > encounterController.Star3Combo && rankSys.getCombo() <= encounterController.Star4Combo)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>().ReceiveMoney(encounterController.Star3Money);
        }
        else if (rankSys.getCombo() > encounterController.Star4Combo && rankSys.getCombo() <= encounterController.Star5Combo)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>().ReceiveMoney(encounterController.Star4Money);
        }
        else if (rankSys.getCombo() > encounterController.Star5Combo)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>().ReceiveMoney(encounterController.Star5Money);
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
