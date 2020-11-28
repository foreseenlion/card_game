using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

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
		socket.On("onEnemyDisconnet", onEnemyDisconnet);
	}

	void onEnemyDisconnet(SocketIOEvent evt)
	{

		socket.Close();
		myReligion.youWin = false;
		SceneManager.LoadScene("End_Game");
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

		int iloscRuchow = int.Parse(evt.data.GetField("iloscRuchow").ToString().Substring(1, evt.data.GetField("iloscRuchow").ToString().Length - 2));
		int pozostaloRuchow = int.Parse(evt.data.GetField("pozostaloRuchow").ToString().Substring(1, evt.data.GetField("pozostaloRuchow").ToString().Length - 2));

		boardManager.SpawnEnemy(id[1], int.Parse(poleX), int.Parse(poleY), iloscRuchow,pozostaloRuchow);
		
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
		boardManager.showTextMessageYourTure();
		boardManager.DoTheEffects(true);
		


	}

	void onEnemyDeckBlack(SocketIOEvent evt)
    {
		string enemydeckId= evt.data.GetField("enemyDeck").ToString();
		boardManager.enemyDeckId = enemydeckId;
		sendToServer.sendMyDeckToEnemy(boardManager.DeckId);
		boardManager.SetEnemyDecks();
		Debug.Log("aaaa");
		boardManager.IsGameStart = true;
		boardManager.spawnMainGods();
	}

	void onEnemyDeckWhite(SocketIOEvent evt)
	{
		Debug.Log("bbbb");
		string enemydeckId = evt.data.GetField("deckWhite").ToString();
		boardManager.enemyDeckId = enemydeckId;
		boardManager.SetEnemyDecks();
		boardManager.IsGameStart = true;
		boardManager.spawnMainGods();
	}

	void onPlayerMove(SocketIOEvent evt)
	{
		
		int zPolaX = int.Parse(evt.data.GetField("poleStartoweX").ToString());
		int zPolaY = int.Parse(evt.data.GetField("poleStartoweY").ToString());
		int naPoleX = int.Parse(evt.data.GetField("poleDoceloweX").ToString());
		int naPoleY = int.Parse(evt.data.GetField("poleDoceloweY").ToString());

		int iloscRuchow = int.Parse(evt.data.GetField("iloscRuchow").ToString().Substring(1, evt.data.GetField("iloscRuchow").ToString().Length-2));
		int pozostaloRuchow = int.Parse(evt.data.GetField("pozostaloRuchow").ToString().Substring(1, evt.data.GetField("pozostaloRuchow").ToString().Length - 2));

		boardManager.DSmoveChessMan(zPolaX,zPolaY,naPoleX,naPoleY, iloscRuchow, pozostaloRuchow);

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
