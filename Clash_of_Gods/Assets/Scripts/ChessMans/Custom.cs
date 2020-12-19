using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : ChessMan
{

    //pole a wychodzi z pionka, nastepne litery to kolejne kolumny 
    public int[] A;
    public int[] B;
    public int[] C;
    public int[] D;
    public int[] E;
    public int[] F;
    public int[] G;
    public int[] H;

    #region
    public int[] A_range;
    public int[] B_range;
    public int[] C_range;
    public int[] D_range;
    public int[] E_range;
    public int[] F_range;
    public int[] G_range;
    public int[] H_range;
    #endregion
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
        
        //bool condition;

        //for (int j = -movewidth; j <= movewidth; j++)
        //{
        //    newPositoinsA(i, j, side);

        // //   condition = sideFind(side);
        //    if (condition && hits[hitsI])
        //    {
        //        try
        //        {
        //            if (!range)
        //                CheckMove(newX, newY, hitsI);
        //            else CheckAtack(newX, newY);
        //        }
        //        catch
        //        {

        //        }

        //    }
        //}
    }



    void newPositoinsA(int j, int i, int side)
    {
        switch (side)
        {
            case 0:
                newX = CurrentX - (color * i);
                newY = CurrentY + color * j;
                break;
            case 1:
                newX = CurrentX + (color * j);
                newY = CurrentY + color * i;
                break;
            case 2:
                newX = CurrentX - color * j;
                newY = CurrentY - (color * i);
                break;
            case 3:
                newX = CurrentX + color * i;
                newY = CurrentY - (color * j);
                break;
        }
    }

    bool sideFind(int side)
    {
        switch (side)
        {
            case 0:
                //left
        return isWhite ? newX >= 0 : newX <= 7;

            case 1:
                //right
             return isWhite ? newX <= 7 : newX >= 0;
              
            case 2:
                //up
          return isWhite ? newY <= 7 : newY >= 0;
             
            case 3:
                //down
       return isWhite ? newY >= 0 : newY <= 7;

        }
        return false;
    }


    List<int[]> rangeList()
    {
        List<int[]> range = new List<int[]>();
        range.Add(A_range);
        range.Add(B_range);
        range.Add(C_range);
        range.Add(D_range);
        range.Add(E_range);
        range.Add(F_range);
        range.Add(G_range);
        range.Add(H_range);
        return range;
    }
    List<int[]> positonsList()
    {
        List<int[]> positons = new List<int[]>();
        positons.Add(A);
        positons.Add(B);
        positons.Add(C);
        positons.Add(D);
        positons.Add(E);
        positons.Add(F);
        positons.Add(G);
        positons.Add(H);
        return positons;
    }

    void setMoveAndAttack(List<int[]> positonsOrAttacks, bool isMove)
    {
        for (int i = 0; i <= 3; i++)
        {
            int x = 0;
            foreach (int[] column in positonsOrAttacks)
            {
                int y = 0;
                foreach (int pos in column)
                {
                    if (pos != 0)
                    {
                        newPositoinsA(x, y, i);
                        if (sideFind(i))
                        {
                            try
                            {
                                if(isMove)
                                CheckMove(newX, newY, i);
                                else
                                CheckAtack(newX, newY);
                            }
                            catch
                            {

                            }

                        }
                    }
                    y++;
                }
                x++;
            }
        }
    }


    public void RookMovement()
    {

        List<int[]> positons = positonsList();
        List<int[]> range = rangeList();

        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;
        setMoveAndAttack(positons, true);
        setMoveAndAttack(range, false);


    }
}
