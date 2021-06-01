
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState: BaseState
{
    private EnemyControl m_enemy;
    private Vector3? m_destination;
   

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

    public override void Destroy()
    {
    }
}