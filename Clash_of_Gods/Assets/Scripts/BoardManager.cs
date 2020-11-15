using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{

    bool IfICanCeonnection = true;

    // fields and events
    #region
    [SerializeField]      //TALIE
    CardManager WhiteDeck;

    [SerializeField]
    CardManager BlackDeck;

    [SerializeField]      //TALIE
    CardManager WhiteDeckHide;

    [SerializeField]
    CardManager BlackDeckHide;

    public SendToServer sendToServer;
    public ChessMan SelectedChessman; //wybrany pion

    public int selectedX = -1; //wybrane pole
    public int selectedY = -1;
    public int number_of_move = 0; //który ruch gracz wykonał (co dwa ruchy zmienia się aktywny graCZ)

    public bool isWhiteTurn = true; //czyja kolej?

    public bool yourWhite;

    public string religionId;
    private string deckId = "";

    public string enemyDeckId = "";

    public bool IsGameStart = false;

    public static BoardManager Instance { get; set; }
    public ChessMan[,] ChessMens { get; set; } //tablica wszystkich pionów

    public string DeckId {
        get
        {
            return deckId;
        }

        set
        {
            deckId = value;
            onDeckIdGenerated.Invoke();

        }
    }
  
    private event Action onDeckIdGenerated;
    #endregion

    private void Start()
    {
        religionId = myReligion.religion;
        sendToServer = new SendToServer();

        Instance = this;
        ChessMens = new ChessMan[8, 8];
        
        onDeckIdGenerated += () =>
        {
            GetDecks();
        };

        if (IfICanCeonnection)
        {
            sendToServer.SocketIoConnection();
            IfICanCeonnection = false;
        }


    }

    private void GetDecks()
    {
        if (deckId[1] == '0')
        {
            WhiteDeck.InstantiateDeck(SetDeckNumber(deckId));
            WhiteDeck.ChessMens = ChessMens;
        }
        else
        {
            BlackDeck.InstantiateDeck(SetDeckNumber(deckId));
            BlackDeck.ChessMens = ChessMens;
        }
       
    }

    public void SetEnemyDecks()
    {
        if (deckId[1] == '1')
        {
            WhiteDeckHide.InstantiateDeck(SetDeckNumber(enemyDeckId));
            WhiteDeckHide.ChessMens = ChessMens;

        }
        else
        {
            BlackDeckHide.InstantiateDeck(SetDeckNumber(enemyDeckId));
            BlackDeckHide.ChessMens = ChessMens;
        }
    }
   public void SpawnEnemy(char idCard, int selectedX, int selectedY)
    {
        if (deckId[1] == '1')
        {
            WhiteDeckHide.DSSpawnEnemy( idCard,  selectedX,  selectedY);
          //  WhiteDeckHide
        }
        else
        {
            BlackDeckHide.DSSpawnEnemy(idCard, selectedX, selectedY);
        }
    }


    public void changeTure(bool isWhite)
    {
        this.isWhiteTurn = isWhite;
    }

    private string SetDeckNumber(string deckID)
    {
        string deck_number = "";
        for (int i = 2; i < deckID.Length-1; i++)
            deck_number += deckID[i];
        return deck_number;
    }

    private void Update()
    {
        UpdateSelection(); // co klatkę gra sprawdza na jakie pole najechał gracz
        MakeMove(); // w zależności gdzie i czy kliknięto gracz wykona ruch, zaatakuje lub zmieni aktywnego piona

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(deckId);
            Debug.Log(enemyDeckId);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
           
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
           
                sendToServer.sendShowGamesInServer();
           
        }

    }

    private void UpdateSelection()
    {
        if (!Camera.main) //jeśli brakuje kamery nic się nie dzieje
            return;

        RaycastHit hit;
       
        //warunek niżej, Raycast "wypuszcza" promień z kamery do miejsca gdzie kliknął gracz i zwraca do hit to na co promień natrafił (jeżeli nie trafił na nic zwraca false)
        if (Physics.Raycast(   
            Camera.main.ScreenPointToRay(Input.mousePosition),
            out hit,
            25.0f, //długość promienia
            LayerMask.GetMask("Board"))) //warunek określający nad jakim polem gracz ma umieszczoną myszkę
        {
            
            selectedX = (int)hit.point.x;
            selectedY = (int)hit.point.z;
            BoardHighlitghs.Instance.HighlightAllowedMove(selectedX, selectedY);
        }

        else
        { 
            BoardHighlitghs.Instance.DestroySelection();
            selectedX = -1;
            selectedX = -1;
        }

    } 

    private void MakeMove()
    {
        if (Input.GetMouseButtonDown(0)) //wduszenie lewego przycisku myszy
        {
            if (selectedX >= 0 || selectedY >= 0) //sprawdzenie czy kliknięto na planszy
            {
                if (SelectedChessman == null) //jeśli nic nie wybrano wybierz danego piona 
                {
                    try
                    {                  
                        SelectChessman(selectedX, selectedY); //zmiana wybranego piona 
                        sendToServer.sendMoveToServer(selectedX, selectedY); //zmiana wybranego piona 
                    }
                    catch (Exception e)
                    {

                    }
                }
                else //jeśli wcześniej wybrano piona rusz nim na wybrane pole
                {
                    try
                    {
                        sendToServer.sendPlayerMove(selectedX, selectedY, SelectedChessman);
                        MoveAndAttackChessman(selectedX, selectedY, true); //rusz wybrany pion na daną pozycję                    
                    }
                    catch (Exception e)
                    {

                    }
                }

            }
        }
    }

    private void SelectChessman(int x, int y)
    {
        if (ChessMens[x, y] == null) //sprawdzenie czy na wybranej pozycji jest pion
            return;
        if (ChessMens[x, y].IsWhite != isWhiteTurn || yourWhite!= ChessMens[x, y].IsWhite) //sprawdzenie czy pion ma kolor danego gracza
            return;

        selectSpecificChessman(x, y);
    } 
    private void selectSpecificChessman(int x, int y)
    {
        BoardHighlitghs.Instance.HideAll();
        SelectedChessman = ChessMens[x, y]; //zmiana wybranego piona
        ChessMens[x, y].UpdateMove(); //sprawdzenie jakie ruchy,ataki są dozwolone

        BoardHighlitghs.Instance.HighlightAllowedMoves(SelectedChessman.PossibleMove, SelectedChessman.PossibleAtacks); //podświetlenie planczy
    }


    public void DSmoveChessMan(int zPolaX, int zPolaY, int naPoleX, int naPoleY)
    {
        selectSpecificChessman(zPolaX, zPolaY);
        MoveAndAttackChessman(naPoleX, naPoleY, false);
    }


    private void MoveAndAttackChessman(int x, int y, bool isPlayer)
    {
        ChessMan target = ChessMens[x, y];
        moveChessman(x, y, isPlayer);
        if (IsGameStart)
            makeAttackChessMan(x, y, target, isPlayer);

        BoardHighlitghs.Instance.HideAll();
        SelectedChessman = null; //klinięcie w inne niż możliwe miejsce anuluje wybór
    }

  

    private void moveChessman(int x, int y, bool isPlayer)
    {
        if (IsGameStart)
            if (SelectedChessman.PossibleMove[x, y]) // można wykonać taki ruch?
        {
            ChessMens[SelectedChessman.CurrentX, SelectedChessman.CurrentY] = null; //wybrany pion 'znika' z aktualnej pozycji
            SelectedChessman.transform.position = GetTileCenter(x, y);
            SelectedChessman.SetPosition(x, y);
            ChessMens[x, y] = SelectedChessman;
                if(isPlayer)
            UpdateMove();

           if (SelectedChessman.firstmove) //wykonanie pierwszego ruchu potrzebne przy pionach
               SelectedChessman.firstmove = false;

        }
    }

    private void makeAttackChessMan(int x, int y, ChessMan target, bool isPlayer)
    {
        if (SelectedChessman.PossibleAtacks[x,y]) //można atakować
        {
            if (SelectedChessman.GetComponent<Distance>() == null) //jeśli pion nie atakuje z dystasnu musi przemieścić się w kierunku przeciwnika
            {
                int[] newPositions = CalculateNewPosition(x, y, SelectedChessman.CurrentX, SelectedChessman.CurrentY); //funkcja sprawia że pion zostaję na pozycji "przed" celem 

                ChessMens[SelectedChessman.CurrentX, SelectedChessman.CurrentY] = null; //wybrany pion 'znika' z aktualnej pozycji
                SelectedChessman.transform.position = GetTileCenter(newPositions[0], newPositions[1]);
                SelectedChessman.SetPosition(newPositions[0], newPositions[1]);

                ChessMens[newPositions[0], newPositions[1]] = SelectedChessman;

            }

            AtackChessMan(target);
            if (isPlayer)
                UpdateMove();

            if (SelectedChessman.firstmove) //wykonanie pierwszego ruchu potrzebne przy pionach
                SelectedChessman.firstmove = false;
        }

        BoardHighlitghs.Instance.HideAll();
        SelectedChessman = null; //klinięcie w inne niż możliwe miejsce anuluje wybór
    }


    private void AtackChessMan(ChessMan target)
    {
        //target.GetComponent<Animator>().Play("take_damage");

        int damage = target.Hp - SelectedChessman.Dmg; //zmniejszenie HP

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
        Destroy(target.gameObject); //zniszczenie figury, automatycznie sniknie też z listy
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
        if (number_of_move >= 2)
            {
            sendToServer.sendEndTureToServer();
            isWhiteTurn = !isWhiteTurn;
            number_of_move = 0;
        }
    }

    private int[] CalculateNewPosition(int newX,int newY,int CurrentX,int CurrentY) //funkcja potrzebna przy ataku która ustawia pion jedno pole "przed" celem w zależności od strony ataku
    {
        int[] results = new int[2];

        if (newX - CurrentX < 0) //poruszam się w lewo
            results[0] = newX + 1;

        if (newX - CurrentX > 0) //poruszam się w prawo
            results[0] = newX - 1;

        if (newX - CurrentX == 0) //nie poruszam się w osi x
            results[0] = CurrentX;

        if (newY - CurrentY < 0) //poruszam się w dół
            results[1] = newY + 1;
            
        if (newY - CurrentY > 0) //poruszam się w górę
            results[1] = newY - 1;

        if (newY - CurrentY == 0) //nie poruszam się w osi y
            results[1] = CurrentY;

        return results;
    }

  


}
