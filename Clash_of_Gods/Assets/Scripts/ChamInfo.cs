using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChamInfo : MonoBehaviour
{
    void infinityLength()
    {

    }

    public void setImage(Sprite move, Sprite attack)
    {
        try {
            SpriteRenderer m_SpriteRenderer = BoardManager.Instance.moveImage.GetComponent<SpriteRenderer>();
            SpriteRenderer a_SpriteRenderer = BoardManager.Instance.imageAttack.GetComponent<SpriteRenderer>();

            m_SpriteRenderer.sprite = move;
            a_SpriteRenderer.sprite = attack;
        } catch { }
        
    }


    public void setChampInfo(bool iswhite, string nameChamp, int hp, int dmg, int movelimit, string powerDes, List<Effects> effects, Sprite move, Sprite attack)
    {
        string activeEfect = "";
        if (effects.Count > 0)
            if (activeEfect == "")
                activeEfect = "Effects: \n";
        foreach (Effects effect in effects)
        {
            if (effect.length != -1)
            {
                if (effect.length != 1)
                    activeEfect += effect.name + " " + effect.length + " more turns\n" + effect.description + "\n";
                else
                    activeEfect += effect.name + " " + effect.length + " more turn\n" + effect.description + "\n";
            }
            else activeEfect += effect.name + "\n";
        }


        if (iswhite)
            GetComponent<Text>().color = Color.blue;
        else GetComponent<Text>().color = new Color(0.3f, 0, 0);
        string name = HealthBarHandler.getChampName(nameChamp);
        string nameLine = CheckIfTheVariableHasAValue(name + "\n", name);
        string HpLine = CheckIfTheVariableHasAValue("Hp: " + hp + "\n", hp);
        string dmgLine = "Dmg: " + dmg + "\n";
        string movelimitLine = CheckIfTheVariableHasAValue("Move limit: " + movelimit + "\n", movelimit);
        string powerDesLine = CheckIfTheVariableHasAValue("Power: " + powerDes, powerDes);

        setImage(move,attack);
        GetComponent<Text>().text = nameLine + HpLine + dmgLine + movelimitLine + activeEfect + powerDesLine;
    }

    public string CheckIfTheVariableHasAValue(string value, string valueToCheck)
    {
        if (valueToCheck == null || valueToCheck == "")
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
