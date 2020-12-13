using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class endgame : MonoBehaviour
{
    public Material Material1;
    public Material Material2;
    //in the editor this is what you would set as the object you wan't to change
    public GameObject Object;
    private float value = 0;
    private bool on = true;
    public GameObject Text;

    public Button btn;
    void Start()
    {
        if (myReligion.youWin)
        {
            Object.GetComponent<MeshRenderer>().material = Material1;
            Text.GetComponent<Text>().text = "You Lost";
        }
        else
        {
            Object.GetComponent<MeshRenderer>().material = Material2;
            Text.GetComponent<Text>().text = "You Win";
        }  
        btn = FindObjectOfType<Button>();
        btn.onClick.AddListener(goToMenu);
        statMenager();
    }

    void Update()
{
        if (on)
        {
            value = value + 0.002f;
            if (value >= 1)
                on = false;
        }
        Object.GetComponent<MeshRenderer>().material.SetFloat("_GlossMapScale", value);
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("Choose_God");
    }


    public void statMenager()
    {
        Mystat mystat = new Mystat(0,0,0,0,0,0,0,0,0,0,0);
        mystat.gameCount = 1;
        if (!myReligion.youWin)
            mystat.winCount = 1;
        else mystat.lossCount = 1;

        switch (myReligion.religion)
        {
            case "G":
                {
                    mystat.grecjaCount = 1;
                    break;
                }
            case "S":
                {
                    mystat.slavCount = 1;
                    break;
                }
            case "N":
                {
                    mystat.nordCount = 1;
                    break;
                }
            case "E":
                {
                    mystat.egipCount = 1;
                    break;
                }
        }

        switch (myReligion.enemyReligion)
        {
            case "G":
                {
                    mystat.grecjaEnemyCount = 1;
                    break;
                }
            case "S":
                {
                    mystat.slavEnemyCount = 1;
                    break;
                }
            case "N":
                {
                    mystat.nordEnemyCount = 1;
                    break;
                }
            case "E":
                {
                    mystat.egipEnemyCount = 1;
                    break;
                }
        }
        stat.SaveStat(mystat);

    }
    


}
