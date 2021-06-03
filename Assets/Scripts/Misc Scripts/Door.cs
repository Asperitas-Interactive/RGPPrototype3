using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
