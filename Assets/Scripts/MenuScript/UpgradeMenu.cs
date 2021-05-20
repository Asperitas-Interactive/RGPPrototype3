using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    GameObject[] upgradeUI;
    bool menuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        upgradeUI = GameObject.FindGameObjectsWithTag("UpgradeUI");
        CloseUpgradeMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUpgradeMenu()
    {
        foreach(GameObject go in upgradeUI)
        {
            go.SetActive(true);
        }
        menuActive = true;
    }

    public void CloseUpgradeMenu()
    {
        foreach (GameObject go in upgradeUI)
        {
            go.SetActive(false);
        }
        menuActive = false;
    }

    public void OnClick()
    {
        if (menuActive)
        {
            Debug.Log("Playing this");
            CloseUpgradeMenu();
        } else
        {
            Debug.Log("Playing That");
            ShowUpgradeMenu();
        }
    }
}
