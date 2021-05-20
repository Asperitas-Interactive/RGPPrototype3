using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDetection : MonoBehaviour
{
    public Button Endbutton;
    public Button UpgradeButton;
    public Image prompt;
    public bool inZone;
    public bool inMenu;
    public GameObject player;
    public GameObject camera;
    public waveManager waveManager;

    GameObject[] PlayerUIObjects;
    GameObject[] ShopUIObjects;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.gameObject;

        Endbutton.gameObject.SetActive(false);

        PlayerUIObjects = GameObject.FindGameObjectsWithTag("PlayerUI");
        ShopUIObjects = GameObject.FindGameObjectsWithTag("ShopUI");

        foreach (GameObject go in PlayerUIObjects)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in ShopUIObjects)
        {
            go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("AOE") && inZone == true)
        {
            if (inMenu == false)
            {
                Cursor.lockState = CursorLockMode.None;

                if (waveManager.m_CombatEnded)
                {
                    Endbutton.gameObject.SetActive(true);
                }

                foreach (GameObject go in PlayerUIObjects)
                {
                    go.SetActive(false);
                }
                foreach(GameObject go in ShopUIObjects)
                {
                    go.SetActive(true);
                }
                
                inMenu = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            inZone = true;
        }
    }

    public void closeMenus()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UpgradeButton.GetComponent<UpgradeMenu>().CloseUpgradeMenu();
        Endbutton.gameObject.SetActive(false);
        foreach (GameObject go in PlayerUIObjects)
        {
            go.SetActive(true);
        }
        foreach (GameObject go in ShopUIObjects)
        {
            go.SetActive(false);
        }

        inMenu = false;
    }
}
