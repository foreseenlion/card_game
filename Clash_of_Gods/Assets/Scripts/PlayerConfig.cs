using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConfig : NetworkBehaviour
{
    // Start is called before the first frame update
    public GameObject playerCamera;

    public bool test;

     NetworkManager networkManager;
   
    void Start()
    {  
        
    
        if (isLocalPlayer)
        {
            playerCamera.SetActive(true);
        }
        else
        {
            playerCamera.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
