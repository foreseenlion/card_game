using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlitghs : MonoBehaviour //podświetlanie możliwych ruchów
{
    [SerializeField]
    GameObject HightlightPrefab;
    [SerializeField]
    GameObject AtackHightlightPrefab;

    private List<GameObject> Hightlights;
    public static BoardHighlitghs Instance { get; set; }
        
    void Start()
    {
        Instance = this;
        Hightlights = new List<GameObject>();
    }

    private GameObject getHightlight(GameObject prefab)  //tworzenie podświetlenia
    {
        GameObject gameObject = Hightlights.Find(g => !g.activeSelf); //znajduje nieaktywny obiekt prześwietlenia
        if (gameObject == null)       //jeżeli nie istnieje tworzy go i dodaje do listy
        {
            gameObject = Instantiate(prefab);
            Hightlights.Add(gameObject);
        }
        return gameObject; //jeżeli istnieje zwraca od razu
    }

    public void HighlightAllowedMoves(bool [,] moves,bool[,] atack) //metoda podświetlająca możliwe ruchy
    {
        
        for(int i = 0; i < 8;i++)       //sprawdza całą planszę
        {
            for (int j = 0; j < 8; j++)
            {
                if(moves[i,j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = getHightlight(HightlightPrefab); //tworzy obiekt podświetlenia
                    gameObject.SetActive(true); //aktywuje go
                    gameObject.transform.position = new Vector3(i+0.5f,0.001f, j+0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
                if (atack[i, j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = getHightlight(AtackHightlightPrefab); //tworzy obiekt podświetlenia
                    gameObject.SetActive(true); //aktywuje go
                    gameObject.transform.position = new Vector3(i + 0.5f,0.001f, j + 0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
            }
        }
    }

    public void HighlightAllowedMoves(bool[,] moves) //metoda podświetlająca możliwe ruchy
    {

        for (int i = 0; i < 8; i++)       //sprawdza całą planszę
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j]) //jeśeli ten ruch jest dozwolony ( = true) 
                {
                    GameObject gameObject = getHightlight(HightlightPrefab); //tworzy obiekt podświetlenia
                    gameObject.SetActive(true); //aktywuje go
                    gameObject.transform.position = new Vector3(i + 0.5f, 0.001f, j + 0.5f); //ustwia na odpowiedniej pozycji na planszy
                }
            
            }
        }
    }


    public void HideAll()
    {
        foreach(GameObject gameObject in Hightlights)
        {
            gameObject.SetActive(false);
        }

    }
    
}
