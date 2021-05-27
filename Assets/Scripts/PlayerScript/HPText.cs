using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    private PlayerMovement m_player;

    [FormerlySerializedAs("currentHealth")] public Text m_currentHealth;
    [FormerlySerializedAs("maxHealth")] public Text m_maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        int health = (int)m_player.GETHealth();
        m_currentHealth.text = health.ToString();
        m_maxHealth.text = m_player.GETMaxHp().ToString();
    }
}
