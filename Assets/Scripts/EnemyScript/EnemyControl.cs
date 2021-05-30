using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnemyControl : MonoBehaviour
{
    //For Health
    [FormerlySerializedAs("health")] public int m_health = 100;

    [FormerlySerializedAs("slider")] public Slider m_slider;
    
    //For Wave Spawning
    [FormerlySerializedAs("spawnNum")] public int m_spawnNum = 5;

    //For Pathfinding
    [FormerlySerializedAs("player")] public Transform m_player;

    [FormerlySerializedAs("rankingSys")] public RankingSystem m_rankingSys;

    public enum eHealthPool
    {
        Weak,
        Normal,
        Strong
    };

    public enum eStatus
    {
        Follow,
        Stun,
        Attack,
    };

    eStatus m_status = eStatus.Follow;

    [FormerlySerializedAs("hPool")] public eHealthPool m_hPool;

    //Rigidbody rb;

    [FormerlySerializedAs("maxCharge")] public float m_maxCharge;
    [FormerlySerializedAs("chargeTimer")] [FormerlySerializedAs("ChargeTimer")] public float m_chargeTimer;
    [FormerlySerializedAs("evadeTimer")] [FormerlySerializedAs("EvadeTimer")] public float m_evadeTimer;
    [FormerlySerializedAs("maxEvade")] public float m_maxEvade = 10f;
    [FormerlySerializedAs("ChargeCoolDown")] public float m_chargeCoolDown = 5.0f;
    [FormerlySerializedAs("EvadeCoolDown")] public float m_evadeCoolDown = 5.0f;
    [FormerlySerializedAs("EvadeDistance")] public float m_evadeDistance = 20f;
    [FormerlySerializedAs("maxAttackCooldown")] public float m_maxAttackCooldown = 3.0f;

    [FormerlySerializedAs("maxStunTimer")] public float m_maxStunTimer = 8.0f;
    [FormerlySerializedAs("maxStunPerHit")] public float m_maxStunPerHit = 3.0f;
    float m_stunTimer = 0.0f;


    [FormerlySerializedAs("damage")] public int m_damage = -10;
    float m_attackTimer;
    float m_attackCooldown = 3.0f;
    float m_maxHealth;

    bool m_canHit = true;

    private NavMeshAgent m_agent;
    bool m_destroy = false;
    float m_destroyTimer = 0.5f;

    [FormerlySerializedAs("hit")] public AudioSource m_hit;
    [FormerlySerializedAs("damagesound")] public AudioSource m_damagesound;
    private BoxCollider m_boxCollider;
    
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private Rigidbody m_rigidbody;
    private static readonly int Speed = Animator.StringToHash("speed");

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_boxCollider = GetComponent<BoxCollider>();
        m_boxCollider = GetComponent<BoxCollider>();
        m_agent = GetComponent<NavMeshAgent>();

        //Set up pathfinding
        SetVariables();
        m_maxCharge = Random.Range(5f, 20f);
        m_chargeTimer = m_maxCharge;
        m_evadeTimer = Random.Range(20.0f, 60.0f);
        m_agent.avoidancePriority = (int)Random.Range(20f, 79f);
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent.SetDestination(m_player.position);

        m_hit = GameObject.FindGameObjectWithTag("HitSound").GetComponent<AudioSource>();
        m_damagesound = GameObject.FindGameObjectWithTag("DamageSound").GetComponent<AudioSource>();

        m_rankingSys = m_player.GetComponent<RankingSystem>();
    }

    private void Update()
    {
        #region EnemyModelFromHealth
        if (m_health < m_maxHealth && m_health > m_maxHealth - m_maxHealth / 5)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }

        else if (m_health < m_maxHealth - m_maxHealth * (1.0f / 5.0f) && m_health > m_maxHealth - m_maxHealth * (2.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }

        else if (m_health < m_maxHealth - m_maxHealth * (2.0f / 5.0f) && m_health > m_maxHealth - m_maxHealth * (3.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(true);
        }
        else if (m_health < m_maxHealth - m_maxHealth * (3.0f / 5.0f) && m_health > m_maxHealth - m_maxHealth * (4.0f / 5.0f))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);
            transform.GetChild(3).gameObject.SetActive(false);
            transform.GetChild(4).gameObject.SetActive(true);
        }
        else if (m_health < m_maxHealth - m_maxHealth * (4.0f / 5.0f) && m_health > m_maxHealth - m_maxHealth * (5.0f / 5.0f))
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
        m_attackCooldown -= Time.deltaTime;
        m_attackTimer -= Time.deltaTime;
        m_stunTimer -= Time.deltaTime;
        #endregion



        switch (m_status)
        {
            case eStatus.Follow:
                {
                    //transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    m_agent.SetDestination(m_player.position);
                    break;
                }
            case eStatus.Stun:
                {
                    //transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    m_agent.velocity = Vector3.zero;
                    break;
                }
            case eStatus.Attack:
                {
                    //transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);

                    m_agent.transform.LookAt(m_player);
                    m_agent.velocity = Vector3.zero;

                    m_boxCollider.enabled = false;
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

        if (m_attackCooldown < 0.0f)
        {
            m_boxCollider.enabled = true;
            m_attackTimer = 3.0f;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool(IsAttacking, true);
                    m_attackCooldown = m_maxAttackCooldown;
                    break;
                }
            }
            m_status = eStatus.Follow;
        }

        
        if (m_status == eStatus.Stun && m_stunTimer < 0.0f)
        {
            m_status = eStatus.Follow;
        }



        if (m_slider != null)
            m_slider.value = m_health;

        if (m_health <= 0 && !m_destroy)
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
                    transform.GetChild(5).gameObject.SetActive(false);
                    //transform.GetChild(6).gameObject.SetActive(true);
                    //transform.GetChild(6).gameObject.GetComponent<DissolvingController>().StartCoroutine(transform.GetChild(5).gameObject.GetComponent<DissolvingController>().Dissolve());
                    // transform.GetChild(i).GetComponent<Animator>().SetBool("death", true);
                    break;
                }
            }
            m_destroy = true;
            //agent.velocity = Vector3.zero;
            m_agent.isStopped  = true;
            m_rigidbody.velocity = Vector3.zero;
            m_destroyTimer = 2.967f;
        }


        m_destroyTimer -= Time.deltaTime;
        if(m_destroyTimer<0.0f && m_destroy)
        {
           // Destroy(this.gameObject);
        }

        //Debug.Log(attackCooldown);

        if ((transform.position - m_player.position).magnitude < (m_agent.stoppingDistance + 2.0f) && m_status != eStatus.Stun) 
        {
            m_status = eStatus.Attack;

        }

        else
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool(IsAttacking, false);
                    break;
                }
            }
        }
        for (int i = 1; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                transform.GetChild(i).GetComponent<Animator>().SetFloat(Speed, m_agent.velocity.magnitude);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if(_collider.CompareTag("Player") && !m_destroy)
        {
            m_player.gameObject.GetComponent<PlayerMovement>().Heal(m_damage);
            m_attackTimer = 0f;
            m_attackCooldown = m_maxAttackCooldown;
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).GetComponent<Animator>().SetBool(IsAttacking, false);
                    break;
                }
            }
            m_rankingSys.DropCombo();
            m_damagesound.Play();
        }
    }
    
    private void OnCollisionEnter(Collision _collision)
    {
        //Check collisions
        if (_collision.gameObject.CompareTag("Sword") && m_canHit)
        {
            m_canHit = false;
            CombatControl cc = _collision.gameObject.GetComponentInParent<CombatControl>();

            if(m_attackCooldown > m_maxAttackCooldown - m_maxAttackCooldown * 4.0f / 5.0f && cc.m_damage > 0)
            {
                m_status = eStatus.Stun;
                if (m_stunTimer > 0f)
                {
                    if(m_stunTimer < m_maxStunTimer - m_maxStunPerHit)
                        m_stunTimer += m_maxStunPerHit;

                }
                else m_stunTimer = m_maxStunPerHit;
            }

            m_health -= cc.m_damage;
            cc.AttackEffect(this);

            if (cc.m_damage <= 0) return;
            
            m_hit.Play();
            m_rankingSys.IncreaseCombo();
        }
    }

    private void OnCollisionExit(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Sword"))
        {
            m_canHit = true;
        }
    }


    private void SetVariables()
    {
        switch (m_hPool)
        {
            case eHealthPool.Weak:
                m_agent.speed = Random.Range(5.0f, 6.0f);
                m_health = Random.Range(400, 600);
                m_maxHealth = m_health;
                break;
            case eHealthPool.Normal:
                m_agent.speed = Random.Range(4.0f, 5.0f);
                m_health = Random.Range(100, 130);
                m_maxHealth = m_health;
                break;
            case eHealthPool.Strong:
                m_agent.speed = Random.Range(3.0f, 4.0f);
                m_health = Random.Range(150, 200);
                m_maxHealth = m_health;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
