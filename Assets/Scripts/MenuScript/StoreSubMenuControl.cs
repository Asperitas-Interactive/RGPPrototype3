using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreSubMenuControl : MonoBehaviour
{
    public Canvas WeaponUp;
    public Canvas ComboUp;
    public Canvas EquipUp;
    // Start is called before the first frame update
    void Start()
    {
        WeaponUp.enabled = false;
        ComboUp.enabled = false;
        EquipUp.enabled = false;
    }

    public void WeaponClick()
    {
        WeaponUp.enabled = true;
        ComboUp.enabled = false;
        EquipUp.enabled = false;
    }

    public void ComboClick()
    {
        ComboUp.enabled = true;
        WeaponUp.enabled = false;
        EquipUp.enabled = false;
    }

    public void StabClick()
    {
        ComboUp.enabled = false;
        WeaponUp.enabled = false;
        EquipUp.enabled = false;
    }

    public void EquipClick()
    {
        EquipUp.enabled = true;
        ComboUp.enabled = false;
        WeaponUp.enabled = false;
    }

    public void CloseAll()
    {
        WeaponUp.enabled = false;
        ComboUp.enabled = false;
        EquipUp.enabled = false;
    }
}
