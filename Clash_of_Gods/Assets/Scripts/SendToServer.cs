using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class SendToServer : MonoBehaviour
{


    private bool tureIsYours;
    SocketIOComponent socket;
    JSONObject message;
    JSONObject ture;
    JSONObject ruch;
    JSONObject atak;
    JSONObject startGame;
    JSONObject myDeckToEnemy;
    JSONObject spawn;

    public SendToServer()
    {
        socket = GameObject.Find("SocketIO").GetComponent<SocketIOComponent>();
        message = new JSONObject();
        ture = new JSONObject();
        ruch = new JSONObject();
        atak = new JSONObject();
        startGame = new JSONObject();
        myDeckToEnemy = new JSONObject();
        spawn = new JSONObject();
    }



    public void SocketIoConnection()
    {
        socket.Connect();
    }

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ACHTUNG!!!!  wszystko dodawane do JSONA musi byc w Stringach bo server nie zaraguje na Emit

    public void sendSpawnToServer(int x, int y, int cardId, int pozostaloRuchow, int iloscRuchow)
    {
        spawn.Clear();
        spawn.AddField("iloscRuchow", pozostaloRuchow.ToString());
        spawn.AddField("pozostaloRuchow", iloscRuchow.ToString());
        spawn.AddField("PoleX", x.ToString());
        spawn.AddField("PoleY", y.ToString());
        spawn.AddField("cardId", cardId.ToString());
        socket.Emit("spawn", spawn);
        spawn.Clear();
    }

    public void sendDebug(string message)
    {
        JSONObject jSONObject = new JSONObject();
        jSONObject.AddField("message", message);
        socket.Emit("debug", jSONObject);
    }


    public void sendShowGamesInServer()
    {
       
        socket.Emit("show");
       
    }

    // funkcja do wywalenia
    public void sendMoveToServer(int x, int y)
    {
        message.Clear();
        message.AddField("PoleX", x.ToString());
        message.AddField("PoleY", y.ToString());
        socket.Emit("move", message);
        message.Clear();
    }
    public void sendEndTureToServer()
    {
        ture.Clear();
        ture.AddField("ture", "1");
        socket.Emit("end", ture);
        tureIsYours = false;
        ture.Clear();
    }

    public void sendMyDeckToEnemy(string deckId)
    {
        myDeckToEnemy.Clear();
        myDeckToEnemy.AddField("deck", SetDeckNumber(deckId));
        socket.Emit("sendMydeckToEnemy", myDeckToEnemy);
        myDeckToEnemy.Clear();
    }


    private string SetDeckNumber(string deckId)
    {
        string deck_number = "";
        for (int i = 1; i < deckId.Length - 1; i++)
            deck_number += deckId[i];
        return deck_number;
    }

    public void sendPlayerMove(int poleDoceloweX, int poleDoceloweY, ChessMan SelectedChessman, int pozostaloRuchow, int iloscRuchow)
    {
        ruch.Clear();
        //Mechanizm id widze w ten sposob że albo server rozdaje id pionka ktore sa tworzone albo pionek tworzony u gracza podaje id drugiemu graczowi
        // Plus lista figur 
      
   
        //trzeba stworzy mechanizm idetyfikowania pionka po id
        ruch.AddField("idPionka", SelectedChessman.idFigure.ToString());
        // Czy pole startowe jest potrzebne jezeli mam id
        ruch.AddField("iloscRuchow", pozostaloRuchow.ToString());
        ruch.AddField("pozostaloRuchow", iloscRuchow.ToString());
        ruch.AddField("poleStartoweX", SelectedChessman.CurrentX.ToString());
        ruch.AddField("poleStartoweY", SelectedChessman.CurrentY.ToString());
        ruch.AddField("poleDoceloweX", poleDoceloweX.ToString());
        ruch.AddField("poleDoceloweY", poleDoceloweY.ToString());
        // wysłanie jsona na server żeby powiedział gdzie co sie ruszyło. 
        socket.Emit("playerMove", ruch);
        ruch.Clear();
    }

    public void sendPlayerAttack(ChessMan atakujacy, ChessMan ofiara)
    {
        // Nietrzeba wiecj wysał "po drugiej stronie" bedzie funkcja wyszukujaca figure
        atak.AddField("atakujacyId", atakujacy.idFigure.ToString());
        atak.AddField("ofiaraId", atakujacy.idFigure.ToString());

    }

    public void sendDC()
    {
        socket.Close();
    }

    public void sendStartGameInfo(string wybranaReligia)
    {

        startGame.AddField("religionId", wybranaReligia);
        socket.Emit("createDeck", startGame);
        startGame.Clear();
       
    }



}
