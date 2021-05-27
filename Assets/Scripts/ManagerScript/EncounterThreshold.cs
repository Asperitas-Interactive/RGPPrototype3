using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EncounterThreshold : MonoBehaviour
{
    [FormerlySerializedAs("Star2Combo")] public int m_star2Combo;
    [FormerlySerializedAs("Star3Combo")] public int m_star3Combo;
    [FormerlySerializedAs("Star4Combo")] public int m_star4Combo;
    [FormerlySerializedAs("Star5Combo")] public int m_star5Combo;

    [FormerlySerializedAs("Star1Money")] public float m_star1Money;
    [FormerlySerializedAs("Star2Money")] public float m_star2Money;
    [FormerlySerializedAs("Star3Money")] public float m_star3Money;
    [FormerlySerializedAs("Star4Money")] public float m_star4Money;
    [FormerlySerializedAs("Star5Money")] public float m_star5Money;

    [FormerlySerializedAs("waves")] public Waves[] m_waves;

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject.CompareTag("Player"))
        {
            Debug.Log("EncounterStart!");
            GameObject.FindGameObjectWithTag("Wavemanager").GetComponent<waveManager>().WaveStart(this);
        }
    }
}