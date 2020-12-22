
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    [SerializeField]
    bool isWhite;

    int idSelectedCard;
  public  GameObject mainGodPrefab;
    List<GameObject> deck = new List<GameObject>();
    List<GameObject> EnemyDeck = new List<GameObject>();
  
    Dictionary<char, int> indexPrefabCard =new Dictionary<char, int>();
    ChessMan[,] chessMens;


    bool[,] SpawnAllowed;

    public ChessMan[,] ChessMens { get => chessMens; set => chessMens = value; }

    private int idFigury()
    {
        return Random.Range(0, 1000000000);
    }

    private void Spawn(GameObject prefab, int x, int y, bool enemy) //tworzenie figury na planszy
    {
        if (chessMens[x, y] == null) //jezeli na wybranym polu nie ma figuty
        {
            
            GameObject temp;
           if (enemy)
                temp = Instantiate(prefab, GetTileCenter(x, y), Quaternion.Euler(0, 0, 0)) as GameObject; //tworzy obiekt na podstawie prefabu o określonej pozycji
           else
               temp = Instantiate(prefab, GetTileCenter(x, y), Quaternion.Euler(0, 180, 0)) as GameObject; //tworzy obiekt na podstawie prefabu o określonej pozycji
        
            temp.GetComponent<ChessMan>().IsWhite = isWhite;
            chessMens[x, y] = temp.GetComponent<ChessMan>(); //zapisanie figury do tablicy figur
            chessMens[x, y].SetPosition(x, y); //ustawienie pozycji figury
            chessMens[x, y].idFigure = idFigury();
            temp.transform.parent = BoardManager.Instance.transform;
            setSideSign(isWhite, chessMens[x, y]);
            chessMens[x, y].IsYou = !enemy;
            chessMens[x, y].ChampIsInBoard1 = true;
            checkAppearEffect(chessMens[x, y],enemy);
        }
    }

