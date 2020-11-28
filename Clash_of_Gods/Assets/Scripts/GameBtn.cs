using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameBtn : MonoBehaviour
{

    public Button surrednder;
    public Button endTure;
    // Start is called before the first frame update
    void Start()
    {
        surrednder.onClick.AddListener(btnSurrender);
        endTure.onClick.AddListener(btnEndTurn);

        
    }


    void btnSurrender()
    {   
        if (BoardManager.Instance.IsGameStart)
        {
            BoardManager.Instance.sendToServer.sendDC();
            myReligion.youWin = false;
            SceneManager.LoadScene("End_Game");

        }
        
    }

    void btnEndTurn()
    {
        if (BoardManager.Instance.IsGameStart)
        {
            if (BoardManager.Instance.whoseTurn())
            {
                BoardManager.Instance.number_of_move = BoardManager.Instance.number_of_move_reverse;
                BoardManager.Instance.UpdateMove();
            }
           
        }
            
    }
}
