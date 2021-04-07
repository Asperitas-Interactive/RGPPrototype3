using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{

    bool isStabbing;
    public Animator animator;
    public int damage = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical");

        //This will make the stab attack happen
        if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x > 0.0f && !isStabbing)
        {
            animator.SetTrigger("Stab");
            damage = 30;
        } 
        //This will make the uppercut attack happen
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x < 0.0f)
        {
            animator.SetTrigger("RisingSlash");
            damage = 50;
        } 
        //This will do the first hit of a combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank"))
        {
            animator.SetTrigger("SwordSlash");
            damage = 10;
        }
        //This will do the second hit of the combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash"))
        {
            animator.SetTrigger("SwordSlash2");
            damage = 20;
        }
        //This will do the final hit of the combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash2"))
        {
            animator.SetTrigger("SwordSlash3");
            damage = 30;
        } else
        {
            damage = 0;
        }


    }
}
