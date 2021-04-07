using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical");

        //This will make the stab attack happen
        if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x > 0.0f)
        {
            animator.SetTrigger("Stab");
        } 
        //This will make the uppercut attack happen
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x < 0.0f)
        {
            animator.SetTrigger("RisingSlash");
        } 
        //This will do the first hit of a combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank"))
        {
            animator.SetTrigger("SwordSlash");
        }
        //This will do the second hit of the combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash"))
        {
            animator.SetTrigger("SwordSlash2");
        }
        //This will do the final hit of the combo
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash2"))
        {
            animator.SetTrigger("SwordSlash3");
        }


    }
}
