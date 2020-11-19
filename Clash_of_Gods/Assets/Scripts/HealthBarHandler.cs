using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarHandler : MonoBehaviour
{
     public  Image HealthBarImage;
    private int hpMax=-1;

    public bool MainGod=false;

    /// <summary>
    /// Sets the health bar value
    /// </summary>
    // <param name="value">should be between 0 to 1</param>
    public  void SetHealthBarValue(float value)
    {
        
        HealthBarImage.fillAmount = value;
        if (HealthBarImage.fillAmount < 0.4f)
        {
            if (!MainGod)
                SetHealthBarColor(Color.red);
            else SetHealthBarColor(new Color(0.3f, 0, 0));
        }
        else if (HealthBarImage.fillAmount < 0.8f)
        {
            if (!MainGod)
                SetHealthBarColor(Color.yellow);
            else SetHealthBarColor(new Color(0, 0.5f, 0));
        }
        else
        {
            if (!MainGod)
            SetHealthBarColor(new Color(0,0.7f,0));
            else SetHealthBarColor(new Color(0, 0.5f, 0.9f));
        }
        
    }

    public  float GetHealthBarValue()
    {
        return HealthBarImage.fillAmount;
    }

    /// <summary>
    /// Sets the health bar color
    /// </summary>
    /// <param name="healthColor">Color </param>
    public  void SetHealthBarColor(Color healthColor)
    {
        HealthBarImage.color = healthColor;
    }
    public void setHp( int hp)
    {

        if (hpMax < hp)
            hpMax = hp;
        float hpNow =(float) hp / (float)hpMax;
      SetHealthBarValue(hpNow);
    }

    public void setHpMax(int hp)
    {

        hpMax = hp;
        SetHealthBarValue(1);
    }
    public bool haveHpMax()
    {
        if (hpMax == -1)
            return true;
        return false;
    }



    /// <summary>
    /// Initialize the variable
    /// </summary>
    private void Start()
    {
        HealthBarImage = GetComponentInParent<Image>();
       SetHealthBarValue(1);
    }
}
