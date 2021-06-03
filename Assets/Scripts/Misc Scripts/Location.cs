using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public Transform[] m_vanLocation;
    public Vector3 test;
    void Start()
    {
        test = new Vector3();
    }

    void Update()
    {
        
    }

    public void SetLocation(int _loc)
    {
        transform.position = m_vanLocation[_loc].position;
        transform.rotation = m_vanLocation[_loc].rotation;
    }
}
