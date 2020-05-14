using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose_God_Button : MonoBehaviour
{
    [SerializeField] Choose_God_ButtonController chooseGodButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions_ChooseGod animatorFunctionsChooseGod;
    [SerializeField] int thisIndexHorizontal,thisIndexVertical;

    void Update()
    {
        if (chooseGodButtonController.indexHorizontal == thisIndexHorizontal && chooseGodButtonController.indexVertical == thisIndexVertical)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunctionsChooseGod.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
        }
    }
}
