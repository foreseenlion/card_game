using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{

    public string TypeOfEffect;
    public int valueEffect;
    public int length;
    public string name;
    public string description;
    public bool iterateEveryTurn;
    public int oldValue;

    public Effects(string typeOfEffect, int valueEffect, int length, string name, string description, bool iterateEveryTurn, int oldValue = 0)
    {
        TypeOfEffect = typeOfEffect;
        this.valueEffect = valueEffect;
        this.length = length;
        this.name = name;
        this.description = description;
        this.iterateEveryTurn = iterateEveryTurn;
        this.oldValue = oldValue;
    }

    


}
