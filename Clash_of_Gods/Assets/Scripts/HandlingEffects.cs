﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlingEffects : MonoBehaviour
{

    // Idzie po wszystkich champ i wykonuje efekty jakie maja na sobie 
    public void DoTheEffects(bool start)
    {
        foreach (ChessMan chess in BoardManager.Instance.ChessMens)
        {
            //&& chess.Effects.Count > 0
            if (chess != null && chess.ChampIsInBoard1)
            {
                try
                {
                    // if sluzy do obslugiwania efektow z strony gracza i przeciwnika 
                    if (start)
                    {
                        if ((chess.IsWhite && BoardManager.Instance.yourWhite) || (!chess.IsWhite && !BoardManager.Instance.yourWhite))
                            foreach (Effects effects in chess.Effects)
                            {

                                DoEffectAndDesReduceLength(effects, chess);

                            }
                    }
                    else
                        if ((chess.IsWhite && !BoardManager.Instance.yourWhite) || (!chess.IsWhite && BoardManager.Instance.yourWhite))
                        foreach (Effects effects in chess.Effects)
                        {

                            DoEffectAndDesReduceLength(effects, chess);
                        }
                }
                catch
                {

                }
            }
        }
    }

    // Podaje konkretyn efekt do wykonania i zmiejsza dlugosc trwania efektu
    void DoEffectAndDesReduceLength(Effects effects, ChessMan chess)
    {
        if (effects.iterateEveryTurn)
            getEffectTure(effects.TypeOfEffect, effects.valueEffect, chess);
        effects.length -= 1;
        if (effects.length == -1) { }
        else
        if (effects.length <= 0)
        {
            backToNormal(effects, chess);
            chess.Effects.Remove(effects);
        }
    }
    
    // Obsluguje efekty pojawienia sie na planszy ( wszystkich, wrogow, sojusznikow)
    public void DoTheEffectsAppear(string type, int value, string toEnemyAppearEffect)
    {
        foreach (ChessMan chess in BoardManager.Instance.ChessMens)
        {
            if (chess != null)
            {
                try
                {
                    if (toEnemyAppearEffect == "enemy")
                    {
                        if ((chess.IsWhite && !BoardManager.Instance.yourWhite) || (!chess.IsWhite && BoardManager.Instance.yourWhite))
                            getEffectTure(type, value, chess);
                    }
                    else if (toEnemyAppearEffect == "ally")
                    {
                        if ((chess.IsWhite && BoardManager.Instance.yourWhite) || (!chess.IsWhite && !BoardManager.Instance.yourWhite))
                           getEffectTure(type, value, chess);
                    }
                    else if (toEnemyAppearEffect == "all")
                    {
                        getEffectTure(type, value, chess);
                    }

                }
                catch
                {

                }


            }
        }
    }

    // switch efektow w trakcie atakowania przez stwory (stwor narzuca na innego cos podczas atakowania)
    public void setEffectWhenChamAttack(string TypeOfEffect, ChessMan SelectedChessman, ChessMan target)
    {
        switch (TypeOfEffect)
        {
            case "dmg":
                {
                    target.Effects.Add(new Effects("dmg", SelectedChessman.ImposesValueEffect, SelectedChessman.ImposesLength, SelectedChessman.effectName, SelectedChessman.DescriptionEffect, true, 0, SelectedChessman.effectAnimation));
                    if (SelectedChessman.effectAnimation != null)
                    {
                        Instantiate(SelectedChessman.effectAnimation, target.GetTileCenter(0.75f), Quaternion.Euler(0f, 0f, 0f), target.transform);
                    }
                    break;
                }
            case "nodmg":
                {
                    target.Effects.Add(new Effects("nodmg", SelectedChessman.ImposesValueEffect, SelectedChessman.ImposesLength, SelectedChessman.effectName, SelectedChessman.DescriptionEffect, false, target.Dmg));
                   getEffectTure("nodmg", SelectedChessman.ImposesValueEffect, target);
                    break;
                }
        }
    }

    // Cofa efekty
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

    // Switch z konkretnymi efektami do wykonania
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
// a to nie wiem po co napisałem ale nie usuwam bo moze się przyda xD
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
