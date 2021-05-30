using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class PurchaseMove : MonoBehaviour
{
    public enum eActivator
    {
        Mighty,
        Explosive,
        Stun
    }

    [FormerlySerializedAs("activatortype")] public eActivator m_activatortype;
    
    [FormerlySerializedAs("equipScript")] public AttackEquip m_equipScript;

    [FormerlySerializedAs("value")] public float m_value;

    [FormerlySerializedAs("moneyController")] public MoneyController m_moneyController;

    private Button m_purchaseButton;
    // Start is called before the first frame update
    void Start()
    {
        m_purchaseButton = gameObject.GetComponent<Button>();
        m_moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void Purchase()
    {
        if (m_moneyController.m_money >= m_value)
        {
            m_purchaseButton.interactable = false;
            m_purchaseButton.transform.GetChild(0).GetComponent<Text>().text = "Sold Out";
            m_equipScript.ActivatePowerup(m_activatortype);
            m_moneyController.TakeMoney(m_value);
        } else
        {
            Debug.Log("insufficient funds");
        }
    }
}
