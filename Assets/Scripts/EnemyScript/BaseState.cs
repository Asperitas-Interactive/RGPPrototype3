using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState
{
    protected Transform m_transform;
    protected GameObject m_gameObject;

    protected Animator m_animator;
    protected EnemyControl m_enemy;
    protected Transform m_player;
    protected NavMeshAgent m_agent;

    public BaseState(GameObject _gameObject)
    {
        this.m_animator = _gameObject.GetComponent<EnemyControl>().m_animator;
        this.m_gameObject = _gameObject;
        this.m_transform = _gameObject.transform;
        this.m_enemy = _gameObject.GetComponent<EnemyControl>();
        this.m_player = m_enemy.m_player;
        this.m_agent = m_enemy.m_agent;

    }

    public abstract Type Tick();

    public void Update()
    {
        if (m_animator != m_enemy.m_animator)
        {
            m_animator = m_enemy.m_animator;
        }
    }

    public abstract void Init();
}