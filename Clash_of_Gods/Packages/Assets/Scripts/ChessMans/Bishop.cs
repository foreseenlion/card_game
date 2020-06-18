using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessMan
{

    bool[] hits = new bool[4];



    public override void UpdateMove()
    {
        bool[,] moves = new bool[8, 8];
        bool[,] atacks = new bool[8, 8];

        BishopMovement(moves, atacks);

        PossibleAtacks = atacks;
        PosssibleMove = moves;

    }

    public void BishopMovement(bool[,] _moves,bool[,] _atk)
    {
        ChessMan c1;

        bool condition_left_up, condition_right_up, condition_left_down, contidion_right_down;

        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;

        int color = isWhite ? 1 : -1;
        int newX, newY;

        for (int i = 1; i <= Move_limit; i++)
        {
            newX = CurrentX - (i * color);
            newY = CurrentY + (i * color);
            condition_left_up = isWhite ? (newX >= 0 && newY <= 7) : (newX <= 7 && newY >= 0);

            if (condition_left_up && hits[0]) //ruch w lewo
            {

                CheckMove(this, _moves, _atk, newX, newY, 0);
            }

            newX = CurrentX + (i * color);
            newY = CurrentY + (i * color);
            condition_right_up = isWhite ? ( newX <= 7 && newY <= 7) : (newX >= 0 && newY >= 0);

            if (condition_right_up && hits[1]) //ruch w prawo
            {


                CheckMove(this, _moves, _atk, newX, newY, 1);
            }
            newX = CurrentX - (i * color);
            newY = CurrentY - (i * color);
            condition_left_down = isWhite ? ( newX >= 0 && newY >= 0) : (newX <= 7 && newY <= 7);

            if (condition_left_down && hits[2]) //ruch w górę
            {
                CheckMove(this, _moves, _atk, newX, newY, 2);
            }


            newX = CurrentX + (i * color);
            newY = CurrentY - (i * color);
            contidion_right_down = isWhite ? (newX <= 7 && newY >= 0) : ( newX >= 0 && newY <= 7);

            if (contidion_right_down && hits[3]) //ruch w dół
            {
                CheckMove(this, _moves, _atk, newX, newY, 3);
            }


        }


        

    }

    private void CheckMove(ChessMan current_chess, bool[,] moves, bool[,] atacks, int _newX, int _newY, int hit)
    {

        ChessMan c;
        c = BoardManager.Instance.ChessMens[_newX, _newY];
        if (c == null)
            moves[_newX, _newY] = true;
        if (c != null && current_chess.isWhite == c.isWhite) //jeśli na lini jest sojusznik nie można ruszyć dalej
            hits[hit] = false;
        if (c != null && current_chess.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zbij go
        {
            atacks[_newX, _newY] = true;
            moves[_newX, _newY] = false;
            hits[hit] = false;
        }

    }
}
