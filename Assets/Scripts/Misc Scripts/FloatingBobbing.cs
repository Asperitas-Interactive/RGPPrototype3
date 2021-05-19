using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBobbing : MonoBehaviour
{
    //The Distance the end points are
    public Vector3 DisplacementPos;
    public Vector3 DisplacementNeg;
    //The Speed you arrive at a end point
    private Vector3 VectorSpeed;
    //The Destination it checks
    private Vector3 DestinationMax;
    private Vector3 DestinationMin;
    // Start is called before the first frame update
    void Start()
    {
        VectorSpeed.y = 0.5f;
        DestinationMax = transform.position + DisplacementPos;
        DestinationMin = transform.position + DisplacementNeg;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 30.0f * Time.deltaTime, 0, Space.Self);
        //Move towards location
        transform.Translate(VectorSpeed * Time.deltaTime, Space.World);
        //Clamp between for correct calculation
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, DestinationMin.x, DestinationMax.x),
            Mathf.Clamp(transform.position.y, DestinationMin.y, DestinationMax.y),
            Mathf.Clamp(transform.position.z, DestinationMin.z, DestinationMax.z));
        //check if reached
        DestinationReach();
    }

    //Check if it reached its destination
    void DestinationReach()
    {
        if (transform.position.x == DestinationMax.x && transform.position.y == DestinationMax.y && transform.position.z == DestinationMax.z)
        {
            VectorSpeed = -VectorSpeed;
        }

        if (transform.position.x == DestinationMin.x && transform.position.y == DestinationMin.y && transform.position.z == DestinationMin.z)
        {
            VectorSpeed = -VectorSpeed;
        }
    }
}
