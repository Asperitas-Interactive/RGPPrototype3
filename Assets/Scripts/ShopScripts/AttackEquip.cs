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

    // Start is called before the first frame update
    void Start()
    {
        dropdown = GetComponent<Dropdown>();

        dropdown.onValueChanged.AddListener(delegate {
            SetDropDown(dropdown);
        });
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
}
