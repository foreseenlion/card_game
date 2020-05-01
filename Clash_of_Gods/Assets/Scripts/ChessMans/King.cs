using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King :ChessMan
{


    public override void UpdateMove()
    {
  
    bool condition_left = isWhite ? CurrentX != 0 : CurrentX != 7;//warunki ograniczające pole  nbica
    bool condition_right = isWhite ? CurrentX != 7 : CurrentY != 0;
    bool condition_up = isWhite ? CurrentY != 7 : CurrentY != 0;
    bool condition_down = isWhite ? CurrentY != 0 : CurrentY != 7;

    int newX, newY;

    int color = isWhite ? 1 : -1;

        possibleMoves = new bool[8, 8];

        possibleAtacks = new bool[8, 8];

     if (condition_left) //ruch w lewo
    {
            newX = CurrentX - color;
            newY = CurrentY;
            CheckMove(newX, newY);
     
    }
        if (condition_right) //ruch w lewo
        {
            newX = CurrentX + color;
            newY = CurrentY;
            CheckMove(newX, newY);

        }
        if (condition_up) //ruch w lewo
        {
            newX = CurrentX;
            newY = CurrentY + color;
            CheckMove(newX, newY);

        }
        if (condition_down) //ruch w lewo
        {
            newX = CurrentX ;
            newY = CurrentY - color;
            CheckMove(newX, newY);

        }



       
    }

}
