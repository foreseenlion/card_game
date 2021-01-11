using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] Animator buttonAnimator;
	[SerializeField] Animator squareAnimator;
	[SerializeField] AudioSource audioSource;
	//[SerializeField] AudioClip click;
	[SerializeField] AudioClip enter;

	public void select()
    {
		buttonAnimator.SetBool("selected", true);
		squareAnimator.SetBool("selected", true);
		audioSource.PlayOneShot(enter);
	}

	public void deselect()
    {
		
		buttonAnimator.SetBool("selected", false);
		squareAnimator.SetBool("selected", false);
	}

	public void press()
    {
		buttonAnimator.SetBool("pressed", true);
		//audioSource.PlayOneShot(click);
	}
}
