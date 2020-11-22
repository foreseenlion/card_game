using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamInfo : MonoBehaviour
{


    public void setChampInfo(bool iswhite,string nameChamp, int hp, int dmg, string move, int movelimit, string powerDes)
    {
        if (iswhite)
           GetComponent<Text>().color = Color.blue;
        else GetComponent<Text>().color = new Color(0.3f, 0, 0);
        string name = HealthBarHandler.getChampName(nameChamp);
       GetComponent<Text>().text =
            name + "\n" +
            "Hp: " + hp + "\n" +
            "Dmg: " + dmg + "\n" +
            "Move: " + move + "\n" +
            "Move limit: " + movelimit + "\n" +
            "Power: " + powerDes;
    }
}
