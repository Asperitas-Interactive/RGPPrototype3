using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickup;

    public List<GameObject> spawnedPickups;

    private float timer = 10.0f;

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            //Update this for our area size
            Vector3 pos = new Vector3(Random.Range(-100, 101), 2.0f, Random.Range(-100, 101));
            GameObject newclone = Instantiate(pickup, pos, Quaternion.Euler(0, 0, 0));
            spawnedPickups.Add(newclone);
            timer = 10.0f;
        }
    }

    public void deletePickups()
    {
        for(int i = 0; i < spawnedPickups.Count; i++)
        {
            Destroy(spawnedPickups[i]);
        }

        spawnedPickups.Clear();
    }
}
