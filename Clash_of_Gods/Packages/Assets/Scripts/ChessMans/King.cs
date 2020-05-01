using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King :ChessMan
{


    public override void UpdateMove()
    {
        ChessMan c1;

    bool[,] moves = new bool[8, 8];

    bool condition_left = isWhite ? CurrentX != 0 : CurrentX != 7;//warunki ograniczające pole  nbica
    bool condition_right = isWhite ? CurrentX != 7 : CurrentY != 0;
    bool condition_up = isWhite ? CurrentY != 7 : CurrentY != 0;
    bool condition_down = isWhite ? CurrentY != 0 : CurrentY != 7;

        int newX, newY;

    int color = isWhite ? 1 : -1;

     if (condition_left) //ruch w lewo
    {
            newX = CurrentX - color;
            newY = CurrentY;
            c1 = BoardManager.Instance.ChessMens[newX,newY];
            if (c1 == null || (c1 != null && this.isWhite != c1.isWhite))
                moves[newX, newY] = true;
     
    }
        if (condition_right) //ruch w lewo
        {
            newX = CurrentX + color;
            newY = CurrentY;
            c1 = BoardManager.Instance.ChessMens[newX, newY];
            if (c1 == null || (c1 != null && this.isWhite != c1.isWhite))
                moves[newX, newY] = true;

        }
        if (condition_up) //ruch w lewo
        {
            newX = CurrentX;
            newY = CurrentY + color;
            c1 = BoardManager.Instance.ChessMens[newX, newY];
            if (c1 == null || (c1 != null && this.isWhite != c1.isWhite))
                moves[newX, newY] = true;

        }
        if (condition_down) //ruch w lewo
        {
            newX = CurrentX ;
            newY = CurrentY - color;
            c1 = BoardManager.Instance.ChessMens[newX, newY];
            if (c1 == null || (c1 != null && this.isWhite != c1.isWhite))
                moves[newX, newY] = true;

        }



        PosssibleMove = moves;
    }

}
