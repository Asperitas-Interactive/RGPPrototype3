using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{
    public enum attackType
    {
        NORMAL,
        MIGHTY,
        EXPLOSIVE,
        STUN
    }

    public bool isAttacking = false;
    bool isStabbing = false;
    public Animator animator;

    public int comboCounter = 0;

    public int damage = 0;
    private int damageIncrease = 0;
    private CharacterController m_characterController;
    public bool canAttack;
    public Transform aoePos;

    //AOE values
    public float Radius = 5.0f;

    public AudioSource thrust;
    public AudioSource slash;

    public GameObject bullet;

    public PlayerMovement m_MovementScript;

    ShopDetection ShopCheck;

    public attackType StabType;
    public attackType ComboType;

    RankingSystem rankSys;

    bool m_moveForward;
    float m_moveTimer;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        m_MovementScript = GetComponent<PlayerMovement>();
        m_characterController = GetComponent<CharacterController>();
        ShopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
        rankSys = GetComponent<RankingSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!ShopCheck.inMenu)
        {
            m_moveTimer -= Time.deltaTime;

            if(m_moveForward && m_moveTimer<0f)
            {
                m_moveForward = false;
            }
            else if(m_moveForward)
            {
                m_characterController.Move(transform.forward * Time.deltaTime * 3.0f);
            }

            
            if (isAttacking == true)
            {
                m_MovementScript.m_isAttacking = true;
            }
            else
            {
                m_MovementScript.m_isAttacking = false;
            }
            float x = Input.GetAxis("Vertical");

            //This will make the stab attack happen

            //This will make the uppercut attack happen
            /*if (Input.GetButtonDown("Melee") && x < 0.0f && !isAttacking)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);

                animator.SetBool("RisingSlash", true);
                damage = 50 + damageIncrease;
            }*/

            //Stab
            /*if (Input.GetAxis("Vertical") > 0.0f && Input.GetButton("Vertical") && Input.GetButtonDown("Melee") && canAttack && !isAttacking)
            {
                isAttacking = true;
                animator.SetBool("isAttacking", true);

                animator.SetBool("Stab", true);
                damage = 30 + damageIncrease;

                thrust.Play();
            }*/
            //This will do the first hit of a combo
            if (Input.GetButtonDown("Melee") && canAttack && comboCounter == 0 && !isAttacking)
            {
                m_moveForward = true;
                m_moveTimer = 0.3f;
                animator.SetBool("isAttacking", true);
                comboCounter = 1;
                isAttacking = true;
                animator.SetInteger("combo", 1);
                damage = 30 + damageIncrease;
                slash.Play();
            }
            //This will do the second hit of the combo
            else if (Input.GetButtonDown("Melee") && canAttack && comboCounter == 1)
            {
                m_moveForward = true;
                m_moveTimer = 0.3f;
                animator.SetBool("isAttacking", true);

                comboCounter = 0;
                isAttacking = true;

                animator.SetInteger("combo", 2);
                damage = 40 + damageIncrease;
                slash.Play();
            }
            

            if (!isAttacking)
            {
                damage = 0;
                animator.SetInteger("combo", 0);
                animator.SetBool("RisingSlash", false);
                animator.SetBool("Stab", false);
                animator.SetBool("isAttacking", false);
            }

            //Special moves
            {
                //Downward plunge
                /*if (Input.GetButtonDown("AOE") && !m_MovementScript.isGrounded && !isAttacking)
                {
                    m_MovementScript.m_IsFalling = true;
                    isAttacking = true;
                    //Add animation code so we cant buffer a move during this
                }
                //stab
                /*else if (Input.GetButtonDown("AOE"))
                {
                    isAttacking = true;
                    animator.SetBool("isAttacking", true);

                    animator.SetBool("Stab", true);
                    damage = 30 + damageIncrease;

                    thrust.Play();
                }*/
            }

            //Debug.Log(isAttacking);
        }
    }

    public void DamageBoost(int Increase)
    {
        damageIncrease = Increase;
    }

    public void AttackEffect(EnemyControl collider)
    {
        if(animator.GetInteger("combo") > 0)
        {
            switch (ComboType)
            {
                case attackType.MIGHTY:
                    int random = Random.Range(1, 100);

                    if (random < 25)
                    {
                        //Want to add any increases in damage to the critical boost
                        collider.health -= (int)((damage + damageIncrease) * 1.5);
                        rankSys.increaseCombo();
                    }
                    else
                    {
                        collider.health -= damage + damageIncrease;
                    }
                    break;
                case attackType.EXPLOSIVE:
                    Collider[] objInRadius = Physics.OverlapSphere(collider.gameObject.transform.position, 5.0f);
                    foreach (Collider col in objInRadius)
                    {
                        if (col.gameObject.tag == "Enemy" && col.gameObject != collider.gameObject)
                        {
                            col.gameObject.GetComponent<EnemyControl>().health -= (int)((damage + damageIncrease) / 2);
                            rankSys.increaseCombo();
                        }
                    }

                    collider.health -= damage + damageIncrease;
                    break;
                case attackType.STUN:
                    Debug.Log("Inset a stun mechanic oops lol");
                    collider.health -= damage + damageIncrease;
                    break;
                //Default is for attackType.NORMAL
                default:
                    collider.health -= damage + damageIncrease;
                    break;
            }
        }
    }
}
