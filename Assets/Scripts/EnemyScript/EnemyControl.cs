using System;
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

    private GameObject[] m_children;
    
    [FormerlySerializedAs("maxCharge")] public float m_maxCharge;
    [FormerlySerializedAs("chargeTimer")] [FormerlySerializedAs("ChargeTimer")] public float m_chargeTimer;
    [FormerlySerializedAs("evadeTimer")] [FormerlySerializedAs("EvadeTimer")] public float m_evadeTimer;
    [FormerlySerializedAs("maxEvade")] public float m_maxEvade = 10f;
    [FormerlySerializedAs("ChargeCoolDown")] public float m_chargeCoolDown = 5.0f;
    [FormerlySerializedAs("EvadeCoolDown")] public float m_evadeCoolDown = 5.0f;
    [FormerlySerializedAs("EvadeDistance")] public float m_evadeDistance = 20f;
    [FormerlySerializedAs("maxAttackCooldown")] public float m_maxAttackCooldown;

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
    private Rigidbody m_rigidbody;
    
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int Speed = Animator.StringToHash("speed");
    private Animator m_animator;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
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

        m_children = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            m_children[i] = transform.GetChild(i).gameObject;
        }
        
        m_boxCollider = m_children[1].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
        m_animator = m_children[1].GetComponent<Animator>();
       

        m_maxAttackCooldown = Random.Range(2f, 6f);
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        #region EnemyModelFromHealth

        if (m_health < m_maxHealth && m_health > m_maxHealth - m_maxHealth / 5)
        {
            m_children[0].gameObject.SetActive(false);
            m_children[1].gameObject.SetActive(true);
            m_boxCollider = m_children[1].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
            m_animator = m_children[1].GetComponent<Animator>();
        }

        else if (m_health < m_maxHealth - m_maxHealth * (1.0f / 5.0f) &&
                 m_health > m_maxHealth - m_maxHealth * (2.0f / 5.0f))
        {
            m_children[0].gameObject.SetActive(false);

            m_children[1].gameObject.SetActive(false);
            m_children[2].gameObject.SetActive(true);

            m_boxCollider = m_children[2].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
            m_animator = m_children[2].GetComponent<Animator>();
        }

        else if (m_health < m_maxHealth - m_maxHealth * (2.0f / 5.0f) &&
                 m_health > m_maxHealth - m_maxHealth * (3.0f / 5.0f))
        {
            m_children[0].gameObject.SetActive(false);
            m_children[1].gameObject.SetActive(false);
            m_children[2].gameObject.SetActive(false);
            m_children[3].gameObject.SetActive(true);

            m_boxCollider = m_children[3].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
            m_animator = m_children[3].GetComponent<Animator>();
        }
        else if (m_health < m_maxHealth - m_maxHealth * (3.0f / 5.0f) &&
                 m_health > m_maxHealth - m_maxHealth * (4.0f / 5.0f))
        {
            m_children[0].gameObject.SetActive(false);
            m_children[1].gameObject.SetActive(false);
            m_children[2].gameObject.SetActive(false);
            m_children[3].gameObject.SetActive(false);
            m_children[4].gameObject.SetActive(true);
            m_boxCollider = m_children[4].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
            m_animator = m_children[4].GetComponent<Animator>();
        }
        else if (m_health < m_maxHealth - m_maxHealth * (4.0f / 5.0f) &&
                 m_health > m_maxHealth - m_maxHealth * (5.0f / 5.0f))
        {
            m_children[0].gameObject.SetActive(false);
            m_children[1].gameObject.SetActive(false);
            m_children[2].gameObject.SetActive(false);
            m_children[3].gameObject.SetActive(false);
            m_children[4].gameObject.SetActive(false);
            m_children[5].gameObject.SetActive(true);

            m_boxCollider = m_children[5].transform.GetChild(transform.childCount - 1).GetComponent<BoxCollider>();
            m_animator = m_children[5].GetComponent<Animator>();
        }

        #endregion

        //CalculateTimer();
        
        switch (m_status)
        {   
            case eStatus.Attack:
                m_agent.transform.LookAt(m_player);
                m_agent.SetDestination(m_player.position);
                m_boxCollider.enabled = true;
                break;
            
            case eStatus.Stun:
                m_agent.SetDestination(transform.position);
                break;
            
            case eStatus.Follow:
                m_agent.SetDestination(m_player.position);
                break;
        }
        

       
        m_animator.SetFloat(Speed, m_agent.velocity.magnitude);

    }

    private void CalculateTimer()
    {
        if (m_status != eStatus.Attack)
        {
            if (m_attackCooldown < 0f)
            {
                m_status = eStatus.Attack;
            }
            else if ((transform.position - m_player.position).magnitude < m_agent.stoppingDistance + 2.0f)
            {
                m_attackCooldown -= Time.deltaTime;
            }
        }
    }

    private void LateUpdate()
    {
        switch (m_status)
        {   
            case eStatus.Attack:
                
                break;
            
            case eStatus.Stun:

                break;
            
            case eStatus.Follow:

                break;
        }        
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.CompareTag("Player") && !m_destroy)
        {
            m_player.gameObject.GetComponent<PlayerMovement>().Heal(m_damage);
            m_attackTimer = 0f;
            m_attackCooldown = m_maxAttackCooldown;
            m_animator.SetBool(IsAttacking, false);

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
