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

    public Effects(string typeOfEffect, int valueEffect, int length, string name, string description)
    {
        TypeOfEffect = typeOfEffect;
        this.valueEffect = valueEffect;
        this.length = length;
        this.name = name;
        this.description = description;
    }
}
