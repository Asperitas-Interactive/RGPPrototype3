using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AttackEquip : MonoBehaviour
{
    public enum eAttacks
    {
        Stab, 
        Combo
    }

    [FormerlySerializedAs("attackType")] public eAttacks m_attackType;

    [FormerlySerializedAs("cc")] public CombatControl m_combatControl;

    private Dropdown m_dropdown;

    //Purchase Checking Booleans
    private bool m_mightyUnlock = false;
    private bool m_explosiveUnlock = false;
    private bool m_stunUnlock = false;
    public Sprite m_mightySprite;
    public Sprite m_explosiveSprite;
    public Sprite m_stunSprite;
    public Sprite m_questionMarkSprite;
    public Sprite m_normalSprite;
    public Sprite m_open;
    public Sprite m_Equip;

    // Start is called before the first frame update
    void Start()
    {
        m_dropdown = GetComponent<Dropdown>();

        m_dropdown.onValueChanged.AddListener(delegate {
            SetDropDown(m_dropdown);
        });
    }

    private void Update()
    {
        //This stupid piece of code is cause of the unity devs
        //I cant manipulate the drop down menu in the way i want
        //Cause the engine creates and destroys it at runtime
        //So i have to do this whenever its in the scene
        //So i can retrieve the appropriate game objects
        //To make the purchased moves toggleable

        //tldr, Unity Dropdown menus suck
        
        GameObject ddMenu;
        Toggle mighty;
        Toggle explosive;
        Toggle stun;
        Toggle normal;

        if(transform.childCount == 4)
        {
            ddMenu = transform.GetChild(3).gameObject;
        } else
        {
            ddMenu = null;
        }

        if(ddMenu != null)
        {
            normal = ddMenu.transform.GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<Toggle>();
            normal.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_normalSprite;
            transform.GetChild(0).GetComponent<Image>().sprite = m_open;
            Destroy(normal.gameObject.transform.GetChild(2).GetComponent<Text>());

            transform.GetChild(3).GetComponent<Image>().enabled = false;
            //Unity I despise you for doing this btw
            mighty = ddMenu.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Toggle>();

            if (m_mightyUnlock)
            {
                mighty.enabled = true;
                mighty.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_mightySprite;
                Destroy(mighty.gameObject.transform.GetChild(2).GetComponent<Text>());

            }
            else
            {
                mighty.enabled = false;
                mighty.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_questionMarkSprite;
                Destroy(mighty.gameObject.transform.GetChild(2).GetComponent<Text>());
                
            }

            explosive = ddMenu.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Toggle>();

            if (m_explosiveUnlock)
            {
                explosive.enabled = true;
                Destroy(explosive.gameObject.transform.GetChild(2).GetComponent<Text>());
                explosive.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_explosiveSprite;
            }
            else
            {
                explosive.enabled = false;
                Destroy(explosive.gameObject.transform.GetChild(2).GetComponent<Text>());
                explosive.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_questionMarkSprite;
            }

            stun = ddMenu.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<Toggle>();

            if (m_stunUnlock)
            {
                stun.enabled = true;
                Destroy(stun.gameObject.transform.GetChild(2).GetComponent<Text>());
                stun.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_stunSprite;
            }
            else
            {
                stun.enabled = false;
                Destroy(stun.gameObject.transform.GetChild(2).GetComponent<Text>());
                stun.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = m_questionMarkSprite;
            }
        }

        else
        {
            transform.GetChild(0).GetComponent<Image>().sprite = m_Equip;

        }
        
        
    }

    void SetDropDown(Dropdown _change)
    {
        //We have this switch so we can have the same script on all dropdowns
        switch (m_attackType)
        {
            case eAttacks.Stab:
                //0 - Normal
                //1 - Mighty
                //2 - Explosive
                //3 - Stun
                switch (m_dropdown.value)
                {
                    case 0:
                        m_combatControl.m_stabType = CombatControl.eAttackType.Normal;
                        break;
                    case 1:
                        m_combatControl.m_stabType = CombatControl.eAttackType.Mighty;
                        break;
                    case 2:
                        m_combatControl.m_stabType = CombatControl.eAttackType.Explosive;
                        break;
                    case 3:
                        m_combatControl.m_stabType = CombatControl.eAttackType.Stun;
                        break;
                    default:
                        break;
                }
                break;
            case eAttacks.Combo:
                //0 - Normal
                //1 - Mighty
                //2 - Explosive
                //3 - Stun
                switch (m_dropdown.value)
                {
                    case 0:
                        m_combatControl.m_comboType = CombatControl.eAttackType.Normal;
                        gameManager.Instance.m_StunTimer = gameManager.Instance.m_defaultStunTimer;
                        break;
                    case 1:
                        m_combatControl.m_comboType = CombatControl.eAttackType.Mighty;
                        gameManager.Instance.m_StunTimer = gameManager.Instance.m_defaultStunTimer;
                        break;
                    case 2:
                        m_combatControl.m_comboType = CombatControl.eAttackType.Explosive;
                        gameManager.Instance.m_StunTimer = gameManager.Instance.m_defaultStunTimer;
                        break;
                    case 3:
                        m_combatControl.m_comboType = CombatControl.eAttackType.Stun;
                        gameManager.Instance.m_StunTimer[0] = gameManager.Instance.m_defaultStunTimer[0] * 2f;
                        gameManager.Instance.m_StunTimer[1] = gameManager.Instance.m_defaultStunTimer[1] * 2f;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void ActivatePowerup(PurchaseMove.eActivator _activator)
    {
        switch (_activator)
        {
            case PurchaseMove.eActivator.Mighty:
                m_mightyUnlock = true;
                break;
            case PurchaseMove.eActivator.Explosive:
                m_explosiveUnlock = true;
                break;
            case PurchaseMove.eActivator.Stun:
                m_stunUnlock = true;
                break;
        }
    }
}
