using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tranformToRB : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "Sword")
        {
            rb = transform.GetChild(0).GetComponent<Rigidbody>();
        }
        else
            rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position);
        rb.MoveRotation(transform.rotation);
    }
}
