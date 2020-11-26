using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessMan
{
    //schemat ruchów podobny do końca w prawdziwych szachach

    public override void UpdateMove()
    {
        PossibleMove = new bool[8, 8];
        PossibleAtacks = new bool[8, 8];
        BishopMovement();
    }

    public void BishopMovement() //aktualizuje tablice możliwych ruchów i ataków
    {

        for (int i = 0; i < hits.Length; i++) 
            hits[i] = true;

        bool condition_left_up, condition_right_up, condition_left_down, contidion_right_down; //warucki które określają czy dany punkt nie jest poza planszą

        
        int newX, newY;

        for (int i = 1; i <= Move_limit; i++)
        {
            newX = CurrentX - (i * color); 
            newY = CurrentY + (i * color);
            condition_left_up = isWhite ? (newX >= 0 && newY <= 7) : (newX <= 7 && newY >= 0);

            if (condition_left_up && hits[0]) //ruch w lewo
            {

                CheckMove(newX, newY, 0);
            }

            newX = CurrentX + (i * color);
            newY = CurrentY + (i * color);
            condition_right_up = isWhite ? ( newX <= 7 && newY <= 7) : (newX >= 0 && newY >= 0);

            if (condition_right_up && hits[1]) //ruch w prawo
            {


                CheckMove(newX, newY, 1);
            }
            newX = CurrentX - (i * color);
            newY = CurrentY - (i * color);
            condition_left_down = isWhite ? ( newX >= 0 && newY >= 0) : (newX <= 7 && newY <= 7);

            if (condition_left_down && hits[2]) //ruch w górę
            {
                CheckMove(newX, newY, 2);
            }


            newX = CurrentX + (i * color);
            newY = CurrentY - (i * color);
            contidion_right_down = isWhite ? (newX <= 7 && newY >= 0) : ( newX >= 0 && newY <= 7);

            if (contidion_right_down && hits[3]) //ruch w dół
            {
                CheckMove( newX, newY, 3);
            }


        }


        bool condition_left = isWhite ? CurrentX != 0 : CurrentX != 7;//warunki ograniczające pole  nbica
        bool condition_right = isWhite ? CurrentX != 7 : CurrentY != 0;
        bool condition_up = isWhite ? CurrentY != 7 : CurrentY != 0;
        bool condition_down = isWhite ? CurrentY != 0 : CurrentY != 7;

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

}
