using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    private int m_health = 100;
    public int spawnNum = 5;

    public enum EnemyType
    {
        Standard,
        Rush,
        Charge
    };

    public EnemyType type;

    private void Start()
    {
        if(type == EnemyType.Standard)
        {

        }
    }

    private void Update()
    {
        if(m_health < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Sword")
        {
            CombatControl cc = collision.gameObject.GetComponentInParent<CombatControl>();

            m_health -= cc.damage;

            Debug.Log(m_health);
        }
    }
}
