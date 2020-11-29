using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextDevelop : MonoBehaviour
{
    private BoardManager boardManager;
    private void Start()
    {
        boardManager = BoardManager.Instance;
        chamInfo = champInfoObject.GetComponent<ChamInfo>();
        tureMessage = textTureMessage.GetComponent<TextTureMessage>();
        signWaitForPlayer = ShowSignWaitForAPlayer(textMessageWaitForEnemy);
    }

    GameObject signWaitForPlayer;
    
    public GameObject textMessage;

    public GameObject textMessageYourMove;

    public GameObject textMessageWaitForEnemy;

    public Text HpManipulationText;

    public GameObject textTureMessage;
    [HideInInspector]
    public ChamInfo chamInfo;
    [HideInInspector]
    public TextTureMessage tureMessage;

    public GameObject champInfoObject;

    public void destroySign()
    {
        Destroy(signWaitForPlayer);
    }
  public void ShowSign(GameObject _sign)
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

    public void showTextMessageYourTure()
    {
        tureMessage.setTureInfo(boardManager.whoseTurn(), boardManager.yourWhite, boardManager.number_of_move_reverse - boardManager.number_of_move);
        ShowSign(textMessageYourMove);
    }
}
