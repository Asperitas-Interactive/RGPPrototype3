using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    [FormerlySerializedAs("cc")] public CombatControl m_cc;
    [FormerlySerializedAs("increase")] [SerializeField]
    private int m_increase;

    [FormerlySerializedAs("value")] public float m_value;

    [FormerlySerializedAs("moneyController")] public MoneyController m_moneyController;

    Button m_selfButton;

    [FormerlySerializedAs("successorButton")] public GameObject m_successorButton;
    [FormerlySerializedAs("successorText")] public GameObject m_successorText;
    // Start is called before the first frame update
    void Start()
    {
        m_moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>();

        m_selfButton = GetComponent<Button>();
        if (m_successorButton != null)
        {
            m_successorButton.gameObject.SetActive(false);
        }
        if (m_successorText != null)
        {
            m_successorText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        if (m_moneyController.m_money >= m_value)
        {
            m_selfButton.interactable = false;
            m_selfButton.transform.GetChild(0).GetComponent<Text>().text = "Sold Out";
            m_cc.DamageBoost(m_increase);
            m_moneyController.TakeMoney(m_value);
            if (m_successorButton != null)
            {
                m_successorButton.gameObject.SetActive(true);
            }
            if (m_successorText != null)
            {
                m_successorText.gameObject.SetActive(true);
            }
        } else
        {
            Debug.Log("Insufficient funds");
        }
    }
}
