using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : ChessMan
{
    public int movewidth = 0;

    //pole a wychodzi z pionka, nastepne litery to kolejne kolumny 
    public int[] A;
    public int[] B;
    public int[] C;
    public int[] D;
    public int[] E;
    public int[] F;
    public int[] G;
    public int[] H;

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


    public void RookMovement()
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


        for (int i = 0; i < hits.Length; i++)
            hits[i] = true;

        for (int i = 0; i <= 3; i++)
        {
            int x = 0;
            foreach (int[] column in positons)
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
                                CheckMove(newX, newY, i);
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
          

        //for (int i = 1; i <= Move_limit; i++)
        //{
        //    try { Move(i, "left", 0, false); } catch { }
        //    try { Move(i, "right", 1, false); } catch { }
        //    try { Move(i, "up", 2, false); } catch { }
        //    try { Move(i, "down", 3, false); } catch { }
        //}
        //if (range != 0)
        //    for (int i = 1; i <= range; i++)
        //    {
        //        try { Move(i, "left", 0, true); } catch { }
        //        try { Move(i, "right", 1, true); } catch { }
        //        try { Move(i, "up", 2, true); } catch { }
        //        try { Move(i, "down", 3, true); } catch { }
        //    }
    }
}
