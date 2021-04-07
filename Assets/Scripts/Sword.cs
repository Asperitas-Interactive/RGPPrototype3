using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(transform.position);
    }
}
