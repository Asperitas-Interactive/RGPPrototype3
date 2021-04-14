using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private PickUps[] pickUps;

    private PickUps setPickup;

    void Start()
    {
        setPickup = pickUps[Random.Range(0, pickUps.Length)];
        gameObject.GetComponent<MeshFilter>().mesh = setPickup.mMesh;
        gameObject.GetComponent<MeshRenderer>().material = setPickup.mMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            setPickup.powerUp(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
