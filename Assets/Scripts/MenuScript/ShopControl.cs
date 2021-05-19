using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{
    public void Upgrade()
    {
        Debug.Log("this will go to a menu");
    }

    public void NextLevel()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameOver();
    }
}
