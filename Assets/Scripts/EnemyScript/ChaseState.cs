using System;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState: BaseState
{
    private EnemyControl m_enemy;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    public ChaseState(EnemyControl _enemy) : base (_enemy.gameObject)
    {
        m_enemy = _enemy;
        m_enemy.m_agent.SetDestination(m_enemy.m_player.position);
    }


    public override Type Tick()
    {
        foreach (var _animator in m_animator)
        {
            _animator.SetFloat(Speed, m_enemy.m_agent.velocity.magnitude);
        }

        m_enemy.m_agent.SetDestination(m_enemy.m_player.position);
        if (m_enemy.m_agent.remainingDistance < 3.5f)
        {
            
            return typeof(PseudoAttackState);
        }
        return null;
    }

    public override void Init()
    {
        m_agent.stoppingDistance = 3f;
        m_enemy.m_boxCollider.enabled = false;
        foreach (var _animator in m_animator)
        {
            _animator.SetBool(Attack, false);
        }

    }

    public override void Destroy()
    {

    }
}