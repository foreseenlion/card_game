using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlitghs : MonoBehaviour //podświetlanie możliwych ruchów
{
    [SerializeField]
    GameObject HightlightPrefab; //prefab standardowego podświetlenia
    [SerializeField]
    GameObject AtackHightlightPrefab; //prefab podświetlenia ataku (work in progress)
    [SerializeField]
    GameObject SelectHightlightPrefab; //prefab podświetlenia ataku (work in progress)

    

    private List<GameObject> Hightlights; //lista podświetlonych obiektów 
    private GameObject SelectionHightlight;

    public static BoardHighlitghs Instance { get; set; } // można się odnosić do konkretnego BoardHighlights bez FindObjectOfType, coś w rodzaju singletonu
        
    void Start()
    {
        Instance = this;
        Hightlights = new List<GameObject>();
    }

  

    public void HighlightAllowedMoves(bool [,] moves,bool[,] atack) //metoda podświetlająca możliwe ruchy
    {
        HideAll();
        if (BoardManager.Instance.whoseTurn())
        for(int i = 0; i < 8;i++)       //sprawdza całą planszę
        {
            for (int j = 0; j < 8; j++)
            {
                if(moves[i,j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = Instantiate(HightlightPrefab); //tworzy obiekt podświetlenia
                    Hightlights.Add(gameObject);
                    gameObject.transform.position = new Vector3(i+0.5f,0.001f, j+0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
                if (atack[i, j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = Instantiate(AtackHightlightPrefab); //tworzy obiekt podświetlenia
                    Hightlights.Add(gameObject); //aktywuje go
                    gameObject.transform.position = new Vector3(i + 0.5f,0.001f, j + 0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
            }
        }
    }

    public void HighlightAllowedMoves(bool[,] moves) //metoda podświetlająca możliwe ruchy
    {
        HideAll();
        if (BoardManager.Instance.whoseTurn())
            for (int i = 0; i < 8; i++)       //sprawdza całą planszę
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = Instantiate(HightlightPrefab); //tworzy obiekt podświetlenia
                    Hightlights.Add(gameObject); //aktywuje go
                    gameObject.transform.position = new Vector3(i + 0.5f, 0.001f, j + 0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
            
            }
        }
    }

    public void HighlightAllowedMove(int x,int y)
    {
         //znajduje nieaktywny obiekt prześwietlenia
        if (SelectionHightlight != null)       //jeżeli nie istnieje tworzy go i dodaje do listy inaczej podświetlenia nakładają się na siebie
        {
            Destroy(SelectionHightlight);
            SelectionHightlight = Instantiate(SelectHightlightPrefab);
        }
        else
        {
            SelectionHightlight = Instantiate(SelectHightlightPrefab);
        }
        SelectionHightlight.SetActive(true); //aktywuje go
        SelectionHightlight.transform.position = new Vector3(x + 0.5f, 0.001f, y + 0.5f); //ustwia na odpowiedniej pozycji na planszy
    }

    public void HideAll() // dezaktywuje wszystkie podświetlenia
    {
        foreach(GameObject gameObject in Hightlights)
        {
            if(gameObject != null)
            Destroy(gameObject);
        }

    }
    public void DestroySelection()
    {
        if(SelectionHightlight!= null)
            Destroy(SelectionHightlight);
    }
    
}
