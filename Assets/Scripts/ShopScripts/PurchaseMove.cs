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

    private Button purchaseButton;
    // Start is called before the first frame update
    void Start()
    {
        purchaseButton = gameObject.GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Purchase()
    {
        purchaseButton.interactable = false;
        purchaseButton.transform.GetChild(0).GetComponent<Text>().text = "Sold Out";
        equipScript.activatePowerup(activatortype);
    }
}
