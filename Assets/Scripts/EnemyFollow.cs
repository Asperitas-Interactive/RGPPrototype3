using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().speed = Random.Range(0.8f, 2.0f);
      //  rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;
        GetComponent<NavMeshAgent>().SetDestination(player.position);
        
    }
}
