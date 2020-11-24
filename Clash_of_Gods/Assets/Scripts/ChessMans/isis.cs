using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isis : ChessMan
{
    public override void UpdateMove()
    {
        PossibleMove = new bool[8, 8];
        PossibleAtacks = new bool[8, 8];
        BishopMovement();
    }

    private int move_limitTmp; 
    public void BishopMovement() //aktualizuje tablice możliwych ruchów i ataków
    {

        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;

        bool condition_left_up, condition_right_up, condition_left_down, contidion_right_down; //warucki które określają czy dany punkt nie jest poza planszą


        int newX, newY;

        if (firstmove)
        {
            move_limitTmp = Move_limit;
               Move_limit += 2;
        }
        else
        {
            Move_limit = move_limitTmp;
        }
            for (int i = 1; i <= Move_limit; i++)
            {
                for (int j = 0; j <= Move_limit; j++)
                {
                    newX = CurrentX - (i * color);
                    newY = CurrentY + (j * color);
                    condition_left_up = isWhite ? (newX >= 0 && newY <= 7) : (newX <= 7 && newY >= 0);

                    if (condition_left_up && hits[0]) //ruch w lewo
                    {
                        CheckMove(newX, newY, 0);
                    }

                    newX = CurrentX + (i * color);
                    newY = CurrentY + (j * color);
                    condition_right_up = isWhite ? (newX <= 7 && newY <= 7) : (newX >= 0 && newY >= 0);
                    if (condition_right_up && hits[1]) //ruch w prawo
                    {


                        CheckMove(newX, newY, 1);
                    }

                    newX = CurrentX - (i * color);
                    newY = CurrentY - (j * color);
                    condition_left_down = isWhite ? (newX >= 0 && newY >= 0) : (newX <= 7 && newY <= 7);
                    if (condition_left_down && hits[2]) //ruch w górę
                    {
                        CheckMove(newX, newY, 2);
                    }


                    newX = CurrentX + (i * color);
                    newY = CurrentY - (j * color);
                    contidion_right_down = isWhite ? (newX <= 7 && newY >= 0) : (newX >= 0 && newY <= 7);
                    if (contidion_right_down && hits[3]) //ruch w dół
                    {
                        CheckMove(newX, newY, 3);
                    }
                }
            }
            for (int i = 1; i <= Move_limit; i++)
            {
                bool condition_up, condition_down;

                newX = CurrentX;
                newY = CurrentY + (color * i);
                condition_up = isWhite ? newY <= 7 : newY >= 0;

                if (condition_up && hits[2]) //ruch w górę
                {
                    CheckMove(newX, newY, 2);
                }

                newX = CurrentX;
                newY = CurrentY - (color * i);
                condition_down = isWhite ? newY >= 0 : newY <= 7;

                if (condition_down && hits[3]) //ruch w dół
                {
                    CheckMove(newX, newY, 3);
                }
            }
        
    }


}
