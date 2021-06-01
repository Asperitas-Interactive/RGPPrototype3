using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatControl : MonoBehaviour
{
    public enum eAttackType
    {
        Normal,
        Mighty,
        Explosive,
        Stun
    }

    private bool m_isCountering = false;
    private float m_CounterTimer = 0f;

    [FormerlySerializedAs("isAttacking")] public bool m_isAttacking = false;
    private bool m_isStabbing = false;
    [FormerlySerializedAs("animator")] public Animator m_animator;

    [FormerlySerializedAs("comboCounter")] public int m_comboCounter = 0;

    [FormerlySerializedAs("damage")] public int m_damage = 0;
    private int m_damageIncrease = 0;
    private CharacterController m_characterController;
    [FormerlySerializedAs("canAttack")] public bool m_canAttack;
    [FormerlySerializedAs("aoePos")] public Transform m_aoePos;

    //AOE values
    [FormerlySerializedAs("Radius")] public float m_radius = 5.0f;

    [FormerlySerializedAs("thrust")] public AudioSource m_thrust;
    [FormerlySerializedAs("slash")] public AudioSource m_slash;

    [FormerlySerializedAs("bullet")] public GameObject m_bullet;

    [FormerlySerializedAs("m_MovementScript")] public PlayerMovement m_movementScript;

    ShopDetection m_shopCheck;

    [FormerlySerializedAs("StabType")] public eAttackType m_stabType;
    [FormerlySerializedAs("ComboType")] public eAttackType m_comboType;

    RankingSystem m_rankSys;

    bool m_moveForward;
    float m_moveTimer;
    public bool m_canDamage;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int Combo = Animator.StringToHash("combo");
    private static readonly int RisingSlash = Animator.StringToHash("RisingSlash");
    private static readonly int Stab = Animator.StringToHash("Stab");

    // Start is called before the first frame update
    void Start()
    {
        m_canDamage = false;

        //animator = GetComponent<Animator>();
        m_movementScript = GetComponent<PlayerMovement>();
        m_characterController = GetComponent<CharacterController>();
        m_shopCheck = GameObject.FindGameObjectWithTag("ShopEvent").GetComponent<ShopDetection>();
        m_rankSys = GetComponent<RankingSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_isCountering && m_CounterTimer<0f)
        {
            m_isCountering = false;
        }
        if (!m_shopCheck.m_inMenu)
        {
            m_moveTimer -= Time.deltaTime;

            if(m_moveForward && m_moveTimer<0f)
            {
                m_moveForward = false;
            }
            else if(m_moveForward)
            {
                m_characterController.Move(transform.forward * (Time.deltaTime * 3.0f));
            }

            
            if (m_isAttacking == true)
            {
                m_movementScript.m_isAttacking = true;
            }
            else
            {
                m_movementScript.m_isAttacking = false;
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
            if (Input.GetButtonDown("Melee") && m_canAttack && m_comboCounter == 0 && !m_isAttacking)
            {
                Hit1();
            }
            //This will do the second hit of the combo
            else if (Input.GetButtonDown("Melee") && m_canAttack && m_comboCounter == 1)
            {
                Hit2();
            }
            

            if (!m_isAttacking)
            {
                m_damage = 0;
                m_animator.SetInteger(Combo, 0);
                m_animator.SetBool(RisingSlash, false);
                m_animator.SetBool(Stab, false);
                m_animator.SetBool(IsAttacking, false);
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

    private void Hit2()
    {
        m_moveForward = true;
        m_moveTimer = 0.3f;
        m_animator.SetBool(IsAttacking, true);

        m_comboCounter = 0;
        m_isAttacking = true;

        m_animator.SetInteger(Combo, 2);
        m_damage = 40 + m_damageIncrease;
        m_slash.Play();
    }

    private void Hit1()
    {
        m_moveForward = true;
        m_moveTimer = 0.3f;
        m_animator.SetBool(IsAttacking, true);
        m_comboCounter = 1;
        m_isAttacking = true;
        m_animator.SetInteger(Combo, 1);
        m_damage = 30 + m_damageIncrease;
        m_slash.Play();
    }

    public void DamageBoost(int _increase)
    {
        m_damageIncrease = _increase;
    }

    public void AttackEffect(EnemyControl _collider)
    {
        if(m_animator.GetInteger(Combo) > 0)
        {
            switch (m_comboType)
            {
                case eAttackType.Mighty:
                    int random = Random.Range(1, 100);

                    if (random < 25)
                    {
                        //Want to add any increases in damage to the critical boost
                        _collider.m_health -= (int)((m_damage + m_damageIncrease) * 1.5);
                        m_rankSys.IncreaseCombo();
                    }
                    else
                    {
                        _collider.m_health -= m_damage + m_damageIncrease;
                    }
                    break;
                case eAttackType.Explosive:
                    Collider[] objInRadius = Physics.OverlapSphere(_collider.gameObject.transform.position, 5.0f);
                    foreach (Collider col in objInRadius)
                    {
                        if (col.gameObject.CompareTag("Enemy") && col.gameObject != _collider.gameObject)
                        {
                            col.gameObject.GetComponent<EnemyControl>().m_health -= (int)((m_damage + m_damageIncrease) / 2);
                            m_rankSys.IncreaseCombo();
                        }
                    }

                    _collider.m_health -= m_damage + m_damageIncrease;
                    break;
                case eAttackType.Stun:
                    Debug.Log("Inset a stun mechanic oops lol");
                    _collider.m_health -= m_damage + m_damageIncrease;
                    break;
                //Default is for attackType.NORMAL
                default:
                    _collider.m_health -= m_damage + m_damageIncrease;
                    break;
            }
        }
    }

    public bool Counter(Transform _enemy)
    {
        if (!m_isCountering && Input.GetButtonDown("Counter"))
        {
            transform.LookAt(_enemy);
            Hit1();
            return true;

        }
        else return false;
    }

    public bool Damage()
    {
        m_canDamage = true;
        return true;
    }

    public void UnDamage()
    {
        m_canDamage = false;
    }
}
