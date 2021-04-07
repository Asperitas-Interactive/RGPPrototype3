using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    //For Health
    private int health = 100;
    //For Wave Spawning
    public int spawnNum = 5;

    //For Pathfinding
    public Transform player;
    Rigidbody rb;


    private void Start()
    {
        //Set up pathfinding
        GetComponent<NavMeshAgent>().speed = Random.Range(0.8f, 2.0f);
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        //Seek
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;
        GetComponent<NavMeshAgent>().SetDestination(player.position);

        if (health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Check collisions
        if (collision.gameObject.tag == "Sword")
        {
            CombatControl cc = collision.gameObject.GetComponentInParent<CombatControl>();

            health -= cc.damage;
        }
    }
}
