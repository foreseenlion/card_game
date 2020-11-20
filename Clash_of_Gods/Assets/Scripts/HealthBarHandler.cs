using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
    private int hpMax=-1;

    public bool MainGod=false;
    public GameObject text;
    private string champName;
    private string hpNow;


    /// <summary>
    /// Sets the health bar value
    /// </summary>
    // <param name="value">should be between 0 to 1</param>
    public  void setColor(float value)
    {

        if (value < 0.4f)
        {
            if (!MainGod)
                SetHealthBarColor(Color.red);
            else SetHealthBarColor(new Color(0.3f, 0, 0));
        }
        else if (value < 0.8f)
        {
            if (!MainGod)
                SetHealthBarColor(Color.yellow);
            else SetHealthBarColor(new Color(0, 0.5f, 0));
        }
        else
        {
            if (!MainGod)
                SetHealthBarColor(new Color(0, 0.7f, 0));
            else SetHealthBarColor(new Color(0, 0.5f, 0.9f));
        }

    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public  void SetHealthBarColor(Color healthColor)
    {
        text.GetComponent<TextMesh>().color = healthColor;
    }
    public void setHp( int hp)
    {

        if (hpMax < hp)
            hpMax = hp;
        float hpNowProportion =(float) hp / (float)hpMax;
        hpNow = hp.ToString();
        setColor(hpNowProportion);
        setText();
    }

    public void setHpMax(int hp)
    {
        hpMax = hp;
        setColor(1);
    }
    public bool haveHpMax()
    {
        if (hpMax == -1)
            return true;
        return false;
    }

    private void setText()
    {
        text.GetComponent<TextMesh>().text = hpNow + " Hp\n" + champName;
    }

    public string getChampName(string name)
    {
        if (name[0] == 'Z' && name[1] == 'Z')
            return name.Substring(2, name.Length-9);
        else return name.Substring(0, name.Length - 7);
    }


  public  void setRotation(bool isYou)
    {
        if (isYou)
        {
            //  Vector3 temp = text.transform.rotation.eulerAngles;
            //temp.y = -180;

            //  text.transform.rotation = Quaternion.Euler(temp);
            //  Debug.Log("asdacz");


            text.transform.Rotate(0,180,0);
        } 
       
    }


    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
    ChessMan champ=  GetComponentInParent<ChessMan>();       
       champName = getChampName(champ.name);
        setHpMax(champ.Hp);
        hpNow = champ.Hp.ToString();
        setText();
        setColor(1);
    }
}
