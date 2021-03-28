using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * CacodemonRotation.cs
 * 
 * Controls the rotation speed of the floating cacodemons over the island
 * 
 */
public class CacodemonRotation : MonoBehaviour
{
    public float speed = .05f; // The speed at which the cacodemons rotate in the game

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed);
    }
}
