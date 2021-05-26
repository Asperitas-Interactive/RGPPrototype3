using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterThreshold : MonoBehaviour
{
    public int Star2Combo;
    public int Star3Combo;
    public int Star4Combo;
    public int Star5Combo;

    public float Star1Money;
    public float Star2Money;
    public float Star3Money;
    public float Star4Money;
    public float Star5Money;

    public Waves[] waves;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("EncounterStart!");
        }
    }
}
