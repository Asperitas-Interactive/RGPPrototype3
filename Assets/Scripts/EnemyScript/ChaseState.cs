using System;
using UnityEngine.AI;

public class ChaseState: BaseState
{
    private EnemyControl m_enemy;
    public ChaseState(EnemyControl _enemy) : base (_enemy.gameObject)
    {
        m_enemy = _enemy;
        m_enemy.m_agent.SetDestination(m_enemy.m_player.position);
    }


    public override Type Tick()
    {
        m_enemy.m_agent.SetDestination(m_enemy.m_player.position);
        if (m_enemy.m_agent.remainingDistance < 2.5f)
        {
            
            return typeof(PseudoAttackState);
        }
        return null;
    }

    public override void Init()
    {
        m_enemy.m_agent.stoppingDistance = 3f;
        m_enemy.m_boxCollider.enabled = false;

    }
}