using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCheck : MonoBehaviour
{
    public EncounterThreshold[] encounters;
    public EncounterThreshold finalEncounter;
    private bool isActive = false; 
    // Start is called before the first frame update
    void Start()
    {
        finalEncounter.GetComponent<BoxCollider>().enabled = false;
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

        if(i != encounters.Length)
        {
            isActive = false;
        } else
        {
            isActive = true;
            finalEncounter.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
