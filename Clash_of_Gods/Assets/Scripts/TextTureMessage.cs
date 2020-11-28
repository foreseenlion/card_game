using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTureMessage : MonoBehaviour
{
   public void setTureInfo(bool isYou, bool isWhite, int moveLeft )
    {
        string result="";
        if (isYou)
            result += "Your turn";
        else result += "Enemy turn";
        if (isWhite)
            GetComponent<Text>().color = Color.blue;
        else GetComponent<Text>().color = new Color(0.3f, 0, 0);
        result += "\nMoves left: " + moveLeft;
        GetComponent<Text>().text = result;
    }
}
