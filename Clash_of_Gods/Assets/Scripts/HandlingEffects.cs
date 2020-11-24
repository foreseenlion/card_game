using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingEffects : MonoBehaviour
{

    

    public void getEffect(string effect, int valueEffect, ChessMan chessMan)
    {
        switch (effect)
        {
            case "dmg":
                timeDmg(valueEffect, chessMan);
                break;
        }
    }

    public void timeDmg(int valueEffect, ChessMan chessMan)
    {
      int demage=  chessMan.Hp - valueEffect;
        BoardManager.Instance.IfHeDies(chessMan, demage);    
    }


}
