using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField]
    bool isWhite;

    public List<Card> deck;

    ChessMan[,] ChessMens;

    bool[,] SpawnAllowed;

 
    

    private void Spawn(GameObject prefab, int x, int y)
    {
        if (ChessMens[x, y] == null) //jezeli na wybranym polu nie ma figuty
        {
            GameObject temp = Instantiate(prefab, GetTileCenter(x, y), Quaternion.Euler(0, 180, 0)) as GameObject; //tworzy obiekt na podstawie prefabu o określonej pozycji
            ChessMens[x, y] = temp.GetComponent<ChessMan>(); //zapisanie figury do tablicy figur
            ChessMens[x, y].SetPosition(x, y); //ustawienie pozycji figury
            temp.transform.SetParent(transform);
        }

    }


    private Vector3 GetTileCenter(int x, int y) //umieszczanie figury na środku danego pola
    {
        Vector3 origin = Vector3.zero;
        origin.x += (1f * x) + 0.5f; //ustawianie na  srodku
        origin.z += (1f * y) + 0.5f;
        return origin;
    }

    public void UpdateSpawn(ChessMan[,] _chessMens) //funkca subskrybująca akcję
    {
        ChessMens = _chessMens;

        foreach (Card card in deck)
        {
            card.onClicked += () =>
            {
                if (isWhite == BoardManager.Instance.isWhiteTurn)
                {
                    BoardHighlitghs.Instance.HideAll();
                    BoardHighlitghs.Instance.HighlightAllowedMoves(isSpawnAllowed()); //podaje możliwe pola do spawnu
                    StartCoroutine(WaitForSpawn(card));
                }

            };

        }

    }

    IEnumerator WaitForSpawn(Card card)
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0)) //jeśli naciśnięto myszkę
            {

                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)) //jeśli "promień" trafił na obiekt
                {
                    int selectedX = BoardManager.Instance.selectedX; //odczyruje pozycje
                    int selectedY = BoardManager.Instance.selectedY;

                    SpawnAllowed = isSpawnAllowed(); //pobiera możliwe pola

                    if (selectedX >= 0 && selectedY >=0 && SpawnAllowed[selectedX,selectedY] == true) //czy można spawnować
                    {

                        Spawn(card.prefab, selectedX, selectedY);  
                        BoardHighlitghs.Instance.HideAll();
                        card.prefab = null;
                        Destroy(card.gameObject); 
                        BoardManager.Instance.UpdateMove(); //wykonano ruch

                    }
                }
            }
            yield return null; //jeśli nie naciśnięto czekaj
        }


    }
  
    private bool[,] isSpawnAllowed()
    {
       int less_than = isWhite ? 1 : 7;
       bool[,] allowedMoves = new bool[8, 8];   //można spawnować tylko na dwóch pierwszych wierszach
        for (int i = 0; i <= 7; i++)
            for (int j = isWhite ? 0 : 6; j <= less_than; j++)
                if (ChessMens[i, j] == null)
                   allowedMoves[i, j] = true;

        return allowedMoves;

    }






}
