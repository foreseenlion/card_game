using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class Network : MonoBehaviour
{
	SocketIOComponent socket;
	BoardManager boardManager; 


	void Start()
	{
		socket = GetComponent<SocketIOComponent>();
		boardManager = GameObject.FindObjectOfType<BoardManager>();

		// This line will set up the listener function
		socket.On("connectionEstabilished", onConnectionEstabilished);
		socket.On("foreignMessage", onForeignMessage);
		socket.On("tureEnd", onTureEnd);
		socket.On("moveTo", onPlayerMove);
		socket.On("drawDeck", onGameStart);
	}

	// This is the listener function definition
	void onConnectionEstabilished(SocketIOEvent evt)
	{
		Debug.Log("Player is connected: " + evt.data.GetField("id"));
		boardManager.sendToServer.sendStartGameInfo("G");
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

		Debug.Log("ture start");
		//żeby przetestować użyj przycisku "A"
		if (evt.data.GetField("ture").str == "0")
			boardManager.changeTure(true);
		else
			boardManager.changeTure(false);
		// blokowanie interfejsu i pionków gracza przeciwanika 
	}

	void onPlayerMove(SocketIOEvent evt)
	{	
		Debug.Log("id fidury: "+evt.data.GetField("idPionka"));
		Debug.Log("z pola x"+evt.data.GetField("poleStartoweX"));
		Debug.Log("z pola y" + evt.data.GetField("poleStartoweY"));
		Debug.Log("na pole x" + evt.data.GetField("poleDoceloweX"));
		Debug.Log("na pole y" + evt.data.GetField("poleDoceloweY"));
	}

	void onGameStart(SocketIOEvent evt)
	{ 
		string deck = evt.data.GetField("deck").ToString();
		boardManager.DeckId = deck;
		
		if (deck[1] == '0')
		{
			//Debug.Log("fd");
			boardManager.yourWhite = true;
		}
		else
		{
			boardManager.yourWhite = false;
		}
	}



}
