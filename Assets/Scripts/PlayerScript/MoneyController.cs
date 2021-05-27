using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MoneyController : MonoBehaviour
{
    [FormerlySerializedAs("Money")] public float m_money;
    
    public void ReceiveMoney(float _moneyCount)
    {
        m_money += _moneyCount;
    }

    public void TakeMoney(float _moneyCount)
    {
        m_money -= _moneyCount;
    }
}
