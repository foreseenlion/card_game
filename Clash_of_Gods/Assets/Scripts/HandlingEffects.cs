using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingEffects : MonoBehaviour
{

    public void backToNormal(Effects effects, ChessMan chessMan)
    {
        switch (effects.TypeOfEffect)
        {
            case "dmg":
                break;
            case "nodmg":
                BackAttackReduction(effects.oldValue, chessMan);
                break;
        }
    }

    public void getEffectTure(string effect, int valueEffect, ChessMan chessMan)
    {
        switch (effect)
        {
            case "dmg":
                timeDmgTure(valueEffect, chessMan);
                break;
            case "nodmg":
                attackReduction(valueEffect, chessMan);
                break;
            case "adddmg":
                addDmg(valueEffect, chessMan);
                break;
            case "hydra":
                addDmg(valueEffect, chessMan);
                break;
        }
    }

    public void addDmg(int valueEffect, ChessMan chessMan)
    {
        chessMan.Dmg+= valueEffect;
    }

    public void timeDmgTure(int valueEffect, ChessMan chessMan)
    {
      int demage=  chessMan.Hp - valueEffect;
        BoardManager.Instance.IfHeDies(chessMan, demage);    
    }

    public void attackReduction(int valueEffect, ChessMan chessMan)
    {
    int result   = chessMan.Dmg - valueEffect;
        if (result <= 0)
            chessMan.Dmg = 0;
        else
        chessMan.Dmg = result;
    }

    public void getAppearEffect(string effect, int valueEffect, ChessMan chessMan)
    {
        switch (effect)
        {
            case "dmg":
                timeDmgTure(valueEffect, chessMan);
                break;
        }
    }

    void BackAttackReduction(int valueEffect, ChessMan chessMan)
    {
            chessMan.Dmg = valueEffect;
    }


}
