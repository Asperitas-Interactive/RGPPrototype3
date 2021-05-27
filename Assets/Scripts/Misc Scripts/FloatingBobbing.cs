using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FloatingBobbing : MonoBehaviour
{
    //The Distance the end points are
    [FormerlySerializedAs("displacementPos")] [FormerlySerializedAs("DisplacementPos")] public Vector3 m_displacementPos;
    [FormerlySerializedAs("displacementNeg")] [FormerlySerializedAs("DisplacementNeg")] public Vector3 m_displacementNeg;
    //The Speed you arrive at a end point
    private Vector3 m_vectorSpeed;
    //The Destination it checks
    private Vector3 m_destinationMax;
    private Vector3 m_destinationMin;
    // Start is called before the first frame update
    void Start()
    {
        m_vectorSpeed.y = 0.5f;
        m_destinationMax = transform.position + m_displacementPos;
        m_destinationMin = transform.position + m_displacementNeg;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 30.0f * Time.deltaTime, 0, Space.Self);
        //Move towards location
        transform.Translate(m_vectorSpeed * Time.deltaTime, Space.World);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, m_destinationMin.x, m_destinationMax.x),
            Mathf.Clamp(transform.position.y, m_destinationMin.y, m_destinationMax.y),
            Mathf.Clamp(transform.position.z, m_destinationMin.z, m_destinationMax.z));
        //check if reached
        DestinationReach();
    }

    //Check if it reached its destination
    void DestinationReach()
    {
        if (Math.Abs(transform.position.x - m_destinationMax.x) < 0.1f && Math.Abs(transform.position.y - m_destinationMax.y) < 0.1f && Math.Abs(transform.position.z - m_destinationMax.z) < 0.1f)
        {
            m_vectorSpeed = -m_vectorSpeed;
        }

        if (Math.Abs(transform.position.x - m_destinationMin.x) < 0.1f && Math.Abs(transform.position.y - m_destinationMin.y) < 0.1f && Math.Abs(transform.position.z - m_destinationMin.z) < 0.1f)
        {
            m_vectorSpeed = -m_vectorSpeed;
        }
    }
}
