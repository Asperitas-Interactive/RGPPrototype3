using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
    public float Money;
    
    public void ReceiveMoney(float _moneyCount)
    {
        Money += _moneyCount;
    }

    public void TakeMoney(float _moneyCount)
    {
        Money -= _moneyCount;
    }
}
