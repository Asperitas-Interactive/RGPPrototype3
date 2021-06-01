using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyFollow : MonoBehaviour
{
    [FormerlySerializedAs("player")] public Transform m_player;

    private NavMeshAgent m_navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = Random.Range(0.8f, 2.0f);
      //  rb = GetComponent<Rigidbody>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;
        m_navMeshAgent.SetDestination(m_player.position);
        
    }
}
