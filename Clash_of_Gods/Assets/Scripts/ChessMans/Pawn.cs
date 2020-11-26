using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessMan
{
    
   
   
    public override void UpdateMove()
    {
        ChessMan c1, c2;

        possibleMoves = new bool[8, 8];
        possibleAtacks = new bool[8, 8];

        bool condition_left = isWhite ? (CurrentX != 0 && CurrentY != 7) : (CurrentX != 0 && CurrentY != 0); //warunki ograniczające pole  nbica
        bool condition_right = isWhite ? (CurrentX != 7 && CurrentY != 7) : (CurrentX != 7 && CurrentY != 0); 
        

          //ustalanie mnożnika przy ustalaniu pozycji

        CheckIsEnd();

        //BICIA
        #region
            //bicie w lewo
            if (condition_left)
            {
                c1 = BoardManager.Instance.ChessMens[CurrentX - color, CurrentY + color];
                if (c1 != null && this.isWhite != c1.IsWhite)
                    possibleAtacks[CurrentX - color, CurrentY + color] = true;

            }

            //bisie w prawo
             if (condition_right)
            {
                c1 = BoardManager.Instance.ChessMens[CurrentX + color, CurrentY + color];
                if (c1 != null && this.isWhite != c1.IsWhite)
                    possibleAtacks[CurrentX + color, CurrentY + color] = true;
            }
        
        #endregion
        //przód
        #region
        //przód
        if (!firstmove)
        {
            c1 = BoardManager.Instance.ChessMens[CurrentX, CurrentY + color];
            if (c1 == null)
                possibleMoves[CurrentX, CurrentY + color] = true;
        }

        
        if (firstmove)
        {
            c1 = BoardManager.Instance.ChessMens[CurrentX, CurrentY + color];
            c2 = BoardManager.Instance.ChessMens[CurrentX, CurrentY + (2*color)];
            if (c1 == null && c2 == null)
            {
                possibleMoves[CurrentX, CurrentY + (2 * color)] = true;
                possibleMoves[CurrentX, CurrentY + (1 * color)] = true;
            }
        }




        condition_left = isWhite ? CurrentX != 0 : CurrentX != 7;//warunki ograniczające pole  nbica
     condition_right = isWhite ? CurrentX != 7 : CurrentY != 0;
        bool condition_up = isWhite ? CurrentY != 7 : CurrentY != 0;
        bool condition_down = isWhite ? CurrentY != 0 : CurrentY != 7;

        int newX, newY;


        possibleMoves = new bool[8, 8];

        possibleAtacks = new bool[8, 8];

        CheckIsEnd();

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
            newX = CurrentX;
            newY = CurrentY - color;
            CheckMove(newX, newY);

        }









    }
    #endregion


}
