using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class flyingEnemy : MonoBehaviour
{


    public float m_radius;
    private Vector3 m_origin;
    public bool m_attackTranstion;
    public float m_maxTimer;

    Transform m_player;

    float m_attackBuffer = 5.0f;
    public enum eState
    {
        Wander,
        Follow,
        Attack,
    }

    public eState m_state;

    private float m_timer;
    NavMeshAgent m_agent;
    Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent = GetComponent<NavMeshAgent>();
        StateStuff();
    }
    private void OnEnable()
    {
        m_origin = transform.localPosition;
        
    }

    //Called every time state changes
    void StateStuff()
    {
        switch (m_state)
        {
            case eState.Wander:
                {
                    m_agent.stoppingDistance = 0;

                    Vector3 movePos = RandomPositionInCircle(m_origin, m_radius);
                    m_agent.SetDestination(movePos);
                }
                break;
            case eState.Follow:
                {
                    m_animator.SetBool("Attack", false);

                    m_agent.stoppingDistance = 25;
                    m_agent.SetDestination(m_player.position);
                }
                break;
            case eState.Attack:
                {
                    transform.LookAt(m_player);
                    m_animator.SetBool("Attack", true);
                }
                break;
            default:
                break;
        }
    }    
    private void OnDisable()
    {
        //GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_timer -= Time.deltaTime;

        switch (m_state)
        {
            case eState.Wander:
                {
                    if (m_agent.remainingDistance < 0.01f)
                    {
                        Vector3 movePos = RandomPositionInCircle(m_origin, m_radius);
                        m_agent.SetDestination(movePos);
                        //transform.rotation = Quaternion.Euler(0f, Mathf.Atan2(vel.x, vel.z) * Mathf.Rad2Deg, 0f);

                        m_timer = m_maxTimer;
                    }
                }   break;
            case eState.Follow:
                {
                    m_agent.SetDestination(m_player.position);
                    NavMeshPath path = m_agent.path;
                    m_agent.CalculatePath(m_player.position, path);
                    if(m_agent.remainingDistance < m_agent.stoppingDistance + 0.1f)
                    {
                        m_state = eState.Attack;
                        StateStuff();
                    }
                }
                break;
            case eState.Attack:
                {
                    m_animator.SetBool("Retreat", false);

                    if ((m_player.position - transform.GetChild(0).position).magnitude > m_attackBuffer && !m_attackTranstion)
                    {
                        m_animator.SetBool("Retreat", true);

                        m_state = eState.Follow;
                        StateStuff();
                    }
                }
                break;
            default:
                break;
        }

        if (m_state == eState.Wander)
        {
            if (m_agent.remainingDistance < 0.01f)
            {
                Vector3 movePos = RandomPositionInCircle(m_origin, m_radius);
                m_agent.SetDestination(movePos);

                m_timer = m_maxTimer;
            }
        }

        
    }

    Vector3 RandomPositionInCircle(Vector3 _origin, float _radius)
    {
        Vector3 randPos = new Vector3((Random.insideUnitCircle.x * _radius) + _origin.x, _origin.y, (Random.insideUnitCircle.y * _radius) + _origin.z) ;
        return randPos;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.tag == "Player")
        {

        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Player")
        {
            //agent.enabled = true;
        }
    }
}