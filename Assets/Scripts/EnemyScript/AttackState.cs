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
        if (m_enemy.m_death)
            return null;
        m_transform.LookAt(new Vector3(m_player.position.x, m_transform.position.y, m_player.position.z));
        m_AttackCooldown -= Time.deltaTime;
        if (m_attacked && m_AttackCooldown > 0f)
        {
            return typeof(StunState);
        }
        
        if(m_AttackCooldown<0.4f)
        {
            //m_enemy.m_boxCollider.enabled = true;

        }

        if (m_AttackCooldown < 0f)
        {
            m_enemy.Critical(false);

            foreach (var _animator in m_animator)
            {
                _animator.SetBool(Attacked, true);
            }

            
        }

        if(m_AttackCooldown < -1.3f)
        {
            return typeof(PseudoAttackState);
        }

        if (m_enemy.m_player.GetComponent<CombatControl>().Counter(this.m_transform))
        {
            return typeof(StunState);
        }

        if (!m_agent.hasPath)
        {
            foreach (var _animator in m_animator)
            {
                _animator.SetFloat("Speed", 0.0f);
            }
        }
        return null;
    }

    public override void Init()
    {
        m_enemy.Critical(true);
        m_attacked = false;

        m_transform.LookAt(m_player.position);
        foreach (var _animator in m_animator)
        {
            _animator.SetBool(Attack, true);
        }
        m_AttackCooldown = gameManager.Instance.m_attackCooldown;
    }
    public override void Destroy()
    {
        foreach (var _animator in m_animator)
        {
            _animator.SetBool("Attacked", false);
            _animator.SetBool(Attack, false);
        }

    }

}