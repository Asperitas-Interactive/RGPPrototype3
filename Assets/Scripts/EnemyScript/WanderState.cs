
using System;
using UnityEditorInternal;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState: BaseState
{
    private EnemyControl m_enemy;
    private Vector3? m_destination;
    private float m_stopDistance = 2f;
    private float m_turnSpeed = 1f;
    private readonly  LayerMask m_layerMask = LayerMask.NameToLayer("Ground");
    private float m_rayDistance = 3.5f;
    private Quaternion m_desiredRotation;
    private Vector3 m_direction;

    private float m_maxWanderTime = gameManager.Instance.m_maxWanderTime;
    private float m_wanderTime;
    private static readonly int Speed = Animator.StringToHash("Speed");



    public WanderState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_enemy = _enemy;
        m_wanderTime = m_maxWanderTime;
    }


    public override Type Tick()
    {
        
        m_animator.SetFloat(Speed, m_enemy.m_agent.velocity.magnitude);
        m_wanderTime -= Time.deltaTime;
        var chaseTarget = m_enemy.m_player;

        if (m_wanderTime < 0f)
        {
            return typeof(ChaseState);
        }

        if(!m_enemy.m_agent.hasPath)
            FindRandomDestination();

        if (m_destination.HasValue == true)
        {
            m_enemy.m_agent.SetDestination(m_destination.Value);
        }
        
        return null;
    }

    public override void Init()
    {
        m_wanderTime = m_maxWanderTime;
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(m_transform.position, m_direction);
        return Physics.SphereCast(ray, 0.5f, m_rayDistance, m_layerMask);
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(m_transform.position, m_transform.forward);
        return Physics.SphereCast(ray, 0.5f, m_rayDistance, m_layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition;
        var dir = Random.Range(0, 3);
        switch (dir)
        {
            case 0:
              testPosition = (m_transform.position + m_transform.forward * 4.0f) +
                new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
                 break;
            
            case 1:
                testPosition = (m_transform.position + m_transform.right * 4.0f) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
                break;
            
            case 2:
                testPosition = (m_transform.position - m_transform.forward * 4.0f) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
                break;
            
            case 3:
                testPosition = (m_transform.position - m_transform.right * 4.0f) +
                               new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));
                break;
            default:
                testPosition = new Vector3(0f, 0f, 0f);
                break;
        }
        
        m_destination = new Vector3(testPosition.x, 1f, testPosition.z);
        
        
    }

    private Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        float aggroRadius = 5f;
        
        RaycastHit hit;
        var angle = m_transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = m_transform.position;
        for(var i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, aggroRadius))
            {
                
                var playa = hit.collider.GetComponent<PlayerMovement>();
                if(playa != null)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return playa.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * aggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}