using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierVfx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Begin()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //transform.GetChild(i).GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }
    
    public void End()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }
}
