using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void loadGodScene()
    {
        SceneManager.LoadScene("Choose_God");
    }

    public void loadStatsScene()
    {
        SceneManager.LoadScene("Statistics_Menu");
    }

    public void loadAboutScene()
    {
        SceneManager.LoadScene("About");
    }

    public void loadMainMenuScene()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void loadGame()
    {
        SceneManager.LoadScene("game");
    }

}