void checkAppearEffect(ChessMan chessMan, bool enemy)
    {
        if (chessMan.toEnemyAppearEffect != "me")
        {
            string result;
            if (enemy)
                result = negativ(chessMan.toEnemyAppearEffect);
            else
                result = chessMan.toEnemyAppearEffect;
            if (chessMan.toAppearEffect != null && chessMan.toAppearEffect != "")
                BoardManager.Instance.handlingEffects.DoTheEffectsAppear(chessMan.toAppearEffect, chessMan.ImposesValueEffect, result);
        }
        else
        {   if(chessMan.toAppearEffect== "hydra")
            chessMan.Effects.Add(new Effects("hydra", 1,10, "Regrowth", "The hydra is still growing heads. She gains 1 damage with 10 turn", true, 1));
            if (chessMan.toAppearEffect == "addture")
                BoardManager.Instance.handlingEffects.getEffectTure("addture", chessMan.ImposesValueEffect, chessMan);
            if (chessMan.toAppearEffect == "healture")
                chessMan.Effects.Add(new Effects("healture", 2, -1, chessMan.effectName, chessMan.DescriptionEffect, true, 1));
        }
    }
  
    string negativ(string toEnemyAppearEffect)
    {
        if (toEnemyAppearEffect == "ally")
            return "enemy";
        else if (toEnemyAppearEffect == "enemy")
            return "ally";
         return toEnemyAppearEffect;
    }


    private Vector3 GetTileCenter(int x, int y) //umieszczanie figury na środku danego pola
    {
        Vector3 origin = Vector3.zero;
        origin.x += (1f * x) + 0.5f; //ustawianie na  srodku
        origin.z += (1f * y) + 0.5f;
        return origin;
    }

    bool SpawDone;


    IEnumerator WaitForSpawn(Card card)  //funkcja kóra "czeka" aż gracz wybierzę pole do spawnu
    {
       myReligion.SpawStop = true;
        SpawDone = true;
        //StopCoroutine("WaitForSpawn"); // zatrzymaj pozostałe instancje  
        BoardHighlitghs.Instance.HideAll();

        SpawnAllowed = isSpawnAllowed();

        BoardHighlitghs.Instance.HighlightAllowedMoves(isSpawnAllowed()); //podaje możliwe pola do spawnu
        BoardManager.Instance.SpawDone = true;
        yield return new WaitForSeconds(1f);

        while (myReligion.SpawStop)
        {
            
            if (Input.GetMouseButtonDown(0)) //jeśli naciśnięto myszkę
            {
               
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) //jeśli "promień" trafił na obiekt
                {
                    
                    int selectedX = BoardManager.Instance.selectedX; //odczyruje pozycje
                    int selectedY = BoardManager.Instance.selectedY;
                    
                    //pobiera możliwe pola
                    if (SpawDone)
                    {
                        
                        if (selectedX >= 0 && selectedY >= 0 && SpawnAllowed[selectedX, selectedY] == true) //czy można spawnować
                        {
                            Debug.Log(" aaa");
                            SendToServer sendToServer = new SendToServer();
                            EnterSpawn(card, selectedX, selectedY, false, false);
                            BoardManager.Instance.UpdateMove();
                            sendToServer.sendSpawnToServer(selectedX, selectedY, idSelectedCard, BoardManager.Instance.number_of_move, BoardManager.Instance.number_of_move_reverse);
                            SpawDone = false;
                            yield break;  // wykonano ruch

                        }
                        else // odkliknięcie
                        {
                            Debug.Log(" bbb");
                            BoardHighlitghs.Instance.HideAll();
                            yield break;
                        }
                    }
                }

            }
            yield return null; //jeśli nie naciśnięto czekaj
        }
    }
    private void setSideSign(bool isYou, ChessMan chessMan)
    {
        SideSign sideSign;
        try
        {
            sideSign = chessMan.GetComponentInChildren<SideSign>();
            sideSign.setColor(isYou);
        }
        catch
        {

        }
    }
    void EnterSpawn(Card card, int selectedX, int selectedY, bool enemy , bool spawnMainGod)
    {
        StopCoroutine("WaitForSpawn"); // zatrzymaj pozostałe instancje
        BoardHighlitghs.Instance.HideAll();

        Spawn(card.prefab, selectedX, selectedY, enemy);
        BoardHighlitghs.Instance.HideAll();
        card.prefab = null;
        if(!spawnMainGod)
        Destroy(card.gameObject);

    }
    public void DSSpawnEnemy(char idCard, int selectedX, int selectedY)
    {
        Card card = new Card();

        card.prefab = deck[int.Parse(idCard.ToString())].GetComponent<Card>().prefab;
        EnterSpawn(card, selectedX, selectedY,true, false);
    }

    public void spawnMainGods(int selectedX, int selectedY, bool enemy)
    {
        Card card = new Card();
        card.prefab = mainGodPrefab.GetComponent<Card>().prefab;

        EnterSpawn(card, selectedX, selectedY, enemy, true);

    }


 public void DSCreateDeckEnemy(string cards)
    {
        deck = CreateDeck(cards);
    }


    private bool[,] isSpawnAllowed()
    {
        int less_than = 1;
        bool[,] allowedMoves = new bool[8, 8];   //można spawnować tylko na dwóch pierwszych wierszach
        for (int i = 0; i <= 7; i++)
            for (int j =  0 ; j <= less_than; j++)
                if (chessMens[i, j] == null)
                    allowedMoves[i, j] = true;

        return allowedMoves;
    }
    private List<GameObject> CreateDeck(string cards)
    {
        Debug.Log(cards);
        GameObject[] tempDeck = new GameObject[10];
        List<GameObject> result = new List<GameObject>();
        switch (cards[0])
        {
            case 'G':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Greek");
                break;

            case 'E':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Egipt");
                break;

            case 'N':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Nord");
                break;
            case 'S':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Slavic");
                break;
            default:
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Greek");
                break;
        }

        
        mainGodPrefab = tempDeck[9];
        for (int i = 1; i < cards.Length; i++)
        {
            int index = (int)System.Char.GetNumericValue(cards[i]);
            // słownik do idetyfikacji kart przeciwnikow  1: string(liczba karty) 2: index na ktorym jest ta karta w liscie prefabow
            indexPrefabCard.Add(cards[i], i);

            result.Add(tempDeck[index]);  
        }
        
        return result;
    }

    public void InstantiateDeck(string cards)
    {
       deck= CreateDeck(cards);
        
        float startX = -1f;

        for (int i = 0; i < deck.Count; i++)
        {
           var temp = Instantiate(deck[i], transform);
           temp.transform.localPosition = new Vector3(startX + i, 0.2f, -1f);

            temp.GetComponent<Card>().id = i;

            temp.GetComponent<Card>().onHover += () =>
            {
                try
                {
                    string name = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().name;
                    int hp = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().Hp;
                    int dmg = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().Dmg;
                 
                    int move_limit = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().Move_limit;
                    string power = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().PowreDescription;
                    List<Effects> effects = temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().Effects;

                    Sprite imageAttack= temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().imageAttack;
                    Sprite imageMove= temp.GetComponent<Card>().prefab.GetComponent<ChessMan>().imageMove;
                    BoardManager.Instance.GetComponent<TextDevelop>().chamInfo.setChampInfo(BoardManager.Instance.yourWhite, name+"(Clone)", hp,
                  dmg, move_limit, power, effects, imageMove, imageAttack);
                }
                catch
                {

                }
            };

            temp.GetComponent<Card>().onClicked += () =>
            {  
                if (BoardManager.Instance.IsGameStart)
                    if (isWhite == BoardManager.Instance.isWhiteTurn )
                {
                    idSelectedCard = temp.GetComponent<Card>().id;

                        StartCoroutine(WaitForSpawn(temp.GetComponent<Card>()));
                }

            };

        }

    }
}
