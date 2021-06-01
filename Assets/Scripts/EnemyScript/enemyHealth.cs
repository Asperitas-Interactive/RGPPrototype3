using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHealth : MonoBehaviour
{
    private int m_health = 100;
    [FormerlySerializedAs("spawnNum")] public int m_spawnNum = 5;

    public enum EnemyType
    {
        Standard,
        Rush,
        Charge
    };

    [FormerlySerializedAs("type")] public EnemyType m_type;

    private void Start()
    {
        if(m_type == EnemyType.Standard)
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

    private void OnCollisionEnter(Collision _collision)
    {
        if(_collision.gameObject.CompareTag("Sword"))
        {
            CombatControl cc = _collision.gameObject.GetComponentInParent<CombatControl>();

            m_health -= cc.m_damage;

            Debug.Log(m_health);
        }
    }
}
