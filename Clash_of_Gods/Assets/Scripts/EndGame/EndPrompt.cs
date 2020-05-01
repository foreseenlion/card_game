using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EndPrompt : MonoBehaviour
{
    
    
    void Start()
    {
        string message = "Game over! \n" + "The winner is: " + GameManager.instance.Winner + "\n"
            + "You won by " + GameManager.instance.Condition + "\n\n" + "CONGRATULATIONS!";
        GetComponent<Text>().text = message;
    }

    
}
