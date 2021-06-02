using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PseudoAttackState: BaseState
{
    private float m_counterTimer = Random.Range(gameManager.Instance.m_counterTime[0], gameManager.Instance.m_counterTime[1]); 
    private EnemyControl m_enemy;
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int Speed = Animator.StringToHash("Speed");

    private void FindRandomDestination(Vector3 _origin)
    {
        
        Vector2 position = new Vector2(m_transform.forward.x, m_transform.forward.z);
        
        position.Normalize();

        position *= -3f;
        
        Vector3 testPosition = new Vector3(position.x + _origin.x, 0f, position.y+ _origin.z);
        foreach (var _animator in m_animator)
        {
            _animator.SetFloat(Speed, 1.0f);
        }
        m_agent.stoppingDistance = 0f;
        m_agent.SetDestination(testPosition);
    }
    
    public PseudoAttackState(EnemyControl _enemy) : base(_enemy.gameObject)
    {
        m_enemy = _enemy;
        
    }


    public override Type Tick()
    {
         m_transform.LookAt(new Vector3(m_player.position.x, m_transform.position.y, m_player.position.z));

         //m_enemy.DisableAttack();
        
        m_counterTimer -= Time.deltaTime;

        if ((m_transform.position - m_player.position).magnitude > 3.5f)
        {
            return typeof(ChaseState);
        }
        
        if (m_counterTimer < 0f)
        {
            return typeof(AttackState);
        }

        if ((m_enemy.m_player.position - m_transform.position).magnitude < 2f)
        {
            FindRandomDestination(m_enemy.m_player.position);
        }

       

       


        if(!m_agent.hasPath)
        {
            foreach (var _animator in m_animator)
            {
                _animator.SetFloat(Speed, 0.0f);
            }
        }

        return null;
    }

    public override void Init()
    {


        foreach (var _animator in m_animator)
        {
            _animator.SetFloat(Speed, 0f);
            _animator.SetBool(Attack, false);
        }
        
        m_counterTimer = Random.Range(gameManager.Instance.m_counterTime[0], gameManager.Instance.m_counterTime[1]);
    }

    public override void Destroy()
    {
    }
}