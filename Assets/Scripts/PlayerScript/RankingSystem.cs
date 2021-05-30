using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RankingSystem : MonoBehaviour
{
    [FormerlySerializedAs("combo")] [SerializeField]
    int m_combo = 0;

    int m_currentScore;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseCombo()
    {
        m_combo++;
    }

    public void DropCombo()
    {
        if (m_combo >= 0 && m_combo < 15)
        {
            m_combo = 0;
        }
        else
        {
            m_combo -= 15;
        }
    }

    public int GETCombo()
    {
        return m_combo;
    }
}
