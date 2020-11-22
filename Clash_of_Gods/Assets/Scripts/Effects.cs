using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getEffect(int effect, int valueEffect, ChessMan chessMan)
    {
        switch (effect)
        {
            case 0:
                timeDmg(valueEffect, chessMan);
                break;
        }
    }

    public void timeDmg(int valueEffect, ChessMan chessMan)
    {
        chessMan.Hp -= valueEffect;
    }


}
