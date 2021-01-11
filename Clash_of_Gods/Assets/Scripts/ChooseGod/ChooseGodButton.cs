using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseGodButton : MonoBehaviour
{
	[SerializeField] Animator buttonAnimator;
	[SerializeField] string religionId;
	[SerializeField] string description;
	[SerializeField] GameObject descriptionField;
	[SerializeField] Image avatarImage;
	[SerializeField] Sprite swapImage;
	[SerializeField] SpriteRenderer backgroundImage;
	[SerializeField] Sprite swapBackground;
	[SerializeField] AudioSource audioSource;
	//[SerializeField] AudioClip click;
	[SerializeField] AudioClip enter;

	public void select()
	{
		buttonAnimator.SetBool("selected", true);
		audioSource.PlayOneShot(enter);
		descriptionField.GetComponent<Text>().text = description;
		avatarImage.sprite = swapImage;
		backgroundImage.sprite = swapBackground;
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

	public void setReligion()
	{
		myReligion.religion = religionId;
	}
}
