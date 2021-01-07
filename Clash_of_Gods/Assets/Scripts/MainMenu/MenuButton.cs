using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] Animator buttonAnimator;
	[SerializeField] Animator squareAnimator;

	public void select()
    {
		Debug.Log("s");
		buttonAnimator.SetBool("selected", true);
		Debug.Log("ss");
		squareAnimator.SetBool("selected", true);
		Debug.Log("sss");
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
