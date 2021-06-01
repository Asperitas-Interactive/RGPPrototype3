using System;
using UnityEngine;

public class AttackState:BaseState
{
    private float m_AttackCooldown;
    private EnemyControl m_enemy;
    private static readonly int Attacked = Animator.StringToHash("Attacked");
    private static readonly int Attack = Animator.StringToHash("Attack");

    public bool m_attacked = false;
    public AttackState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_enemy = _enemy;
    }

    public override Type Tick()
    {
        m_transform.LookAt(new Vector3(m_player.position.x, m_transform.position.y, m_player.position.z));
        m_AttackCooldown -= Time.deltaTime;
        if (m_attacked && m_AttackCooldown > 0f)
        {
            return typeof(StunState);
        }
        if (m_AttackCooldown < 0f)
        {

            
            m_animator.SetBool(Attacked, true);
            m_animator.SetBool(Attack, false);

            m_enemy.m_boxCollider.enabled = true;
            
        }

        if(m_AttackCooldown < -1.5f)
        {
            return typeof(PseudoAttackState);
        }

        if (m_enemy.m_player.GetComponent<CombatControl>().Counter(this.m_transform))
        {
            return typeof(StunState);
        }

        if (!m_agent.hasPath)
            m_animator.SetFloat("Speed", 0.0f);

        return null;
    }

    public override void Init()
    {
        m_attacked = false;
        m_animator.SetBool(Attack, true);

        m_transform.LookAt(m_player.position);
        m_AttackCooldown = gameManager.Instance.m_attackCooldown;
    }
    public override void Destroy()
    {
    }

}