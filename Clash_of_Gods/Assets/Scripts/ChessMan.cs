using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    public int range;

    [NonSerialized]
    public bool firstdeath = true;

    public List<Effects> Effects = new List<Effects>();

    public int ImposesValueEffect;
    public int ImposesLength;
    public string TypeOfEffect;
    public string DescriptionEffect;
    public string effectName;
    public string PowreDescription;

    public Sprite imageMove;
    public Sprite imageAttack;

    public GameObject effectAnimation;

    public string toAppearEffect;
    // all, enemy, ally
    public string toEnemyAppearEffect = "all";
    private bool ChampIsInBoard = false;

    public bool IsYou
    {
        get
        {
            return isYou;
        }
        set
        {
            if (healthBarHandler == null)
            {
                healthBarHandler = GetComponentInChildren<HealthBarHandler>();
                // healthBarHandler.getChampName(name);
            }
            healthBarHandler.setRotation(value);
            isYou = value;

        }
    }
    public Vector3 GetTileCenter(float y) //umieszczanie figury na środku danego pola
    {
        Vector3 origin = Vector3.zero;
        origin.x += (1f * CurrentX) + 0.5f; //ustawianie na  srodku
        origin.z += (1f * CurrentY) + 0.5f;
        origin.y += y;
        return origin;
    }

    public void showHpManiText(Text text, string texthp, bool heal)
    {
        Text textTmp = Instantiate(text, GetTileCenter(2.1f), Quaternion.Euler(0f, 0f, 0f), transform.Find("CanvasHpManipulation").GetComponent<Canvas>().transform);
        textTmp.GetComponent<Text>().text = texthp;
        if (heal)
        {
            textTmp.GetComponent<Text>().color = Color.green;
        }else textTmp.GetComponent<Text>().color = Color.red;
        Destroy(textTmp, 3f);
    }

    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            showHpManipulation( value);
            setHpBar(value);
            hp = value;

        }
    }

    void showHpManipulation(int value)
    {
        int hpMani;
        Color c;
        if (value < hp)
        {
            hpMani = hp - value;
            c = Color.red;
            BoardManager.Instance.GetComponent<AnimationsHendling>().DmgAnimation(this);
            showHpManiText(BoardManager.Instance.GetComponent<TextDevelop>().HpManipulationText, hpMani.ToString(),true);
        }
        else
        {
            hpMani = value - hp;
            c = Color.green;
            showHpManiText(BoardManager.Instance.GetComponent<TextDevelop>().HpManipulationText, hpMani.ToString(),false);

        }
        
    }

    public int Dmg { get => dmg; set => dmg = value; }
    public bool IsWhite
    {

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

    public bool ChampIsInBoard1 { get => ChampIsInBoard; set => ChampIsInBoard = value; }

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
    int move_limit = 8; //ile może wykonać ruchów

    [SerializeField]
    int hp = 3;

    [SerializeField]
    int dmg = 1;

    [SerializeField]
    bool fly = false;

    [SerializeField]
    public bool MainGod = false;

    public bool isYou;

    public HealthBarHandler healthBarHandler;

    private void ChangeColor()
    {
        color = isWhite ? 1 : -1;
    }
    public void setHpBar(int value)
    {

        if (healthBarHandler == null)
        {
            healthBarHandler = GetComponentInChildren<HealthBarHandler>();
            // healthBarHandler.getChampName(name);
        }

        if (healthBarHandler.haveHpMax())
            healthBarHandler.setHpMax(hp);
        healthBarHandler.setHp(value);
    }

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }
    public virtual void UpdateMove() //virtual oznacza że klasa może zosta nadpisana, każdy rodzaj piona ma swój schemat ruchów
    {
        possibleMoves = new bool[8, 8];
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
            if (!fly)
                hits[hit] = false;

        }

        if (c != null && this.isWhite != c.isWhite) //jeśli na lini jest przeciwnik zaatakuj go
        {
            possibleAtacks[_newX, _newY] = true;
            possibleMoves[_newX, _newY] = false;
            if (!fly)
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
