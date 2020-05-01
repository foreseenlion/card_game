using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{
    public GameObject prefab;
    Camera camera;
    public event Action onClicked;
    

    void Awake()
    {
        camera = FindObjectOfType<Camera>();
    }
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition); // jeżeli po wciśnięciu przycisku kursor będzie nad kartą
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            

            if (Input.GetMouseButtonDown(0))
            {
                try
                {
                    if(onClicked!=null)
                        hit.transform.gameObject.GetComponent<Card>().onClicked.Invoke();
                }
                catch(Exception e)
                { }

            }

        }
    }
   
}
