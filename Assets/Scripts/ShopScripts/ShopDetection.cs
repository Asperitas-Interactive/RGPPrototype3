using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDetection : MonoBehaviour
{
    public Button[] buttons;
    public Image prompt;
    public bool inZone;
    public bool inMenu;
    public GameObject player;
    public GameObject camera;
    public waveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.gameObject;
        Cursor.lockState = CursorLockMode.None;

        buttons[0].gameObject.SetActive(false);
        buttons[1].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("AOE") && inZone == true)
        {
            if (inMenu == false)
            {
                Cursor.lockState = CursorLockMode.None;

                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<CombatControl>().enabled = false;
                camera.GetComponent<ThirdPersonCam>().enabled = false;

                if (waveManager.m_CombatEnded)
                {
                    buttons[0].gameObject.SetActive(true);
                }

                buttons[1].gameObject.SetActive(true);
                
                inMenu = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;

                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<CombatControl>().enabled = true;
                camera.GetComponent<ThirdPersonCam>().enabled = true;

                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(false);

                inMenu = false;
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
}
