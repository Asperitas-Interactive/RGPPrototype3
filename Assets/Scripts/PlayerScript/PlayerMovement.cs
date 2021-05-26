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

    public Transform m_playerCam;

    public Animator animator;

    Vector3 velocity;
    public bool isGrounded;

    private int m_health = 100;
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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }



        if (!ShopCheck.inMenu)
        {
            slider.value = m_health;

            if (m_health <= 0)
            {
                viginette.GetComponent<FadeOut>().beginFade();
            }

            float velX = Input.GetAxisRaw("Horizontal");
            velZ = Input.GetAxisRaw("Vertical");

            bool jumpPress = Input.GetButtonDown("Jump");

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = 0.0f;
            }

            Vector3 movement = new Vector3(velX, 0f, velZ);

            if(movement.magnitude > 0.1f)
            {
                animator.SetFloat("speed", movement.magnitude);
            }
            else
            {
                animator.SetFloat("speed", 0f);
            }


            //I'll try this later
            /*Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
            Debug.Log(NextDir);
            foreach (GameObject go in bodyToRotate)
            {
                //go.transform.rotation = Quaternion.LookRotation(NextDir);
            }*/
            if (m_isAttacking == false && movement.magnitude > 0.1f)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else if(movement.magnitude > 0.1f && GetComponent<CombatControl>().canAttack && Input.GetButtonDown("Melee"))
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
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
        m_health = (int)Mathf.Clamp(m_health + recovery, 0, MaxHealth);
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
