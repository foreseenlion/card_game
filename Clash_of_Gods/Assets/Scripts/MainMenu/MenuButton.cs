using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] Animator buttonAnimator;
	[SerializeField] Animator squareAnimator;

	public void select()
    {
		buttonAnimator.SetBool("selected", true);
		squareAnimator.SetBool("selected", true);
	}

	public void deselect()
    {
		buttonAnimator.SetBool("selected", false);
		squareAnimator.SetBool("selected", false);
	}

	public void press()
    {
		buttonAnimator.SetBool("pressed", true);
	}
}
