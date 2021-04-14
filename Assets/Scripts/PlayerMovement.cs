using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12.0f;
    public float gravity = -20.0f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    private int health = 100;
    private int MaxHealth = 100;

    public Slider slider;
    public Image viginette;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        slider.value = health;

        if(health <= 0)
        {
            viginette.GetComponent<FadeOut>().beginFade();
            //GameObject.FindGameObjectWithTag("Manager").GetComponent<gameManager>().gameLost();
        }

        float velX = Input.GetAxis("Horizontal");
        float velZ = Input.GetAxis("Vertical");

        bool jumpPress = Input.GetButtonDown("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0.0f;
        }

        Vector3 movement = transform.right * velX + transform.forward * velZ;

        controller.Move(movement * speed * Time.deltaTime);

        if(jumpPress && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }



    public void Heal(int recovery)
    {
        health = (int)Mathf.Clamp(health + recovery, 0, MaxHealth);
    }

    public void MaxHealthUp(int increase)
    {
        MaxHealth += increase;
        slider.maxValue = MaxHealth;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHP()
    {
        return MaxHealth;
    }
}
