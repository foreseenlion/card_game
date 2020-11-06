
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    bool isWhite;

    List<GameObject> deck = new List<GameObject>();

    ChessMan[,] chessMens;

    bool[,] SpawnAllowed;

    public ChessMan[,] ChessMens { get => chessMens; set => chessMens = value; }

    private int idFigury()
    {
        return Random.Range(0, 1000000000);
    }

    private void Spawn(GameObject prefab, int x, int y) //tworzenie figury na planszy
    {
        if (chessMens[x, y] == null) //jezeli na wybranym polu nie ma figuty
        {
            GameObject temp = Instantiate(prefab, GetTileCenter(x, y), Quaternion.Euler(0, 180, 0)) as GameObject; //tworzy obiekt na podstawie prefabu o określonej pozycji
            temp.GetComponent<ChessMan>().IsWhite = isWhite;
            chessMens[x, y] = temp.GetComponent<ChessMan>(); //zapisanie figury do tablicy figur
            chessMens[x, y].SetPosition(x, y); //ustawienie pozycji figury
            chessMens[x, y].idFigure = idFigury();
            temp.transform.parent = BoardManager.Instance.transform;
        }

    }

    private Vector3 GetTileCenter(int x, int y) //umieszczanie figury na środku danego pola
    {
        Vector3 origin = Vector3.zero;
        origin.x += (1f * x) + 0.5f; //ustawianie na  srodku
        origin.z += (1f * y) + 0.5f;
        return origin;
    }

    IEnumerator WaitForSpawn(Card card)  //funkcja kóra "czeka" aż gracz wybierzę pole do spawnu
    {
        StopCoroutine("WaitForSpawn"); // zatrzymaj pozostałe instancje
        BoardHighlitghs.Instance.HideAll();

        yield return new WaitForEndOfFrame();

        SpawnAllowed = isSpawnAllowed();
        BoardHighlitghs.Instance.HighlightAllowedMoves(SpawnAllowed); //podaje możliwe pola do spawnu

        while (true)
        {
            if (Input.GetMouseButtonDown(0)) //jeśli naciśnięto myszkę
            {

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) //jeśli "promień" trafił na obiekt
                {
                    int selectedX = BoardManager.Instance.selectedX; //odczyruje pozycje
                    int selectedY = BoardManager.Instance.selectedY;

                    //pobiera możliwe pola

                    if (selectedX >= 0 && selectedY >= 0 && SpawnAllowed[selectedX, selectedY] == true) //czy można spawnować
                    {

                        Spawn(card.prefab, selectedX, selectedY);
                        BoardHighlitghs.Instance.HideAll();
                        card.prefab = null;
                        Destroy(card.gameObject);
                        BoardManager.Instance.UpdateMove();
                        yield break;//wykonano ruch

                    }
                    else // odkliknięcie
                    {
                        BoardHighlitghs.Instance.HideAll();
                        yield break;
                    }

                }


            }
            yield return null; //jeśli nie naciśnięto czekaj
        }


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

    private void CreateDeck(string cards)
    {
        GameObject[] tempDeck = new GameObject[10];
        
        switch (cards[0])
        {
            case 'G':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Greek");
                break;

            case 'E':
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Egipt");
                break;

            default:
                tempDeck = Resources.LoadAll<GameObject>("Prefabs/Cards/Greek");
                break;
        }

     
        for (int i = 1; i < cards.Length; i++)
        {
            int index = (int)System.Char.GetNumericValue(cards[i]);
        
            deck.Add(tempDeck[index]);  
        }

    }

    public void InstantiateDeck(string cards)
    {
        CreateDeck(cards);
        
        float startX = -5f;

        for (int i = 0; i < deck.Count; i++)
        {
            var temp = Instantiate(deck[i], transform);
            temp.transform.localPosition = new Vector3(startX + i, 0.2f, -1f);

            temp.GetComponent<Card>().onClicked += () =>
            {
                if (isWhite == BoardManager.Instance.isWhiteTurn )
                {
                    StartCoroutine(WaitForSpawn(temp.GetComponent<Card>()));
                }

            };

        }

    }
}
