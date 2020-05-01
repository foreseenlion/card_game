using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessMan : MonoBehaviour
{
    //gettery i settery
    #region
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool[,] PossibleMove { get => possibleMoves; set => possibleMoves = value; }
    public bool[,] PossibleAtacks { get => possibleAtacks; set => possibleAtacks = value; }
    public int Move_limit { get => move_limit; set => move_limit = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    #endregion

    protected bool[] hits = new bool[4];

    public bool isWhite;

    public bool firstmove = true;

    protected bool[,] possibleMoves;

    protected bool[,] possibleAtacks;

    [SerializeField]
    int move_limit=8;

    [SerializeField]
    int hp = 3;

    [SerializeField]
    int dmg = 1;

    
    

    public void SetPosition(int x,int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    public virtual void UpdateMove() //virtual oznacza że klasa może zosta nadpisana
    {
        possibleMoves = new bool[8,8];
        possibleAtacks = new bool[8, 8];
    }

    protected void CheckMove(int _newX, int _newY, int hit)
    {
      

       

        
        ChessMan c;
        c = BoardManager.Instance.ChessMens[_newX, _newY];


        if (c == null)
        {
            
            possibleMoves[_newX, _newY] = true;
        }
        if (c != null && this.isWhite == c.isWhite) //jeśli na lini jest sojusznik nie można ruszyć dalej
        {
            hits[hit] = false;
            
        }
        if (c != null && this.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zbij go
        {
            possibleAtacks[_newX, _newY] = true;
            possibleMoves[_newX, _newY] = false;
            hits[hit] = false;
            
        }

    }

    protected void CheckMove(int _newX, int _newY)
    {
        

        ChessMan c;
        c = BoardManager.Instance.ChessMens[_newX, _newY];

        if (c == null)
            possibleMoves[_newX, _newY] = true;
        if (c != null && this.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zbij go
        {
            possibleAtacks[_newX, _newY] = true;
            possibleMoves[_newX, _newY] = false;
            
        }

    }






}
