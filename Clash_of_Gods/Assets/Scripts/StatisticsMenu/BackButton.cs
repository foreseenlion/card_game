using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
	[SerializeField] Animator buttonAnimator;
	[SerializeField] AudioSource audioSource;
	//[SerializeField] AudioClip click;
	[SerializeField] AudioClip enter;

	public void select()
	{
		buttonAnimator.SetBool("selected", true);
		audioSource.PlayOneShot(enter);
	}

	public void deselect()
	{

		buttonAnimator.SetBool("selected", false);
	}

	public void press()
	{
		buttonAnimator.SetBool("pressed", true);
		//audioSource.PlayOneShot(click);
	}
}
