using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsHendling : MonoBehaviour
{

    private MeterialChanges materialChange;//tło
    public void DmgAnimation(ChessMan target)
    {
        target.GetComponent<Animator>().runtimeAnimatorController = EmptyAnimator;
        target.GetComponent<Animator>().runtimeAnimatorController = DmgAnimator;

    }
    private void Start()
    {
        materialChange = GameObject.FindObjectOfType<MeterialChanges>(); //tło
    }

    private void Update()
    {
        materialChange.changeSmooth();// tło
    }


    public RuntimeAnimatorController DeadAnimator;
    public RuntimeAnimatorController DmgAnimator;
    public RuntimeAnimatorController EmptyAnimator;

}
