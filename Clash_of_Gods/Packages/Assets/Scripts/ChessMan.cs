using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChessMan : MonoBehaviour
{
    //gettery i settery
    #region
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool[,] PosssibleMove { get => possibleMove; set => possibleMove = value; }
    public bool[,] PossibleAtacks { get => possibleAtacks; set => possibleAtacks = value; }
    public int Move_limit { get => move_limit; set => move_limit = value; }
    public int Hp { get => hp; set => hp = value; }
    public int Dmg { get => dmg; set => dmg = value; }
    #endregion

    public bool isWhite;

    public bool firstmove = true;

    private bool[,] possibleMove;

    private bool[,] possibleAtacks;

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
        possibleMove = new bool[8,8];
        possibleAtacks = new bool[8, 8];
    }


  




}
