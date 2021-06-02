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

    private Vector2 baseSize;

    // Start is called before the first frame update
    void Start()
    {
        m_weaponUp.enabled = false;
        baseSize = m_WeaponButton.gameObject.GetComponent<RectTransform>().sizeDelta;
        m_comboUp.enabled = false;
        m_equipUp.enabled = false;
    }

    public void WeaponClick()
    {
        m_weaponUp.enabled = true;
        m_WeaponButton.sprite = m_WeaponSelectedImage;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().sizeDelta = m_WeaponSelectedImage.rect.size;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);

        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_ComboButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_ComboButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
        m_EquipButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_EquipButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
    }

    public void ComboClick()
    {
        m_comboUp.enabled = true;
        m_ComboButton.sprite = m_ComboSelectedImage;
        m_ComboButton.gameObject.GetComponent<RectTransform>().sizeDelta = m_ComboSelectedImage.rect.size;
        m_ComboButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);

        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
        m_EquipButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_EquipButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
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
        m_EquipButton.gameObject.GetComponent<RectTransform>().sizeDelta = m_EquipSelectedImage.rect.size;
        m_EquipButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(0.8f, 0.8f);

        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_ComboButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_ComboButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
    }

    public void CloseAll()
    {
        m_weaponUp.enabled = false;
        m_WeaponButton.sprite = m_WeaponImage;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_WeaponButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        m_comboUp.enabled = false;
        m_ComboButton.sprite = m_ComboImage;
        m_ComboButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_ComboButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);

        m_equipUp.enabled = false;
        m_EquipButton.sprite = m_EquipImage;
        m_EquipButton.gameObject.GetComponent<RectTransform>().sizeDelta = baseSize;
        m_EquipButton.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1.5f, 1.5f);
    }
}
