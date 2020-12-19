using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choose_God_ButtonController : MonoBehaviour
{
    public int indexHorizontal,indexVertical;
    [SerializeField] bool keyDown;
    [SerializeField] int maxIndex;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Horizontal") < 0)
                {
                    if (indexHorizontal > 0)
                    {
                        indexHorizontal--;
                    }
                }
                else if (Input.GetAxis("Horizontal") > 0)
                {
                    if (indexHorizontal < maxIndex)
                    {
                        indexHorizontal++;
                    }
                }
                keyDown = true;
            }
        }
        else if (Input.GetAxis("Vertical") != 0)
        {
            if (!keyDown)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    if (indexVertical < maxIndex)
                    {
                        indexVertical++;
                    }
                }
                else if (Input.GetAxis("Vertical") > 0)
                {
                    if (indexVertical > 0)
                    {
                        indexVertical--;
                    }
                }
                keyDown = true;
            }
        }
        else
        {
            keyDown = false;
        }
    }
}
