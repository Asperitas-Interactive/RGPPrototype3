using System;
using UnityEngine;

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
        foreach (var _animator in m_animator)
        {
            _animator.SetBool("Stun", true);
        }

    }

    public override void Destroy()
    {
        foreach (var _animator in m_animator)
        {
            _animator.SetBool("Stun", false);
        }

    }
}