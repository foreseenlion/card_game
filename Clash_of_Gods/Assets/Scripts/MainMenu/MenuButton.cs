using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
	[SerializeField] Animator animator;

	public void select()
    {
		animator.SetBool("selected", true);
	}

	public void deselect()
    {
		animator.SetBool("selected", false);
	}

	public void press()
    {
		animator.SetBool("pressed", true);
	}

  //  void Update()
  //  {
		//if(menuButtonController.index == thisIndex)
		//{
		//	animator.SetBool ("selected", true);
		//	if(Input.GetAxis ("Submit") == 1)
		//	{	
		//		animator.SetBool ("pressed", true);
		//	}else if (animator.GetBool ("pressed")){
		//		animator.SetBool ("pressed", false);
		//		animatorFunctions.disableOnce = true;
		//	}
		//}else{
		//	animator.SetBool ("selected", false);
		//}
  //  }
}
