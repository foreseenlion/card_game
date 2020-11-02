using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
public class Network : MonoBehaviour
{
	SocketIOComponent socket;

	void Start()
	{
		socket = GetComponent<SocketIOComponent>();

		// This line will set up the listener function
		socket.On("connectionEstabilished", onConnectionEstabilished);
		socket.On("foreignMessage", onForeignMessage);
		socket.On("tureEnd", onTureEnd);
		socket.On("moveTo", onPlayerMove);
	}

	// This is the listener function definition
	void onConnectionEstabilished(SocketIOEvent evt)
	{
		Debug.Log("Player is connected: " + evt.data.GetField("id"));
	}

	void onForeignMessage(SocketIOEvent evt)
	{
		Debug.Log("pozycja x" + evt.data.GetField("x"));
		Debug.Log("pozycja y" + evt.data.GetField("y"));
	}


    void onTureEnd(SocketIOEvent evt)
    {

		if (evt.data.GetField("ture").str == "0")
        Debug.Log(evt.data.GetField("ture"));
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





}
