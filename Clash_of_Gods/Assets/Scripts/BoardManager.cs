using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{



    public static BoardManager Instance { get; set; }

 

    public ChessMan[,] ChessMens { get; set; }
    public ChessMan SelectedChessman;

   

    public int selectedX = -1; //wybrane pole
    public int selectedY = -1;


    public bool isWhiteTurn = true; //czyja kolej?
    int number_of_move = 0;

    [SerializeField]
    CardManager WhiteDeck;
    [SerializeField]
    CardManager BlackDeck;


    private void Start()
    {
        Instance = this;
        ChessMens = new ChessMan[8, 8];
        WhiteDeck.UpdateSpawn(ChessMens);
        BlackDeck.UpdateSpawn(ChessMens);
       
    }
    private void Update()
    {
        UpdateSelection();

        if (selectedX >= 0 && selectedY >= 0) //coś zaznaczono
        {
            Debug.DrawLine(
                Vector3.forward * selectedY + Vector3.right * selectedX,
                Vector3.forward * (selectedY + 1) + Vector3.right * (selectedX + 1));
        }

        if (Input.GetMouseButtonDown(0)) //wduszenie lewego przycisku myszy
        {
            if (selectedX >= 0 || selectedY >= 0) //sprawdzenie czy kliknięto na planszy
            {
                if (SelectedChessman == null) //jeśli nic nie wybrano wybierz danego piona 
                {
                    try
                    {
                        SelectChessman(selectedX, selectedY);
                    }
                    catch (Exception e)
                    {

                    }
                }
                else //jeśli wcześniej wybrano piona rusz nim na wybrane pole
                {
                    MoveChessman(selectedX, selectedY);
                }

            }
        }

    }

    private void MoveChessman(int x, int y)
    {

        ChessMan target = ChessMens[x, y];

        if (SelectedChessman.PossibleMove[x, y]) // można wykonać taki ruch?
        {

            ChessMens[SelectedChessman.CurrentX, SelectedChessman.CurrentY] = null; //wybrany pion 'znika' z aktualnej pozycji
            SelectedChessman.transform.position = GetTileCenter(x, y);
            SelectedChessman.SetPosition(x, y);
            ChessMens[x, y] = SelectedChessman;
            UpdateMove();
           

            if (SelectedChessman.firstmove) //wykonanie pierwszego ruchu potrzebne przy pionach
                SelectedChessman.firstmove = false;


        }
        if (SelectedChessman.PossibleAtacks[x,y]) //można atakować
        {
            
            int[] newPositions = CalculateNewPosition(x, y, SelectedChessman.CurrentX, SelectedChessman.CurrentY);
            ChessMens[SelectedChessman.CurrentX, SelectedChessman.CurrentY] = null; //wybrany pion 'znika' z aktualnej pozycji
            SelectedChessman.transform.position = GetTileCenter(newPositions[0], newPositions[1]);
            SelectedChessman.SetPosition(newPositions[0], newPositions[1]);
            ChessMens[newPositions[0], newPositions[1]] = SelectedChessman;

            AtackChessMan(target);
            UpdateMove();

            if (SelectedChessman.firstmove) //wykonanie pierwszego ruchu potrzebne przy pionach
                SelectedChessman.firstmove = false;

        }
        BoardHighlitghs.Instance.HideAll();
        SelectedChessman = null; //klinięcie w inne niż możliwe miejsce anuluje wybór


    }

    private void AtackChessMan(ChessMan target)
    {

        target.GetComponent<Animator>().Play("take_damage");
        int damage = target.Hp - SelectedChessman.Dmg;

        if (damage <= 0)
            KillChessMan(target);
        else
            target.Hp = damage;
        
    }

    private void KillChessMan(ChessMan target)
    {
        if (target.GetType() == typeof(King)) //jeśli to król zakończ grę
        {
            GameManager.instance.Winner = isWhiteTurn ? "White" : "Black";
            GameManager.instance.Condition = "kiling the God";
            SceneManager.LoadScene("End_Game");

        }
        //usunięcie z listy aktywnych figur
        Destroy(target.gameObject); //zniszczenie figury
    }

    private void SelectChessman(int x, int y)
    {
        if (ChessMens[x, y] == null) //sprawdzenie czy na wybranej pozycji jest pion
            return;
        if (ChessMens[x, y].isWhite != isWhiteTurn) //sprawdzenie czy pion ma kolor danego gracza
            return;

        BoardHighlitghs.Instance.HideAll();
        SelectedChessman = ChessMens[x, y];
        ChessMens[x, y].UpdateMove();

        BoardHighlitghs.Instance.HighlightAllowedMoves(SelectedChessman.PossibleMove,SelectedChessman.PossibleAtacks);
      


    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;

        if (Physics.Raycast(
            Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            25.0f,
            LayerMask.GetMask("Board"))) //warunek określający nad jakim polem gracz ma umieszczoną myszkę
        {
            selectedX = (int)hit.point.x;
            selectedY = (int)hit.point.z;
        }
        else
        {
            selectedX = -1;
            selectedX = -1;
        }

    }
   
    


    private Vector3 GetTileCenter(int x, int y) //umieszczanie figury na środku danego pola
    {
        Vector3 origin = Vector3.zero;
        origin.x += (1 * x) + 0.5f; //ustawianie na  srodku
        origin.z += (1 * y) + 0.5f;
        return origin;
    }

    

    public void UpdateMove() //funkcja przełącza aktywnego gracza
        {
            if (number_of_move < 2) 
            number_of_move++;

            if (number_of_move == 2)
            {
            isWhiteTurn = !isWhiteTurn;
            number_of_move = 0;
            }

        
        }

  
     private int[] CalculateNewPosition(int newX,int newY,int CurrentX,int CurrentY)
    {
        int[] results = new int[2];


        if (newX - CurrentX < 0) //poruszam się w lewo
            results[0] = newX + 1;
        if (newX - CurrentX > 0) //poruszam się w prawo
            results[0] = newX - 1;
        if (newX - CurrentX == 0) //nie poruszam się w osi x
            results[0] = CurrentX;

        if (newY - CurrentY < 0) //poruszam się w dół
        {
            results[1] = newY + 1;
            Debug.Log("DÓŁ");
        }
        if (newY - CurrentY > 0) //poruszam się w górę
        {
            results[1] = newY - 1;
            Debug.Log("GÓRA");

        }
        if (newY - CurrentY == 0) //nie poruszam się w osi y
            results[1] = CurrentY;

        return results;

    }


}
