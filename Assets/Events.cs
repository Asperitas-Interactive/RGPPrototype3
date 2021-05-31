using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Events : MonoBehaviour
{
    // Start is called before the first frame update
    public void Test()
    {
        Debug.Log("Test");
    }

    public void DisableCollider()
    {
        transform.parent.GetComponent<BoxCollider>().enabled = false;
    }
}
