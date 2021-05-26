using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public float Money;
    
    void ReceiveMoney(float _moneyCount)
    {
        Money += _moneyCount;
    }

    void TakeMoney(float _moneyCount)
    {
        Money -= _moneyCount;
    }
}
