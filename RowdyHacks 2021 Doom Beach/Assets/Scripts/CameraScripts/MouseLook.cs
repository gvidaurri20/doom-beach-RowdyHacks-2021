using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * MouseLook.cs
 * This program focuses on the aspects of moving the camera left and right for the player in First-Person.
 *
 */

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f; // How sensitive the mouse is

    public Transform playerBody;  // The player's body, used as a reference for the camera

    //float xRotation = 0f;  Originally used to describe x-axis rotations in order to move the camera for y-axis things (NOT NEEDED)

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is dead or the game is paused, the cursor should no longer be locked, otherwise, it should keep the cursor locked while in game
        if (PlayerLife.isDead == true || pMenu.GameIsPaused == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // If the player is still alive or the game isn't paused, then the camera should along moves along the x-axis.
        if (PlayerLife.isDead == false || pMenu.GameIsPaused == false)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Gets the mouse direction for x
            playerBody.Rotate(Vector3.up * mouseX); // Rotates the camera along the x-axis direction
        }

    }
}
