using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> m_availableStates;

    private void SwitchToNextState(Type _nextState)
    {
        m_currentState = m_availableStates[_nextState];
        a_OnStateChanged?.Invoke(m_currentState);
        
        m_currentState?.Init();

        gameManager.Instance.m_State = m_currentState.ToString();

    }

    public BaseState m_currentState { get; private set; }
    public event Action<BaseState> a_OnStateChanged;

    public void SetState(Dictionary<Type, BaseState> _states)
    {
        m_availableStates = _states;
    }
    
    void Update()
    {
        if (m_currentState == null)
        {
            m_currentState = m_availableStates.Values.First();
        }

        m_currentState?.Update();
        Type nextState = m_currentState?.Tick();

        if (nextState != null &&
            nextState != m_currentState?.GetType())
        {
            SwitchToNextState(nextState);
            
        }
    }
}