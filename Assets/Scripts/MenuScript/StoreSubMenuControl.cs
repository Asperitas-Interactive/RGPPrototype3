using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StoreSubMenuControl : MonoBehaviour
{
    [FormerlySerializedAs("WeaponUp")] public Canvas m_weaponUp;
    [FormerlySerializedAs("ComboUp")] public Canvas m_comboUp;
    [FormerlySerializedAs("EquipUp")] public Canvas m_equipUp;
    // Start is called before the first frame update
    void Start()
    {
        m_weaponUp.enabled = false;
        m_comboUp.enabled = false;
        m_equipUp.enabled = false;
    }

    public void WeaponClick()
    {
        m_weaponUp.enabled = true;
        m_comboUp.enabled = false;
        m_equipUp.enabled = false;
    }

    public void ComboClick()
    {
        m_comboUp.enabled = true;
        m_weaponUp.enabled = false;
        m_equipUp.enabled = false;
    }

    public void StabClick()
    {
        m_comboUp.enabled = false;
        m_weaponUp.enabled = false;
        m_equipUp.enabled = false;
    }

    public void EquipClick()
    {
        m_equipUp.enabled = true;
        m_comboUp.enabled = false;
        m_weaponUp.enabled = false;
    }

    public void CloseAll()
    {
        m_weaponUp.enabled = false;
        m_comboUp.enabled = false;
        m_equipUp.enabled = false;
    }
}
