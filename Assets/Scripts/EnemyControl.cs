using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    //For Health
    private int health = 100;

    public Slider slider;
    
    //For Wave Spawning
    public int spawnNum = 5;

    //For Pathfinding
    public Transform player;

    Rigidbody rb;

    public float maxCharge;
    public float ChargeTimer;
    public float EvadeTimer;
    public float ChargeCoolDown = 5.0f;
    public float EvadeCoolDown = 5.0f;

    private bool ChargeBool = false;

    private void Start()
    {
        //Set up pathfinding
        GetComponent<NavMeshAgent>().speed = Random.Range(0.8f, 2.0f);
        maxCharge = Random.Range(10.0f, 11.0f);
        ChargeTimer = maxCharge;
        EvadeTimer = Random.Range(20.0f, 60.0f);
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        GetComponent<NavMeshAgent>().SetDestination(player.position);
    }

    private void Update()
    {
        //Seek
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;
        ChargeAttack();

        if (ChargeBool == false)
        {
            GetComponent<NavMeshAgent>().SetDestination(player.position);
        } else
        {
            GetComponent<NavMeshAgent>().SetDestination(transform.forward * 20.0f);
        }
        if(slider!=null)
        slider.value = health;

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

    private void ChargeAttack()
    {
        if(ChargeBool == false)
        {
            if (ChargeTimer <= 0.0f)
            {
                GetComponent<NavMeshAgent>().speed *= 2;
                ChargeBool = true;
                ChargeCoolDown = 5.0f;
            } else
            {
                ChargeTimer -= Time.deltaTime;
            }
        } else
        {
            if(ChargeCoolDown <= 0.0f)
            {
                GetComponent<NavMeshAgent>().speed /= 2;
                ChargeBool = false;
                ChargeTimer = maxCharge;
            } else
            {
                ChargeCoolDown -= Time.deltaTime;
            }
        }
        
    }
}
