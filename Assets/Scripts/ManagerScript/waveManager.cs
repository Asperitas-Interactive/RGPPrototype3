using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class waveManager : MonoBehaviour
{
    [FormerlySerializedAs("boidCount")] public int m_boidCount = 0;
    [FormerlySerializedAs("maxWaves")] public int m_maxWaves;
    int m_wave;

    [FormerlySerializedAs("boids")] public GameObject[] m_boids;

    private Waves[] m_waveControl;

    [FormerlySerializedAs("m_CombatEnded")] public bool m_combatEnded = false;

    [FormerlySerializedAs("rankSys")] public RankingSystem m_rankSys;

    [FormerlySerializedAs("isActive")] public bool m_isActive = false;

    [FormerlySerializedAs("encounterController")] public EncounterThreshold m_encounterController;

    [FormerlySerializedAs("meter")] public ScoreUI m_meter;
    private MoneyController m_moneyController;

    // Start is called before the first frame update
    void Start()
    {
        m_wave = 0;
        m_moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>();
        m_rankSys = GameObject.FindGameObjectWithTag("Player").GetComponent<RankingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isActive)
        {
            int t = 0;
            bool flag = false;

            foreach (GameObject boid in m_boids)
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
            m_boidCount = t;

            if (!flag)
            {
                m_wave++;
                NewWave();
            }

            if (m_rankSys.GETCombo() <= m_encounterController.m_star2Combo)
            {
                m_meter.m_rank = 1;
            }
            else if (m_rankSys.GETCombo() > m_encounterController.m_star2Combo && m_rankSys.GETCombo() <= m_encounterController.m_star3Combo)
            {
                m_meter.m_rank = 2;
            }
            else if (m_rankSys.GETCombo() > m_encounterController.m_star3Combo && m_rankSys.GETCombo() <= m_encounterController.m_star4Combo)
            {
                m_meter.m_rank = 3;
            }
            else if (m_rankSys.GETCombo() > m_encounterController.m_star4Combo && m_rankSys.GETCombo() <= m_encounterController.m_star5Combo)
            {
                m_meter.m_rank = 4;
            }
            else if (m_rankSys.GETCombo() > m_encounterController.m_star5Combo)
            {
               m_meter.m_rank = 5;
            }

            //I moved it down the bottom so we can have multiple waves in one encounter
            if (m_wave > m_maxWaves)
            {
                //Insert new way to change the Scene
                m_combatEnded = true;
                WaveEnd();
            }
        }
    }

    public int WaveStart(EncounterThreshold _encounter)
    {
        if (m_isActive == false)
        {
            int count = 0;

            m_encounterController = _encounter;

            m_encounterController.turnOnWalls();

            m_waveControl = _encounter.m_waves;

            m_maxWaves = _encounter.m_waves.Length;

            m_wave = 0;

            for (int i = 0; i < m_waveControl[m_wave].m_enemies.Length; i++)
            {
                count++;
            }

            m_boids = new GameObject[count];

            for (int i = 0; i < m_waveControl[m_wave].m_enemies.Length; i++)
            {
                m_boids[i] = Instantiate(m_waveControl[m_wave].m_enemies[i], GetRandomLocation(), Quaternion.identity, null).gameObject;
            }

            m_isActive = true;

            m_meter.ShowImages();

            return count;
        }

        return 0;
    }

    int NewWave()
    {
        int count = 0;

        if (m_wave < m_maxWaves)
        {
            for (int i = 0; i < m_waveControl[m_wave].m_enemies.Length; i++)
            {
                count++;
            }

            m_boids = new GameObject[count];

            for (int i = 0; i < m_waveControl[m_wave].m_enemies.Length; i++)
            {
                m_boids[i] = Instantiate(m_waveControl[m_wave].m_enemies[i], GetRandomLocation(), Quaternion.identity, null).gameObject;
            }
        }
        return count;
    }

    private void WaveEnd()
    {
        m_encounterController.turnOffWalls();
        m_meter.HideImages();
        m_wave = 0;
        m_isActive = false;
        m_waveControl = new Waves[0];

        //Rewards
        if (m_rankSys.GETCombo() <= m_encounterController.m_star2Combo)
        {
            m_moneyController.ReceiveMoney(m_encounterController.m_star1Money);
        }
        else if (m_rankSys.GETCombo() > m_encounterController.m_star2Combo && m_rankSys.GETCombo() <= m_encounterController.m_star3Combo)
        {
            m_moneyController.ReceiveMoney(m_encounterController.m_star2Money);
        }
        else if (m_rankSys.GETCombo() > m_encounterController.m_star3Combo && m_rankSys.GETCombo() <= m_encounterController.m_star4Combo)
        {
            m_moneyController.ReceiveMoney(m_encounterController.m_star3Money);
        }
        else if (m_rankSys.GETCombo() > m_encounterController.m_star4Combo && m_rankSys.GETCombo() <= m_encounterController.m_star5Combo)
        {
            m_moneyController.ReceiveMoney(m_encounterController.m_star4Money);
        }
        else if (m_rankSys.GETCombo() > m_encounterController.m_star5Combo)
        {
            m_moneyController.ReceiveMoney(m_encounterController.m_star5Money);
        }
    }


    private Vector3 GetRandomLocation()
    {
        /*NavMeshTriangulation data = NavMesh.CalculateTriangulation();

        int t = Random.Range(0, data.indices.Length - 3);

        Vector3 point = Vector3.Lerp(data.vertices[data.indices[t]], data.vertices[data.indices[t + 1]], Random.value);
        point = Vector3.Lerp(point, data.vertices[data.indices[t + 2]], Random.value);*/

        var bounds = m_encounterController.gameObject.GetComponent<BoxCollider>().bounds;

        var point = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            0.0f,
            Random.Range(bounds.min.z, bounds.max.z)
            );

        return point;
    }
}
