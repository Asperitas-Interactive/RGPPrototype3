using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackEquip : MonoBehaviour
{
    public enum attacks
    {
        STAB, 
        COMBO
    }

    public attacks attackType;

    public CombatControl cc;

    private Dropdown dropdown;

    //Purchase Checking Booleans
    private bool mightyUnlock = false;
    private bool explosiveUnlock = false;
    private bool stunUnlock = false;

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            SetDropDown(dropdown);
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
        if(transform.childCount == 4)
        {
            ddMenu = transform.GetChild(3).gameObject;
        } else
        {
            ddMenu = null;
        }

        if(ddMenu != null)
        { 
            //Unity I despise you for doing this btw
            mighty = ddMenu.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.GetComponent<Toggle>();

            if (mightyUnlock)
            {
                mighty.enabled = true;
                mighty.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Mighty";
            }
            else
            {
                mighty.enabled = false;
                mighty.gameObject.transform.GetChild(2).GetComponent<Text>().text = "???";
            }

            explosive = ddMenu.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.GetComponent<Toggle>();

            if (explosiveUnlock)
            {
                explosive.enabled = true;
                explosive.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Explosive";
            }
            else
            {
                explosive.enabled = false;
                explosive.gameObject.transform.GetChild(2).GetComponent<Text>().text = "???";
            }

            stun = ddMenu.transform.GetChild(0).GetChild(0).GetChild(4).gameObject.GetComponent<Toggle>();

            if (stunUnlock)
            {
                stun.enabled = true;
                stun.gameObject.transform.GetChild(2).GetComponent<Text>().text = "Stun";
            }
            else
            {
                stun.enabled = false;
                stun.gameObject.transform.GetChild(2).GetComponent<Text>().text = "???";
            }
        }
    }

    void SetDropDown(Dropdown change)
    {
        //We have this switch so we can have the same script on all dropdowns
        switch (attackType)
        {
            case attacks.STAB:
                //0 - Normal
                //1 - Mighty
                //2 - Explosive
                //3 - Stun
                switch (dropdown.value)
                {
                    case 0:
                        cc.StabType = CombatControl.attackType.NORMAL;
                        break;
                    case 1:
                        cc.StabType = CombatControl.attackType.MIGHTY;
                        break;
                    case 2:
                        cc.StabType = CombatControl.attackType.EXPLOSIVE;
                        break;
                    case 3:
                        cc.StabType = CombatControl.attackType.STUN;
                        break;
                    default:
                        break;
                }
                break;
            case attacks.COMBO:
                //0 - Normal
                //1 - Mighty
                //2 - Explosive
                //3 - Stun
                switch (dropdown.value)
                {
                    case 0:
                        cc.ComboType = CombatControl.attackType.NORMAL;
                        break;
                    case 1:
                        cc.ComboType = CombatControl.attackType.MIGHTY;
                        break;
                    case 2:
                        cc.ComboType = CombatControl.attackType.EXPLOSIVE;
                        break;
                    case 3:
                        cc.ComboType = CombatControl.attackType.STUN;
                        break;
                    default:
                        break;
                }
                break;

            default:
                break;
        }
    }

    public void activatePowerup(PurchaseMove.activator activator)
    {
        switch (activator)
        {
            case PurchaseMove.activator.Mighty:
                mightyUnlock = true;
                break;
            case PurchaseMove.activator.Explosive:
                explosiveUnlock = true;
                break;
            case PurchaseMove.activator.Stun:
                stunUnlock = true;
                break;
        }
    }
}
