using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterialChanges : MonoBehaviour
{
    public Material Material1;
    //in the editor this is what you would set as the object you wan't to change
    public GameObject Object;
    private float value = 0;
    private bool upDown = true;
    void Start()
    { 
        Object.GetComponent<MeshRenderer>().material.SetFloat("_GlossMapScale", 0);
    }

     public void changeSmooth()
    {
        if (upDown)
        {
            value = value + 0.0001f;
            if (value >= 1)
                upDown = false;
        }
        else
        {
            value = value - 0.0001f;
            if (value <= 0)
                upDown = true;
        }
        Object.GetComponent<MeshRenderer>().material.SetFloat("_GlossMapScale", value);
    }

}
