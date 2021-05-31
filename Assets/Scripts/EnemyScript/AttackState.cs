using System;
using UnityEngine;

public class AttackState:BaseState
{
    private float m_AttackCooldown;
    private EnemyControl m_enemy;
    private static readonly int Attack = Animator.StringToHash("Attack");

    public AttackState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_enemy = _enemy;
    }

    public override Type Tick()
    {
        m_AttackCooldown -= Time.deltaTime;
        if (m_AttackCooldown < 0f)
        {
            m_enemy.m_boxCollider.enabled = true;
            return typeof(PseudoAttackState);
        }

        return null;
    }

    public override void Init()
    {
        m_transform.LookAt(m_player.position);
        m_animator.SetBool(Attack, true);
        m_AttackCooldown = gameManager.Instance.m_attackCooldown;
    }
}