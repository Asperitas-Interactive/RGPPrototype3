using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public ShopDetection detector;

    public void CloseMenu()
    {
        detector.closeMenus();
    }

    public void NextLevel()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameOver();
    }
}
