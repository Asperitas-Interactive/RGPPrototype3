using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl : MonoBehaviour
{
    //For Health
    public int health = 100;

    public Slider slider;
    
    //For Wave Spawning
    public int spawnNum = 5;

    //For Pathfinding
    public Transform player;

    public RankingSystem rankingSys;

    public enum healthPool
    {
        WEAK,
        NORMAL,
        STRONG
    };

    public enum eStatus
    {
        follow,
        stun,
        attack,
    };

    eStatus m_status = eStatus.follow;

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

    public float maxStunTimer = 8.0f;
    public float maxStunPerHit = 3.0f;
    float stunTimer = 0.0f;


    public int damage = -10;
    float attackTimer;
    float attackCooldown = 3.0f;
    float maxHealth;

    Vector3 chargevel;
    private NavMeshAgent agent;
    private bool ChargeBool = false;
    bool evading = false;
    bool destroy = false;
    float destroyTimer = 0.5f;

    public AudioSource hit;
    public AudioSource damagesound;
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

        hit = GameObject.FindGameObjectWithTag("HitSound").GetComponent<AudioSource>();
        damagesound = GameObject.FindGameObjectWithTag("DamageSound").GetComponent<AudioSource>();

        rankingSys = player.GetComponent<RankingSystem>();
    }

    private void Update()
    {
        #region EnemyModelFromHealth
        if (health < maxHealth && health > maxHealth - maxHealth / 5)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        else if (health < maxHealth - maxHealth * (1.0f / 5.0f) && health > maxHealth - maxHealth * (2.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        else if (health < maxHealth - maxHealth * (2.0f / 5.0f) && health > maxHealth - maxHealth * (3.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (health < maxHealth - maxHealth * (3.0f / 5.0f) && health > maxHealth - maxHealth * (4.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (health < maxHealth - maxHealth * (4.0f / 5.0f) && health > maxHealth - maxHealth * (5.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(false);
            transform.GetChild(5).gameObject.SetActive(true);
        }

        #endregion

        #region attackTimers
        attackCooldown -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
        #endregion



        switch (m_status)
        {
            case eStatus.follow:
                {
                    //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    agent.SetDestination(player.position);
                    break;
                }
            case eStatus.stun:
                {
                    //transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    agent.velocity = Vector3.zero;
                    break;
                }
            case eStatus.attack:
                {
                    //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                    agent.transform.LookAt(player);
                    agent.velocity = Vector3.zero;

                    GetComponent<BoxCollider>().enabled = false;
                    for (int i = 1; i < transform.childCount; i++)
                    {
                        if (transform.GetChild(i).gameObject.activeSelf)
                        {
                            break;
                        }
                    }


                    break;
                }
            default:
                break;
        }

        //Seek
        // rb.velocity = (player.position - transform.position).normalized * 5.0f;

        if (attackCooldown < 0.0f)
        {
            GetComponent<BoxCollider>().enabled = true;
            attackTimer = 3.0f;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("isAttacking", true);
                    attackCooldown = maxAttackCooldown;
                    break;
                }
            }
            m_status = eStatus.follow;
        }

        if (m_status == eStatus.stun && stunTimer < 0.0f)
        {
            m_status = eStatus.follow;
        }



        if (slider!=null)
        slider.value = health;

        if (health <= 0 && !destroy)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    transform.GetChild(3).gameObject.SetActive(false);
                    transform.GetChild(4).gameObject.SetActive(false);
                    transform.GetChild(5).gameObject.SetActive(true);
                    transform.GetChild(5).gameObject.GetComponent<DissolvingController>().StartCoroutine(transform.GetChild(5).gameObject.GetComponent<DissolvingController>().Dissolve());
                    // transform.GetChild(i).GetComponent<Animator>().SetBool("death", true);
                    break;
                }
            }
            destroy = true;
            //agent.velocity = Vector3.zero;
            agent.isStopped  = true;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            destroyTimer = 2.967f;
        }


        destroyTimer -= Time.deltaTime;
        if(destroyTimer<0.0f && destroy)
        {
           // Destroy(this.gameObject);
        }

        //Debug.Log(attackCooldown);

        if ((transform.position - player.position).magnitude < (agent.stoppingDistance + 2.0f) && m_status != eStatus.stun) 
        {
            m_status = eStatus.attack;

        }

        else
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("isAttacking", false);
                    break;
                }
            }
        }
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).GetComponent<Animator>().SetFloat("speed", agent.velocity.magnitude);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player") && !destroy)
        {
            player.gameObject.GetComponent<PlayerMovement>().Heal(damage);
            attackTimer = 0f;
            attackCooldown = maxAttackCooldown;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool("isAttacking", false);
                    break;
                }
            }
            rankingSys.dropCombo();
            damagesound.Play();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Check collisions
        if (collision.gameObject.tag == "Sword")
        {
            CombatControl cc = collision.gameObject.GetComponentInParent<CombatControl>();

            if(attackCooldown > maxAttackCooldown - maxAttackCooldown * 4.0f / 5.0f && cc.damage > 0)
            {
                m_status = eStatus.stun;
                if (stunTimer > 0f)
                {
                    if(stunTimer < maxStunTimer - maxStunPerHit)
                        stunTimer += maxStunPerHit;

                }
                else stunTimer = maxStunPerHit;
            }

            health -= cc.damage;
            cc.AttackEffect(this);

            if (cc.damage > 0)
            {
                hit.Play();
                rankingSys.increaseCombo();
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
            health = Random.Range(400, 600);
            maxHealth = health;
        }
        else if (hPool == healthPool.NORMAL)
        {
            agent.speed = Random.Range(4.0f, 5.0f);
            health = Random.Range(600, 800);
            maxHealth = health;
        }
        else if (hPool == healthPool.STRONG)
        {
            agent.speed = Random.Range(3.0f, 4.0f);
            health = Random.Range(800, 1200);
            maxHealth = health;
        }
    }
}
