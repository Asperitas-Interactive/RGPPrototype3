using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [FormerlySerializedAs("controller")] public CharacterController m_controller;
    [FormerlySerializedAs("speed")] public float m_speed = 12.0f;
    [FormerlySerializedAs("gravity")] public float m_gravity = -20.0f;
    [FormerlySerializedAs("jumpHeight")] public float m_jumpHeight = 2f;

    [FormerlySerializedAs("groundCheck")] public Transform m_groundCheck;
    [FormerlySerializedAs("groundDistance")] public float m_groundDistance = 0.4f;
    [FormerlySerializedAs("groundMask")] public LayerMask m_groundMask;

    public Transform m_playerCam;

    [FormerlySerializedAs("animator")] public Animator m_animator;

    Vector3 m_velocity;
    [FormerlySerializedAs("isGrounded")] public bool m_isGrounded;

    private int m_health = 100;
    private int m_maxHealth = 100;

    [FormerlySerializedAs("slider")] public Slider m_slider;
    public Image m_viginette;

    [FormerlySerializedAs("m_IsFalling")] public bool m_isFalling = false;

    [FormerlySerializedAs("bodyToRotate")] public GameObject[] m_bodyToRotate;

    public bool m_isAttacking = false;

    [FormerlySerializedAs("velZ")] public float m_velZ;
    ShopDetection m_shopCheck;
    private FadeOut m_fadeOut;
    private static readonly int Speed = Animator.StringToHash("speed");
    private CombatControl m_combatControl;

    // Start is called before the first frame update
    void Start()
    {
        m_combatControl = GetComponent<CombatControl>();
        m_fadeOut = m_viginette.GetComponent<FadeOut>();
        m_shopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }


        if (m_shopCheck == null)
        {
            m_shopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
        }

        if (!m_shopCheck.m_inMenu)
        {
            m_slider.value = m_health;

            if (m_health <= 0)
            {
                m_fadeOut.BeginFade();
            }

            float velX = Input.GetAxisRaw("Horizontal");
            m_velZ = Input.GetAxisRaw("Vertical");

            bool jumpPress = Input.GetButtonDown("Jump");

            m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

            if (m_isGrounded && m_velocity.y < 0)
            {
                m_velocity.y = 0.0f;
            }

            Vector3 movement = new Vector3(velX, 0f, m_velZ);

            if(movement.magnitude > 0.1f)
            {
                m_animator.SetFloat(Speed, movement.magnitude);
            }
            else
            {
                m_animator.SetFloat(Speed, 0f);
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
                m_controller.Move(moveDir.normalized * (m_speed * Time.deltaTime));
            }
            else if(movement.magnitude > 0.1f && m_combatControl.m_canAttack && Input.GetButtonDown("Melee"))
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg + m_playerCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }

            if (m_isFalling && !m_isGrounded)
            {
                m_velocity.y += m_gravity * 10 * Time.deltaTime;
            }
            else
            {
                m_velocity.y += m_gravity * Time.deltaTime;
            }

            m_controller.Move(m_velocity * Time.deltaTime);
        }
    }



    public void Heal(int _recovery)
    {
        m_health = (int)Mathf.Clamp(m_health + _recovery, 0, m_maxHealth);
    }

    public void MAXHealthUp(int _increase)
    {
        if (m_maxHealth < 500)
        {
            m_maxHealth += _increase;
            m_slider.maxValue = m_maxHealth;
            m_health += _increase;
        }
    }

    public int GETHealth()
    {
        return m_health;
    }

    public int GETMaxHp()
    {
        return m_maxHealth;
    }
}
