using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLines : ChessMan
{

    public int movewidth = 0;
    int width;
    public override void UpdateMove()
    {
        possibleMoves = new bool[8, 8];
        possibleAtacks = new bool[8, 8];
        RookMovement();
    }
    int newX = 0;
    int newY = 0;



    private void Move(int i, string side, int hitsI, bool range)
    {
        bool condition;

        for (int j=-movewidth;j<=movewidth; j++)
        {
            newPositoinsA(i, j, side);

            condition = sideFind(side);
            if (condition && hits[hitsI])
            {
                try
                {   if(!range)
                    CheckMove(newX, newY, hitsI);
                else CheckAtack(newX, newY);
                }
                catch
                {

                }
                
            }
        }
    }



    void newPositoinsA(int i, int j, string side)
    {
        switch (side)
        {
            case "left":
                newX = CurrentX - (color * i);
                newY = CurrentY + j;
                break;
            case "right":
                newX = CurrentX + (color * i);
                newY = CurrentY + j;
                break;
            case "up":
                newX = CurrentX + j;
                newY = CurrentY + (color * i);
                break;
            case "down":
                newX = CurrentX + j;
                newY = CurrentY - (color * i);
                break;
        }
    }

    bool sideFind(string side)
    {
        switch (side)
        {
            case "left":
                return isWhite ? newX >= 0 : newX <= 7;
            case "right":
                return isWhite ? newX <= 7 : newX >= 0;
            case "up":
                return isWhite ? newY <= 7 : newY >= 0;
            case "down":
                return isWhite ? newY >= 0 : newY <= 7;
        }
        return false;
    }


    public void RookMovement()
    {

        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;

        for (int i = 1; i <= Move_limit; i++)
        {
            try { Move(i, "left", 0,false); } catch { }
            try { Move(i, "right", 1, false); } catch { }
            try { Move(i, "up", 2, false); } catch { }
            try { Move(i, "down", 3, false); } catch { }
        }
        if(range!=0)
        for (int i = 1; i <= range; i++)
        {
            try { Move(i, "left", 0, true); } catch { }
            try { Move(i, "right", 1, true); } catch { }
            try { Move(i, "up", 2, true); } catch { }
            try { Move(i, "down", 3, true); } catch { }
        }
    }
}