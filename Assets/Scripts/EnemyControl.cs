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

    public enum healthPool
    {
        WEAK,
        NORMAL,
        STRONG
    };

    public healthPool hPool;

    //Rigidbody rb;

    public float maxCharge;
    public float ChargeTimer;
    public float EvadeTimer;
    public float maxEvade = 10f;
    public float ChargeCoolDown = 5.0f;
    public float EvadeCoolDown = 5.0f;
    public float EvadeDistance = 20f;
    public float maxAttackCooldown = 3.0f;
    public int damage = -10;
    float attackTimer;
    float attackCooldown = 3.0f;

    Vector3 chargevel;
    private NavMeshAgent agent;
    private bool ChargeBool = false;
    bool evading = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        //Set up pathfinding
        setVariables();
        maxCharge = Random.Range(5f, 20f);
        ChargeTimer = maxCharge;
        EvadeTimer = Random.Range(20.0f, 60.0f);
        agent.avoidancePriority = (int)Random.Range(20f, 79f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.SetDestination(player.position);
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
        //transform.GetChild(0).transform.LookAt(player);

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

        #region attackTimers
        attackCooldown -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        #endregion

        if ((transform.position - player.position).magnitude < (agent.stoppingDistance + 2.0f))
        {
            if(attackCooldown < 0.0f)
            {
                GetComponent<BoxCollider>().enabled = true;
                attackTimer = 3.0f;
                GetComponent<Animator>().SetBool("isAttacking", true);
            }

            if(attackTimer < 0.0f)
            {
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<Animator>().SetBool("isAttacking", false);
            }

        }

        else
        {
            GetComponent<Animator>().SetBool("isAttacking", false);
        }

        GetComponent<Animator>().SetFloat("speed", agent.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            player.gameObject.GetComponent<PlayerMovement>().Heal(damage);
            attackTimer = 0f;
            attackCooldown = maxAttackCooldown;
            GetComponent<Animator>().SetBool("isAttacking", false);

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

    public void setVariables()
    {
        if (hPool == healthPool.WEAK)
        {
            agent.speed = Random.Range(5.0f, 6.0f);
            health = Random.Range(50, 80);
        }
        else if (hPool == healthPool.NORMAL)
        {
            agent.speed = Random.Range(4.0f, 5.0f);
            health = Random.Range(80, 110);
        }
        else if (hPool == healthPool.STRONG)
        {
            agent.speed = Random.Range(3.0f, 4.0f);
            health = Random.Range(110, 140);
        }
    }
}
