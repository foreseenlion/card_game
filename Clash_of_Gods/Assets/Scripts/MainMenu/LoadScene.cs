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

}
