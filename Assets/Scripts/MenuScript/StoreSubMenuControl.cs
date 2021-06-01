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

    [FormerlySerializedAs("WeaponButton")] public Image m_WeaponButton;
    [FormerlySerializedAs("ComboButton")] public Image m_ComboButton;
    [FormerlySerializedAs("EquipButton")] public Image m_EquipButton;

    [FormerlySerializedAs("WeaponImage")] public Sprite m_WeaponImage;
    [FormerlySerializedAs("WeaponSelectedImage")] public Sprite m_WeaponSelectedImage;

    [FormerlySerializedAs("ComboImage")] public Sprite m_ComboImage;
    [FormerlySerializedAs("ComboSelectedImage")] public Sprite m_ComboSelectedImage;

    [FormerlySerializedAs("EquipImage")] public Sprite m_EquipImage;
    [FormerlySerializedAs("EquipSelectedImage")] public Sprite m_EquipSelectedImage;

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
        m_WeaponButton.sprite = m_WeaponSelectedImage;
        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
    }

    public void ComboClick()
    {
        m_comboUp.enabled = true;
        m_ComboButton.sprite = m_ComboSelectedImage;
        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
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
        m_EquipButton.sprite = m_EquipSelectedImage;
        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
    }

    public void CloseAll()
    {
        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
    }
}
