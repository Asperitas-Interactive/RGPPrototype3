using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgrade : MonoBehaviour
{
    public CombatControl cc;
    [SerializeField]
    private int increase;

    Button selfButton;

    public GameObject successorButton;
    public GameObject successorText;
    // Start is called before the first frame update
    void Start()
    {
        selfButton = GetComponent<Button>();
        if (successorButton != null)
        {
            successorButton.gameObject.SetActive(false);
        }
        if (successorText != null)
        {
            successorText.gameObject.SetActive(false);
        }
    }

    public void OnClick()
    {
        selfButton.interactable = false;
        selfButton.transform.GetChild(0).GetComponent<Text>().text = "Sold Out";
        cc.DamageBoost(increase);
        if (successorButton != null)
        {
            successorButton.gameObject.SetActive(true);
        }
        if (successorText != null)
        {
            successorText.gameObject.SetActive(true);
        }

    }
}