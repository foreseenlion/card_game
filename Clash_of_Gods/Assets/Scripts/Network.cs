using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class Network : MonoBehaviour
{
	SocketIOComponent socket;
	BoardManager boardManager;
	bool yourfirst;
	SendToServer sendToServer;


	void Start()
	{
		socket = GetComponent<SocketIOComponent>();
		boardManager = GameObject.FindObjectOfType<BoardManager>();
		sendToServer = new SendToServer();
		// This line will set up the listener function
		socket.On("connectionEstabilished", onConnectionEstabilished);
		socket.On("foreignMessage", onForeignMessage);
		socket.On("tureEnd", onTureEnd);
		socket.On("moveTo", onPlayerMove);
		socket.On("drawDeck", onGameStart);
		socket.On("onEnemyDeck", onEnemyDeckBlack);
		socket.On("enemyDeckWhite", onEnemyDeckWhite);
		socket.On("spawnEnemy", onSpwanEnemy);
	}





	// This is the listener function definition
	void onConnectionEstabilished(SocketIOEvent evt)
	{
		Debug.Log("Player is connected: " + evt.data.GetField("id"));
		// symulowanie startu
		sendToServer.sendStartGameInfo(boardManager.religionId);
		Debug.Log(boardManager.DeckId);
	}



	void onSpwanEnemy(SocketIOEvent evt)
	{
		string poleX = evt.data.GetField("PoleX").ToString();
		string poleY = evt.data.GetField("PoleY").ToString();
		string id = evt.data.GetField("id").ToString();
		//DSSpawnEnemy
		Debug.Log("Spawn na pozycji x" + poleX + " y" + poleY + " id karty: " + id);
		boardManager.SpawnEnemy(id[1], int.Parse(poleX), int.Parse(poleY));
		Debug.Log("Spawn na pozycji x" + poleX + " y" + poleY + " id karty: " + id);
	}


	void onForeignMessage(SocketIOEvent evt)
	{
	//	Debug.Log("pozycja x" + evt.data.GetField("x"));
	//	Debug.Log("pozycja y" + evt.data.GetField("y"));

		JSONObject tmp = evt.data;
		
		int x=int.Parse(evt.data.GetField("x").ToString());
		int y=int.Parse(evt.data.GetField("y").ToString());
		//Debug.Log(x);
		//Debug.Log(y);
		
	}


    void onTureEnd(SocketIOEvent evt)
    {
        if (boardManager.yourWhite == true)
        {
			boardManager.number_of_move = 0;
			boardManager.changeTure(true);
        }
        else
        {
			boardManager.number_of_move = 0;
			boardManager.changeTure(false);
		}
		Debug.Log("ture start");	
	}

	void onEnemyDeckBlack(SocketIOEvent evt)
    {
		string enemydeckId= evt.data.GetField("enemyDeck").ToString();
		boardManager.enemyDeckId = enemydeckId;
		sendToServer.sendMyDeckToEnemy(boardManager.DeckId);
		boardManager.SetEnemyDecks();
	}

	void onEnemyDeckWhite(SocketIOEvent evt)
	{
		string enemydeckId = evt.data.GetField("deckWhite").ToString();
		boardManager.enemyDeckId = enemydeckId;
		boardManager.SetEnemyDecks();
	}

	void onPlayerMove(SocketIOEvent evt)
	{	
		//Debug.Log("id fidury: "+evt.data.GetField("idPionka"));
		//Debug.Log("z pola x"+evt.data.GetField("poleStartoweX"));
		//Debug.Log("z pola y" + evt.data.GetField("poleStartoweY"));
		//Debug.Log("na pole x" + evt.data.GetField("poleDoceloweX"));
		//Debug.Log("na pole y" + evt.data.GetField("poleDoceloweY"));

		int zPolaX = int.Parse(evt.data.GetField("poleStartoweX").ToString());
		int zPolaY = int.Parse(evt.data.GetField("poleStartoweY").ToString());
		int naPoleX = int.Parse(evt.data.GetField("poleDoceloweX").ToString());
		int naPoleY = int.Parse(evt.data.GetField("poleDoceloweY").ToString());
		boardManager.DSmoveChessMan(zPolaX,zPolaY,naPoleX,naPoleY);

	}

	void onGameStart(SocketIOEvent evt)
	{ 
		string deck = evt.data.GetField("deck").ToString();
		boardManager.DeckId = deck;
		
		if (deck[1] == '0')
		{
			boardManager.yourWhite = true;
		}
		else
		{
			boardManager.yourWhite = false;
		}
	}



}
