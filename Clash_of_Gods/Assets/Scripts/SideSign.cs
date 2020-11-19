using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSign : MonoBehaviour
{
    public Material materialWhite;
    public Material materialBlack;

    public Material materialBlackGod;
    public Material materialWhiteGod;

       public  bool God=false;

    public GameObject Object;
    public void setColor(bool isWhite)
    {
      
        if (isWhite)
        {
            if(!God)
            Object.GetComponent<MeshRenderer>().material = materialWhite;
            else
            Object.GetComponent<MeshRenderer>().material = materialWhiteGod;
        }
        else
        {
            if (!God)
                Object.GetComponent<MeshRenderer>().material = materialBlack;
             else
                Object.GetComponent<MeshRenderer>().material = materialBlackGod;
        }

}
}
