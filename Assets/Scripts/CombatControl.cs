using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{


    public bool isAttacking = false;
    bool isStabbing = false;
    public Animator animator;

    public int comboCounter = 0;

    public int damage = 0;
    private int damageIncrease = 0;
    public bool canAttack;
    float timer = 0.0f;
    bool canAOE = true;
    public Transform aoePos;

    //AOE values
    public float Radius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            canAOE = true;
        }

        float x = Input.GetAxis("Vertical");

        //This will make the stab attack happen
        if (Input.GetButtonDown("Melee") && x > 0.0f && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);

            animator.SetBool("Stab", true);
            damage = 30 + damageIncrease;
        }
        //This will make the uppercut attack happen
        /*if (Input.GetButtonDown("Melee") && x < 0.0f && !isAttacking)
        {
            isAttacking = true;
            animator.SetBool("isAttacking", true);

            animator.SetBool("RisingSlash", true);
            damage = 50 + damageIncrease;
        }*/


        //This will do the first hit of a combo
        if (Input.GetButtonDown("Melee") && canAttack && comboCounter == 0 && !isAttacking)
        {
            animator.SetBool("isAttacking", true);
            comboCounter = 1;
            isAttacking = true;
            animator.SetInteger("combo", 1);
            damage = 30 + damageIncrease;
        }
        //This will do the second hit of the combo
        else if (Input.GetButtonDown("Melee") && canAttack && comboCounter == 1)
        {
            animator.SetBool("isAttacking", true);

            comboCounter = 2;
            isAttacking = true;

            animator.SetInteger("combo", 2);
            damage = 40 + damageIncrease;
        }
        //This will do the final hit of the combo
        else if (Input.GetButtonDown("Melee") && canAttack && comboCounter == 2)
        {
            animator.SetBool("isAttacking", true);

            comboCounter = 0;
            isAttacking = true;

            animator.SetInteger("combo", 3);
            damage = 50 + damageIncrease;
        }

        if (!isAttacking)
        {
            damage = 0;
            animator.SetInteger("combo", 0);
            animator.SetBool("RisingSlash", false);
            animator.SetBool("Stab", false);
            animator.SetBool("isAttacking", false);
        }


        //AOE MOVE
        if (canAOE == true)
        {
            if (Input.GetButtonDown("AOE"))
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

                for (int i = 0; i < enemies.Length; i++)
                {
                    if (Radius >= Vector3.Distance(aoePos.position, enemies[i].transform.position))
                    {
                        enemies[i].GetComponent<EnemyControl>().AOEDamage();
                        canAOE = false;
                        timer = 30.0f;
                    }
                }
            }
        }
    }

    public void DamageBoost(int Increase)
    {
        damageIncrease += Increase;
    }
}
