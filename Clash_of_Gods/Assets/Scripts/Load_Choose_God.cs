using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Choose_God : MonoBehaviour
{
    public void changeScene()
    {
        SceneManager.LoadScene("Choose_God");
    }
    public void changeSceneState()
    {
        SceneManager.LoadScene("Statistics_Menu");
    }
}
