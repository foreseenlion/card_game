using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartWithGod : MonoBehaviour
{
 
  public void changeScene()
  {
    Debug.Log("akcja");
    SceneManager.LoadScene("game");
  }
}
