using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    public EncounterThreshold[] encounters;
    private bool isActive = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach(EncounterThreshold et in encounters)
        {
            if (et.GetComponent<BoxCollider>().enabled == true)
            {
                break;
            }

            i++;
        }

        if(i != encounters.Length - 1)
        {
            isActive = false;
        } else
        {
            isActive = true;
        }

        Debug.Log(isActive);
    }
}
