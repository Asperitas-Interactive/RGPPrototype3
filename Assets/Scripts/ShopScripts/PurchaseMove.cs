using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PurchaseMove : MonoBehaviour
{
    [SerializeField]
    private string DropdownText;
    //for the sprite
    [SerializeField]
    private Sprite DropdownSprite;

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

    void Purchase()
    {
        purchaseButton.interactable = false;
    }
}
