using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    GameObject[] m_upgradeUI;
    bool m_menuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        m_upgradeUI = GameObject.FindGameObjectsWithTag("UpgradeUI");
        CloseUpgradeMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUpgradeMenu()
    {
        foreach(GameObject go in m_upgradeUI)
        {
            go.SetActive(true);
        }
        m_menuActive = true;
    }

    public void CloseUpgradeMenu()
    {
        foreach (GameObject go in m_upgradeUI)
        {
            go.SetActive(false);
        }
        m_menuActive = false;
    }

    public void OnClick()
    {
        if (m_menuActive)
        {
            CloseUpgradeMenu();
        } else
        {
            ShowUpgradeMenu();
        }
    }
}
