using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public bool m_shouldOpen { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        m_shouldOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (m_shouldOpen && _collider.CompareTag("Player"))
        {
            Doors(true);
        }
    }

    public void Doors(bool _state)
    {
        if (_state)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Open", true);
                transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Close", false);
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Open", false);
                transform.GetChild(i).GetChild(0).GetComponent<Animator>().SetBool("Close", true);
            }   
        }
    }
}
