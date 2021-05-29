using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PseudoAttackState: BaseState
{
    private float m_counterTimer = gameManager.Instance.m_counterTime; 
    private EnemyControl m_enemy;
    private void FindRandomDestination(Vector3 _origin)
    {
        Vector2 position = Random.insideUnitCircle;
        
        position.Normalize();

        
        Vector3 testPosition = new Vector3(position.x * 3f + _origin.x, 0f, position.y * 3f + _origin.z);

        m_enemy.m_agent.stoppingDistance = 0f;
        m_enemy.m_agent.SetDestination(testPosition);
    }
    
    public PseudoAttackState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_enemy = _enemy;
    }

    public override Type Tick()
    {
        m_counterTimer -= Time.deltaTime;

        
        if (m_counterTimer < 0f)
        {
            return typeof(AttackState);
        }

        if ((m_enemy.m_player.position - m_transform.position).magnitude < 2f)
        {
            FindRandomDestination(m_enemy.m_player.position);
        }

        m_transform.LookAt(m_enemy.m_player);


        if (m_enemy.m_player.GetComponent<CombatControl>().Counter())
        {
            return typeof(StunState);
        }
        return typeof(AttackState);
    }

    public override void Init()
    {
        m_counterTimer = gameManager.Instance.m_counterTime;
    }
}

public class StunState:BaseState
{
    private float m_stunTimer;
    public StunState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_stunTimer = gameManager.Instance.m_StunTimer;
        _enemy.m_agent.SetDestination(m_transform.position);
    }

    public override Type Tick()
    {
        m_stunTimer -= Time.deltaTime;

        if (m_stunTimer < 0f)
        {
            return typeof(ChaseState);
        }

        return null;
    }

    public override void Init()
    {
        m_stunTimer = gameManager.Instance.m_StunTimer;
        

    }
}