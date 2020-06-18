using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    string winner;
    string condition;

    public string Winner { get => winner; set => winner = value; }
    public string Condition { get => condition; set => condition = value; }


    void Awake()
    {
        
        if (instance == null)
        {
            
            instance = this;
        }
        else if (instance != this)
        {
            
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
