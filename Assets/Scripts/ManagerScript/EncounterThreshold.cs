using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EncounterThreshold : MonoBehaviour
{
    [FormerlySerializedAs("Star2Combo")] public int m_star2Combo;
    [FormerlySerializedAs("Star3Combo")] public int m_star3Combo;
    [FormerlySerializedAs("Star4Combo")] public int m_star4Combo;
    [FormerlySerializedAs("Star5Combo")] public int m_star5Combo;
    public UnityEvent m_passthrough;


    [FormerlySerializedAs("Star1Money")] public float m_star1Money;
    [FormerlySerializedAs("Star2Money")] public float m_star2Money;
    [FormerlySerializedAs("Star3Money")] public float m_star3Money;
    [FormerlySerializedAs("Star4Money")] public float m_star4Money;
    [FormerlySerializedAs("Star5Money")] public float m_star5Money;

    [FormerlySerializedAs("waves")] public Waves[] m_waves;

    [FormerlySerializedAs("InvisWalls")] public GameObject[] m_invisibleWalls;

    public String m_messageContent;
    
    public UnityEvent m_Open;
    public UnityEvent m_Close;
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("WaveManager").GetComponent<waveManager>().WaveStart(this);
            
        }
    }

    private void Start()
    {
        
    }

    public void turnOnWalls()
    { 
        m_Open?.Invoke();
        foreach(GameObject go in m_invisibleWalls)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
    }

    public void turnOffWalls()
    {
        m_Close?.Invoke();
        foreach(GameObject go in m_invisibleWalls)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }
    }

    public void Close()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
