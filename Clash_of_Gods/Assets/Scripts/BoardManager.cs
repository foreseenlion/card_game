using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private MeterialChanges materialChange;
    public GameObject textMessage;
    public GameObject textMessageYourMove;
    public GameObject textMessageWaitForEnemy;
    GameObject signWaitForPlayer;
    HandlingEffects handlingEffects;
    public ChamInfo chamInfo;

    public GameObject champInfoObject;

    public static BoardManager Instance { get; set; }
    public ChessMan[,] ChessMens { get; set; } //tablica wszystkich pionów

    public void DoTheEffects(bool start)
    {
        foreach (ChessMan chess in ChessMens)
        {
            //&& chess.Effects.Count > 0
            if (chess != null)
            {
                try
                {
                    if (start)
                    {
                        if ((chess.IsWhite && yourWhite) || (!chess.IsWhite && !yourWhite))
                            foreach (Effects effects in chess.Effects)
                            {

                                DoEffectAndDesReduceLength(effects, chess);

                            }
                    }
                    else
                        if ((chess.IsWhite && !yourWhite) || (!chess.IsWhite && yourWhite))
                        foreach (Effects effects in chess.Effects)
                        {

                            DoEffectAndDesReduceLength(effects, chess);
                        }
                }
                catch
                {

                }


            }
        }
    }

    void DoEffectAndDesReduceLength(Effects effects, ChessMan chess)
    {
        if (effects.iterateEveryTurn)
            handlingEffects.getEffectTure(effects.TypeOfEffect, effects.valueEffect, chess);
        effects.length -= 1;
        if (effects.length == -1) { }else
        if (effects.length <= 0)
        {
            handlingEffects.backToNormal(effects, chess);
            chess.Effects.Remove(effects);
        }
    }

    public void DoTheEffectsAppear(string type, int value, string toEnemyAppearEffect)
    {
        foreach (ChessMan chess in ChessMens)
        {
            if (chess != null)
            {
                try
                {
                    if (toEnemyAppearEffect== "enemy")
                    {
                        if ((chess.IsWhite && !yourWhite) || (!chess.IsWhite && yourWhite))
                            handlingEffects.getEffectTure(type, value, chess);
                    }
                    else if(toEnemyAppearEffect== "ally")
                    {
                        if ((chess.IsWhite && yourWhite) || (!chess.IsWhite && !yourWhite))
                            handlingEffects.getEffectTure(type, value, chess);
                    }
                    else if (toEnemyAppearEffect == "all")
                    {
                            handlingEffects.getEffectTure(type, value, chess);
                    }

                }
                catch
                {

                }


            }
        }
    }


    public string DeckId
    {
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
        handlingEffects = new HandlingEffects();
        chamInfo = champInfoObject.GetComponent<ChamInfo>();
        materialChange = GameObject.FindObjectOfType<MeterialChanges>();
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

        signWaitForPlayer = ShowSignWaitForAPlayer(textMessageWaitForEnemy);

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
            WhiteDeckHide.DSSpawnEnemy(idCard, selectedX, selectedY);
            //  WhiteDeckHide
        }
        else
        {
            BlackDeckHide.DSSpawnEnemy(idCard, selectedX, selectedY);
        }
    }

    public void spawnMainGods()
    {
        Destroy(signWaitForPlayer);
        if (yourWhite)
        {
            setGods(WhiteDeck, BlackDeckHide);
        }
        else
        {
            setGods(BlackDeck, WhiteDeckHide);
        }
        ShowSign(textMessage);

    }
    void ShowSign(GameObject _sign)
    {
        var sign = Instantiate(_sign,
        new Vector3(4.1f, 1f, 3f),
         Quaternion.Euler(40, 0, 0));
        Destroy(sign, 3000);
    }
    GameObject ShowSignWaitForAPlayer(GameObject _sign)
    {
        return Instantiate(_sign,
        new Vector3(4.1f, 1f, 3f),
         Quaternion.Euler(40, 0, 0));

    }

    private void setGods(CardManager you, CardManager enemy)
    {
        you.spawnMainGods(3, 0, false);
        enemy.spawnMainGods(4, 7, true);
    }
    public void showTextMessageYourTure()
    {
        ShowSign(textMessageYourMove);
    }

    public void changeTure(bool isWhite)
    {
        this.isWhiteTurn = isWhite;
    }

    private string SetDeckNumber(string deckID)
    {
        string deck_number = "";
        for (int i = 2; i < deckID.Length - 1; i++)
            deck_number += deckID[i];
        return deck_number;
    }

    private void Update()
    {
        materialChange.changeSmooth();

        UpdateSelection(); // co klatkę gra sprawdza na jakie pole najechał gracz
        MakeMove(); // w zależności gdzie i czy kliknięto gracz wykona ruch, zaatakuje lub zmieni aktywnego piona

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(deckId);
            Debug.Log(enemyDeckId);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            sendToServer.sendDC();
            SceneManager.LoadScene("Choose_God");
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
            try
            {
                chamInfo.setChampInfo(ChessMens[selectedX, selectedY].IsWhite, ChessMens[selectedX, selectedY].name, ChessMens[selectedX, selectedY].Hp,
                ChessMens[selectedX, selectedY].Dmg, ChessMens[selectedX, selectedY].move, ChessMens[selectedX, selectedY].Move_limit, ChessMens[selectedX, selectedY].PowreDescription, ChessMens[selectedX, selectedY].Effects);
            }
            catch
            {

            }
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
        if (ChessMens[x, y].IsWhite != isWhiteTurn || yourWhite != ChessMens[x, y].IsWhite) //sprawdzenie czy pion ma kolor danego gracza
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
                if (isPlayer)
                    UpdateMove();

                if (SelectedChessman.firstmove) //wykonanie pierwszego ruchu potrzebne przy pionach
                    SelectedChessman.firstmove = false;

            }
    }

    private void makeAttackChessMan(int x, int y, ChessMan target, bool isPlayer)
    {
        if (SelectedChessman.PossibleAtacks[x, y]) //można atakować
        {
            if (SelectedChessman.range == 0) //jeśli pion nie atakuje z dystasnu musi przemieścić się w kierunku przeciwnika
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
        if (SelectedChessman.TypeOfEffect == "dmg")
            target.Effects.Add(new Effects("dmg", SelectedChessman.ImposesValueEffect, SelectedChessman.ImposesLength, SelectedChessman.effectName, SelectedChessman.DescriptionEffect, true));
        if (SelectedChessman.TypeOfEffect == "nodmg")
        {
            target.Effects.Add(new Effects("nodmg", SelectedChessman.ImposesValueEffect, SelectedChessman.ImposesLength, SelectedChessman.effectName, SelectedChessman.DescriptionEffect, false, target.Dmg));
            handlingEffects.getEffectTure("nodmg", SelectedChessman.ImposesValueEffect, target);
        }
        int damage = target.Hp - SelectedChessman.Dmg; //zmniejszenie HP
        IfHeDies(target, damage);


    }

    public void IfHeDies(ChessMan target, int HpLeft)
    {
        if (HpLeft <= 0)
            KillChessMan(target);
        else
            target.Hp = HpLeft;
    }


    private void KillChessMan(ChessMan target)
    {
        if (target.MainGod) //jeśli to król zakończ grę
        {
            GameManager.instance.Winner = isWhiteTurn ? "White" : "Black";
            GameManager.instance.Condition = "kiling the God";
            if ((target.IsWhite && yourWhite) || (!target.IsWhite && !yourWhite))
                myReligion.youWin = true;
            else myReligion.youWin = false;
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
            DoTheEffects(false);
            sendToServer.sendEndTureToServer();
            isWhiteTurn = !isWhiteTurn;
            number_of_move = 0;
        }
    }


    private int[] CalculateNewPosition(int newX, int newY, int CurrentX, int CurrentY) //funkcja potrzebna przy ataku która ustawia pion jedno pole "przed" celem w zależności od strony ataku
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
