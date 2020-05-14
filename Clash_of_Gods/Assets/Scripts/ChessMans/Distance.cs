using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance : Rook
{
    [SerializeField]
    private int range;

    public override void UpdateMove()
    {
        possibleMoves = new bool[8, 8];
        possibleAtacks = new bool[8, 8];
        RookMovement();
        DistanceMovement();
    }

    private void DistanceMovement()
    {
        int newX = 0;
        int newY = 0;

        bool condition_left, condition_right, condition_up, condition_down;

        int color = isWhite ? 1 : -1;

        newX = CurrentX - (color * range);
        newY = CurrentY;
        condition_left = isWhite ? newX >= 0 : newX <= 7; //ustalanie warunku ograniczającego

        if (condition_left) //atak w lewo
        {

            CheckAtack(newX, newY);

        }

        newX = CurrentX + (color * range);
        newY = CurrentY;
        condition_right = isWhite ? newX <= 7 : newX >= 0;

        if (condition_right)  //atak w prawo
        {
            CheckAtack(newX, newY);
        }

        newX = CurrentX;
        newY = CurrentY + (color * range);
        condition_up = isWhite ? newY <= 7 : newY >= 0;

        if (condition_up) //atak w górę
        {
            CheckAtack(newX, newY);
        }

        newX = CurrentX;
        newY = CurrentY - (color * range);
        condition_down = isWhite ? newY >= 0 : newY <= 7;

        if (condition_down) //atak w dół
        {
            CheckAtack(newX, newY);
        }
    }
}
