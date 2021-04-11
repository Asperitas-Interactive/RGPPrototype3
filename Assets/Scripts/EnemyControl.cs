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

    //Rigidbody rb;

    public float maxCharge;
    public float ChargeTimer;
    public float EvadeTimer;
    public float maxEvade = 10f;
    public float ChargeCoolDown = 5.0f;
    public float EvadeCoolDown = 5.0f;
    public float EvadeDistance = 20f;

    Vector3 chargevel;
    private NavMeshAgent agent;
    private bool ChargeBool = false;
    bool evading = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Set up pathfinding
        agent.speed = Random.Range(4.0f, 5.0f);
        maxCharge = Random.Range(5f, 20f);
        ChargeTimer = maxCharge;
        EvadeTimer = Random.Range(20.0f, 60.0f);
        agent.avoidancePriority = (int)Random.Range(20f, 79f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(player.position);
    }

    private void Update()
    {
        #region Evade
        EvadeTimer -= Time.deltaTime;
        EvadeCoolDown -= Time.deltaTime;
        if(evading && EvadeCoolDown <0f)
        {
            evading = false;
            EvadeTimer = maxEvade;
        }

        if(!evading && EvadeTimer < 0f)
        {
            GetComponent<Renderer>().material.color = (Color.green);
            evading = true;
            EvadeCoolDown = 5f;
        }

        #endregion
        //Seek
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;
        
        if (!evading)
        {
            ChargeAttack();
            if (ChargeBool == false)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                agent.destination = agent.desiredVelocity * 5 + transform.position;
                // agent.velocity = chargevel * 2;

            }
        }
        else
        {
            agent.SetDestination(player.position);
            //if(Vector3.Distance(player.position, transform.position) < EvadeDistance)
            {
                agent.velocity = agent.desiredVelocity * -1f;
            }
        }
        if(slider!=null)
        slider.value = health;

        if (health <= 0)
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
                GetComponent<Renderer>().material.color = Color.blue;
                agent.speed *= 2;
                ChargeBool = true;
                transform.LookAt(player);
                chargevel = agent.velocity;
                //agent.destination = transform.forward * 20.0f;
                
                ChargeCoolDown = 5.0f;
            } else
            {
                ChargeTimer -= Time.deltaTime;
            }
        } else
        {
            if(ChargeCoolDown <= 0.0f)
            {
                GetComponent<Renderer>().material.color = Color.white;
                agent.speed /= 2;
                ChargeBool = false;
                ChargeTimer = maxCharge;
            } else
            {
                ChargeCoolDown -= Time.deltaTime;
            }
        }
        
    }

    public void AOEDamage()
    {
        health -= 100;
    }
}
