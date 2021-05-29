using System;
using UnityEngine;

public abstract class BaseState
{
    protected Transform m_transform;
    protected GameObject m_gameObject;

    public BaseState(GameObject _gameObject)
    {
        this.m_gameObject = _gameObject;
        this.m_transform = _gameObject.transform;
    }
    public abstract Type Tick();
    public abstract void Init();
}