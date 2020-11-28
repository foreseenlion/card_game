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

}
