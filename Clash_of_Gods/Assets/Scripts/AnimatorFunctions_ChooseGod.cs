using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunctions_ChooseGod : MonoBehaviour
{
    [SerializeField] Choose_God_ButtonController chooseGodButtonController;
    public bool disableOnce;

    void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            chooseGodButtonController.audioSource.PlayOneShot(whichSound);
        }
        else
        {
            disableOnce = false;
        }
    }
}
