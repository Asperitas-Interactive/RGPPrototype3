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
    public bool isGrounded;

    private int m_health = 10000;
    private int MaxHealth = 100;

    public Slider slider;
    public Image viginette;

    public bool m_IsFalling = false;

    public GameObject[] bodyToRotate;

    public bool m_isAttacking = false;

    public float velZ;
    ShopDetection ShopCheck;

    // Start is called before the first frame update
    void Start()
    {
        ShopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }

        slider.value = m_health;

        if(m_health <= 0)
        {
            viginette.GetComponent<FadeOut>().beginFade();
        }

        float velX = Input.GetAxis("Horizontal");
        velZ = Input.GetAxis("Vertical");

        bool jumpPress = Input.GetButtonDown("Jump");

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0.0f;
        }

        Vector3 movement = transform.right * velX + transform.forward * velZ;

        //I'll try this later
        /*Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Debug.Log(NextDir);
        foreach (GameObject go in bodyToRotate)
        {
            //go.transform.rotation = Quaternion.LookRotation(NextDir);
        }*/
        if (m_isAttacking == false)
        {
            controller.Move(movement * speed * Time.deltaTime);
        }

        if(jumpPress && isGrounded)
        if (!ShopCheck.inMenu)
        {
            slider.value = health;

            if (health <= 0)
            {
                viginette.GetComponent<FadeOut>().beginFade();
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

            //I'll try this later
            /*Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            Debug.Log(NextDir);
            foreach (GameObject go in bodyToRotate)
            {
                //go.transform.rotation = Quaternion.LookRotation(NextDir);
            }*/
            if (m_isAttacking == false)
            {
                controller.Move(movement * speed * Time.deltaTime);
            }

            if (jumpPress && isGrounded)
            {
                velocity.y = Mathf.Sqrt(-jumpHeight * 2f * -gravity);
                m_IsFalling = false;
            }

            if (m_IsFalling && !isGrounded)
            {
                velocity.y += gravity * 10 * Time.deltaTime;
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }

            controller.Move(velocity * Time.deltaTime);
        }
    }



    public void Heal(int recovery)
    {
        //m_health = (int)Mathf.Clamp(m_health + recovery, 0, MaxHealth);
    }

    public void MaxHealthUp(int increase)
    {
        if (MaxHealth < 500)
        {
            MaxHealth += increase;
            slider.maxValue = MaxHealth;
            m_health += increase;
        }
    }

    public int getHealth()
    {
        return m_health;
    }

    public int getMaxHP()
    {
        return MaxHealth;
    }
}
