using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPText : MonoBehaviour
{
    private PlayerMovement player;

    public Text currentHealth;
    public Text maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        int health = (int)player.getHealth();
        currentHealth.text = health.ToString();
        maxHealth.text = player.getMaxHP().ToString();
    }
}
