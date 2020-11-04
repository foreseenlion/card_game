using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ChessMan : MonoBehaviour
{
    //gettery i settery
    #region
    public int idFigure { get; set; }
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool[,] PossibleMove { get => possibleMoves; set => possibleMoves = value; }
    public bool[,] PossibleAtacks { get => possibleAtacks; set => possibleAtacks = value; }
    public int Move_limit { get => move_limit; set => move_limit = value; }
    public int Hp { get => hp; set =>hp = value;}
    public int Dmg { get => dmg; set => dmg = value; }
    public bool IsWhite { 

        get
        {
            return isWhite;
        }

        set
        {
            isWhite = value;
            ChangeColor();
        }
    
    }

    #endregion

    protected bool[] hits = new bool[4];

    [NonSerialized]
    protected bool isWhite;

    [NonSerialized]
    public bool firstmove = true;

    protected bool[,] possibleMoves;

    protected bool[,] possibleAtacks;

    protected int color; //zależnie od koloru inna jest orientacja (to co dla białego jest prawo dla czarnego lewo)

    [SerializeField]
    int move_limit=8; //ile może wykonać ruchów

    [SerializeField]
    int hp = 3;

    [SerializeField]
    int dmg = 1;


    private void ChangeColor()
    {
        color = isWhite ? 1 : -1;
    }

    public void SetPosition(int x,int y) 
    {
        CurrentX = x;
        CurrentY = y;
    }
    public virtual void UpdateMove() //virtual oznacza że klasa może zosta nadpisana, każdy rodzaj piona ma swój schemat ruchów
    {
        possibleMoves = new bool[8,8];
        possibleAtacks = new bool[8, 8];
    }

    //sprawdza czy ruch może zostać wykonany, hit jest potrzebne gdy pion wykonuje więcej niż jeden ruch w turze
    protected void CheckMove(int _newX, int _newY, int hit)  
    {

        ChessMan c;
        c = BoardManager.Instance.ChessMens[_newX, _newY];  //tworzymy wirtualengo piona


        if (c == null) //wirtualny pion nie istnieje więc możemy się tam poruszyć 
        {
            
            possibleMoves[_newX, _newY] = true;
        }
        if (c != null && this.isWhite == c.isWhite) //jeśli na lini jest sojusznik nie można ruszyć dalej
        {
            hits[hit] = false;
            
        }
        if (c != null && this.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zaatakuj go
        {
            possibleAtacks[_newX, _newY] = true;
            possibleMoves[_newX, _newY] = false;
            hits[hit] = false; //jeżeli podczas pętli trafimy na sojusznika lub wroga nie sprawdzamy dalej danego kierunku
            
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

    protected void CheckAtack(int _newX, int _newY)
    {
        ChessMan c;
        c = BoardManager.Instance.ChessMens[_newX, _newY];

        
        if (c != null && this.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zaatakuj go
        {
            possibleAtacks[_newX, _newY] = true;
        
        }
    }

    protected void CheckIsEnd()
    {
        if (transform.position.z > 7 && color == 1)
        {
            color = -1;
            Debug.Log("zmaina");
        }
        if (transform.position.z < 1 && color == -1)
        {
            color = 1;
            Debug.Log("zmaina");
        }


    }







}
