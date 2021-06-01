using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    [FormerlySerializedAs("Money")] public float m_money;
    [FormerlySerializedAs("MoneyCount")] public Text MoneyUI;

    private void Update()
    {
        MoneyUI.text = m_money.ToString();
    }

    public void ReceiveMoney(float _moneyCount)
    {
        m_money += _moneyCount;

        if(m_money > 999)
        {
            m_money = 999;
        }
    }

    public void TakeMoney(float _moneyCount)
    {
        m_money -= _moneyCount;
    }
}
