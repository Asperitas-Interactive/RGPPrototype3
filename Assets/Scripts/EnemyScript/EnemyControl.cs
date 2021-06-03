using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
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
    public Transform m_player { get; private set; }

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

    public bool m_isStunned = false;

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

    [SerializeField] private GameObject m_critical;
    
    bool m_canHit = true;

    public NavMeshAgent m_agent { get; private set; }
    bool m_destroy = false;
    float m_destroyTimer = 0.5f;

    [FormerlySerializedAs("hit")] public AudioSource m_hit;
    [FormerlySerializedAs("damagesound")] public AudioSource m_damagesound;
    public BoxCollider m_boxCollider { get; private set; }
    private Rigidbody m_rigidbody;
    
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("speed");
    public Animator[] m_animator { get; private set; }

    private Transform m_target;
    public bool m_death { get; private set; }

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

        m_hit = GameObject.FindGameObjectWithTag("HitSound").GetComponent<AudioSource>();
        m_damagesound = GameObject.FindGameObjectWithTag("DamageSound").GetComponent<AudioSource>();

        m_rankingSys = m_player.GetComponent<RankingSystem>();

        m_children = new GameObject[transform.childCount];
        for (int j = 0; j < transform.childCount; j++)
        {
            m_children[j] = transform.GetChild(j).gameObject;
        }

        m_agent.speed = Random.Range(gameManager.Instance.m_enemySpeed[0], gameManager.Instance.m_enemySpeed[1]);

        int i = 0;
        if (m_hPool == eHealthPool.Weak)
        {
            m_animator = new Animator[transform.childCount - 1];

            foreach (var child in m_children)
            {
                if (child.name != "Canvas")
                {
                    m_animator[i++] = child.GetComponent<Animator>();
                }
            }
        }
        else
        {
            m_animator = new Animator[1];
            m_animator.SetValue(m_children[6].GetComponent<Animator>(), 0);

        }

        m_boxCollider = GetComponent<BoxCollider>();       

        m_maxAttackCooldown = Random.Range(2f, 6f);
        InitializeStateMachine();
    }

    private void Test()
    {
        Debug.Log("AnimationEvents");
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            {typeof(WanderState), new WanderState(this)},
            {typeof(ChaseState), new ChaseState(this)},
            {typeof(PseudoAttackState), new PseudoAttackState(this)},
            {typeof(AttackState), new AttackState(this)},
            {typeof(StunState), new StunState(this)},
        };
        
        GetComponent<StateMachine>().SetState(states);
    }

    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if(m_death)
            Destroy(this);

        if (m_hPool == eHealthPool.Weak)
        {
            #region EnemyModelFromHealth


            if (m_health < m_maxHealth && m_health > m_maxHealth - m_maxHealth / 5)
            {

                m_children[1].transform.GetChild(4).gameObject.SetActive(true);

                m_children[0].SetActive(false);

            }

            else if (m_health < m_maxHealth - m_maxHealth * (1.0f / 5.0f) &&
                     m_health > m_maxHealth - m_maxHealth * (2.0f / 5.0f))
            {

                m_children[1].transform.GetChild(4).gameObject.SetActive(false);
                m_children[2].transform.GetChild(4).gameObject.SetActive(true);

            }

            else if (m_health < m_maxHealth - m_maxHealth * (2.0f / 5.0f) &&
                     m_health > m_maxHealth - m_maxHealth * (3.0f / 5.0f))
            {
                m_children[1].transform.GetChild(4).gameObject.SetActive(false);
                m_children[2].transform.GetChild(4).gameObject.SetActive(false);
                m_children[3].transform.GetChild(4).gameObject.SetActive(true);

            }
            else if (m_health < m_maxHealth - m_maxHealth * (3.0f / 5.0f) &&
                     m_health > m_maxHealth - m_maxHealth * (4.0f / 5.0f))
            {
                m_children[1].transform.GetChild(4).gameObject.SetActive(false);
                m_children[2].transform.GetChild(4).gameObject.SetActive(false);
                m_children[3].transform.GetChild(4).gameObject.SetActive(false);
                m_children[4].transform.GetChild(4).gameObject.SetActive(true);

            }
            else if (m_health < m_maxHealth - m_maxHealth * (4.0f / 5.0f) &&
                     m_health > m_maxHealth - m_maxHealth * (5.0f / 5.0f))
            {
                m_children[1].transform.GetChild(4).gameObject.SetActive(false);
                m_children[2].transform.GetChild(4).gameObject.SetActive(false);
                m_children[3].transform.GetChild(4).gameObject.SetActive(false);
                m_children[4].transform.GetChild(4).gameObject.SetActive(false);
                m_children[5].transform.GetChild(4).gameObject.SetActive(true);

            }
            else if (m_health < 0f)
            {
                m_children[1].transform.GetChild(4).gameObject.SetActive(false);
                m_children[2].transform.GetChild(4).gameObject.SetActive(false);
                m_children[3].transform.GetChild(4).gameObject.SetActive(false);
                m_children[4].transform.GetChild(4).gameObject.SetActive(false);
                m_children[5].transform.GetChild(4).gameObject.SetActive(true);

                m_death = true;
                m_children[5].GetComponent<DissolvingController>().Die();
            }

            #endregion
        }
        //CalculateTimer();

        if (m_hPool == eHealthPool.Normal)
        {
            
            if (m_health < 0f)
            {
                m_death = true;
               
                m_children[6].GetComponent<DissolvingController>().Die();
            }
        }
        

       
       // m_animator.SetFloat(Speed, m_agent.velocity.magnitude);

    }

    private void CalculateTimer()
    {
        if (m_status != eStatus.Attack) ;
        // {
        //     if (m_attackCooldown < 0f)
        //     {
        //         m_status = eStatus.Attack;
        //     }
        //     else if ((transform.position - m_player.position).magnitude < m_agent.stoppingDistance + 2.0f)
        //     {
        //         m_attackCooldown -= Time.deltaTime;
        //     }
        // }
    }

    // private void LateUpdate()
    // {
    //     switch (m_status)
    //     {   
    //         case eStatus.Attack:
    //             
    //             break;
    //         
    //         case eStatus.Stun:
    //
    //             break;
    //         
    //         case eStatus.Follow:
    //
    //             break;
    //     }        
    // }
    //
    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.CompareTag("Player") && !m_destroy)
        {
            m_player.gameObject.GetComponent<PlayerMovement>().Heal(m_damage);
            m_attackTimer = 0f;
            m_attackCooldown = m_maxAttackCooldown;
            foreach (var _animator in m_animator)
            {
                _animator.SetBool(Attack, false);
            }
    
            m_rankingSys.DropCombo();
            m_damagesound.Play();
        }
    }
    //
    private void OnCollisionEnter(Collision _collision)
    {
        //Check collisions
        if (_collision.gameObject.CompareTag("Sword") && m_canHit && m_player.gameObject.GetComponent<CombatControl>().m_canDamage)
        {
            m_player.gameObject.GetComponent<CombatControl>().m_canDamage = false;

            foreach (var _animator in m_animator)
            {
                _animator.SetBool("Damage", true);
            }

            GetComponent<StateMachine>().Attacked();
        
            m_canHit = false;
            CombatControl cc = _collision.gameObject.GetComponentInParent<CombatControl>();
    
            
    
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
               // m_agent.speed = Random.Range(5.0f, 6.0f);
                m_health = Random.Range(20, 30);
                m_maxHealth = m_health;
                break;
            case eHealthPool.Normal:
                m_health = Random.Range(300, 400);
               // m_agent.speed = Random.Range(4.0f, 5.0f);
                m_maxHealth = m_health;
                break;
            case eHealthPool.Strong:
               // m_agent.speed = Random.Range(3.0f, 4.0f);
                m_health = Random.Range(150, 200);
                m_maxHealth = m_health;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetTarget(Transform _target)
    {
        m_target = _target;
    }

    public void Critical(bool _critical)
    {
        m_critical.SetActive(_critical);
    }
}
