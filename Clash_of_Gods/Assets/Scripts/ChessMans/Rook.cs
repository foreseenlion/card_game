using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessMan
{
   

    public override void UpdateMove()
    {
        possibleMoves = new bool[8, 8];
        possibleAtacks = new bool[8, 8];
        RookMovement();
    }

    public void RookMovement()
    {
        

        int newX = 0;
        int newY = 0;

        bool condition_left, condition_right, condition_up, condition_down;


        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;

        for (int i = 1; i <= Move_limit; i++)
        {
            newX = CurrentX - (color * i);
            newY = CurrentY;
            condition_left = isWhite ? newX >= 0 :  newX <= 7; //ustalanie warunku ograniczającego

            if (condition_left && hits[0]) //ruch w lewo
            {
        
                CheckMove(newX, newY, 0);
               
            }
            
            newX = CurrentX + (color * i);
            newY = CurrentY;
            condition_right = isWhite ?  newX <= 7 : newX >= 0;

            if (condition_right && hits[1])  //ruch w prawo
            {
                CheckMove(newX, newY, 1);
            }

            newX = CurrentX;
            newY =CurrentY + (color * i);
            condition_up = isWhite ?  newY <= 7 :  newY >= 0;

            if (condition_up && hits[2]) //ruch w górę
            {
                CheckMove(newX, newY, 2);
            }

            newX = CurrentX;
            newY = CurrentY - (color * i);
            condition_down = isWhite ?  newY >= 0 :  newY <= 7;

            if (condition_down && hits[3]) //ruch w dół
            {
                CheckMove(newX, newY, 3);
            }


        }

    }

 






}
