using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopDetection : MonoBehaviour
{
    [FormerlySerializedAs("Endbutton")] public Button m_endbutton;
    [FormerlySerializedAs("UpgradeButton")] public Button m_upgradeButton;
    [FormerlySerializedAs("prompt")] public Image m_prompt;
    [FormerlySerializedAs("inZone")] public bool m_inZone;
    [FormerlySerializedAs("inMenu")] public bool m_inMenu;
    [FormerlySerializedAs("player")] public GameObject m_player;
    [FormerlySerializedAs("camera")] public GameObject m_camera;
    [FormerlySerializedAs("waveManager")] public waveManager m_waveManager;

    GameObject[] m_playerUIObjects;
    GameObject[] m_shopUIObjects;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main.gameObject;

        m_endbutton.gameObject.SetActive(false);

        m_playerUIObjects = GameObject.FindGameObjectsWithTag("PlayerUI");
        m_shopUIObjects = GameObject.FindGameObjectsWithTag("ShopUI");

        foreach (GameObject go in m_playerUIObjects)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in m_shopUIObjects)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("AOE") && m_inZone == true)
        {
            if (m_inMenu == false)
            {
                Cursor.lockState = CursorLockMode.None;

                if (m_waveManager.m_combatEnded)
                {
                    m_endbutton.gameObject.SetActive(true);
                }

                foreach (GameObject go in m_playerUIObjects)
                {
                    go.SetActive(false);
                }
                foreach(GameObject go in m_shopUIObjects)
                {
                    go.SetActive(true);
                }
                
                m_inMenu = true;
            }
        }
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject == m_player)
        {
            m_inZone = true;
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if(_other.gameObject == m_player)
        {
            m_inZone = false;
        }
    }

    public void CloseMenus()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_upgradeButton.GetComponent<StoreSubMenuControl>().CloseAll();
        m_upgradeButton.GetComponent<UpgradeMenu>().CloseUpgradeMenu();
        m_endbutton.gameObject.SetActive(false);
        foreach (GameObject go in m_playerUIObjects)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in m_shopUIObjects)
        {
            go.SetActive(false);
        }

        m_inMenu = false;
    }
}
