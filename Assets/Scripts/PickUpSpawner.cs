using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickup;

    private float timer = 10.0f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            //Update this for our area size
            Vector3 pos = new Vector3(Random.Range(-100, 101), 2.0f, Random.Range(-100, 101));
            Instantiate(pickup, pos, Quaternion.Euler(0, 0, 0));
            timer = 10.0f;
        }
    }
}
