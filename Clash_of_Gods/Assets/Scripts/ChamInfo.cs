using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamInfo : MonoBehaviour
{


    public void setChampInfo(bool iswhite,string nameChamp, int hp, int dmg, string move, int movelimit, string powerDes, List<Effects> effects)
    {
        string activeEfect = "";
        if (effects.Count>0)
            if (activeEfect == "")
                activeEfect = "Effects: \n";
        foreach (Effects effect in effects)
            {
            if (effect.length != 1)
                activeEfect += effect.name + " " + effect.length + " more turns\n"+ effect.description+"\n";
            else
                activeEfect += effect.name + " " + effect.length + " more turn\n"+ effect.description + "\n";
            }

        if (iswhite)
           GetComponent<Text>().color = Color.blue;
        else GetComponent<Text>().color = new Color(0.3f, 0, 0);
        string name = HealthBarHandler.getChampName(nameChamp);
        string nameLine = CheckIfTheVariableHasAValue(name + "\n",name);
        string HpLine = CheckIfTheVariableHasAValue("Hp: " + hp + "\n",hp);
        string dmgLine = CheckIfTheVariableHasAValue("Dmg: " + dmg + "\n",dmg);
        string moveLine = CheckIfTheVariableHasAValue("Move: " + move + "\n",move);
        string movelimitLine = CheckIfTheVariableHasAValue("Move limit: " + movelimit + "\n",movelimit);
        string powerDesLine = CheckIfTheVariableHasAValue("Power: " + powerDes, powerDes);

        GetComponent<Text>().text = nameLine + HpLine + dmgLine + moveLine + movelimitLine + activeEfect + powerDesLine;
    }

    public string CheckIfTheVariableHasAValue(string value, string valueToCheck)
    {
        if (valueToCheck == null|| valueToCheck=="")
            return "";
        else return value;
    }
    public string CheckIfTheVariableHasAValue(string value, int valueToCheck)
    {
        if (valueToCheck == -1 || valueToCheck == 0 || valueToCheck == null)
            return "";
        else return value;
    }

}
