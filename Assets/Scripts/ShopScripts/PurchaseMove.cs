using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseMove : MonoBehaviour
{
    public enum activator
    {
        Mighty,
        Explosive,
        Stun
    }

    public activator activatortype;
    
    public AttackEquip equipScript;

    public float value;

    public MoneyController moneyController;

    private Button purchaseButton;
    // Start is called before the first frame update
    void Start()
    {
        purchaseButton = gameObject.GetComponent<Button>();
        moneyController = GameObject.FindGameObjectWithTag("Player").GetComponent<MoneyController>();
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    public void Purchase()
    {
        if (moneyController.Money >= value)
        {
            purchaseButton.interactable = false;
            purchaseButton.transform.GetChild(0).GetComponent<Text>().text = "Sold Out";
            equipScript.activatePowerup(activatortype);
            moneyController.TakeMoney(value);
        } else
        {
            Debug.Log("insufficient funds");
        }
    }
}
