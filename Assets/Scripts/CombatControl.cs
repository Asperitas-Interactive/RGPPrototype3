using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl : MonoBehaviour
{
    int comboCounter = 0;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x > 0.0f)
        {
            animator.SetTrigger("Stab");
        } 
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank") && x < 0.0f)
        {
            animator.SetTrigger("RisingSlash");
        } 
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("Blank"))
        {
            animator.SetTrigger("SwordSlash");
        }
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash") && comboCounter == 0)
        {
            comboCounter = 1;
            animator.SetTrigger("SwordSlash2");
        }
        else if (Input.GetButtonDown("Melee") && animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSlash2") && comboCounter == 1)
        {
            comboCounter = 0;
            animator.SetTrigger("SwordSlash3");
        }


    }
}
