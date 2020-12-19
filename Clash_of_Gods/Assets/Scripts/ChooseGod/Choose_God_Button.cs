using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Choose_God_Button : MonoBehaviour
{
    [SerializeField] Choose_God_ButtonController chooseGodButtonController;
    [SerializeField] Animator animator;
    [SerializeField] AnimatorFunctions_ChooseGod animatorFunctionsChooseGod;
    [SerializeField] int thisIndexHorizontal,thisIndexVertical;
    public string religionId;

    void Update()
    {
        if (chooseGodButtonController.indexHorizontal == thisIndexHorizontal && chooseGodButtonController.indexVertical == thisIndexVertical)
        {
            animator.SetBool("selected", true);
            if (Input.GetAxis("Submit") == 1)
            {
                myReligion.religion = religionId;
                SceneManager.LoadScene("game");
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
