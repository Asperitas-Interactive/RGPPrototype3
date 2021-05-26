using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{
    [SerializeField]
    int combo = 0;

    int currentScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseCombo()
    {
        combo++;
    }

    public void dropCombo()
    {
        if (combo >= 0 && combo < 15)
        {
            combo = 0;
        }
        else
        {
            combo -= 15;
        }
    }
}
