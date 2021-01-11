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

	public void select()
	{
		buttonAnimator.SetBool("selected", true);
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
	}

	public void setReligion()
	{
		myReligion.religion = religionId;
	}
}
