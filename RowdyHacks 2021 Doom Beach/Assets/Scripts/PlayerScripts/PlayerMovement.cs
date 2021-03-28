using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * PlayerMovement.cs
 * 
 * This program focuses on anything related to the player's movement.
 *
 */

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;  // The Player's Character Controller

    public float verticalSpeed = 60f;  // How fast the player moves forward/backward
    public float horizontalSpeed = 50f;  // How fast the player moves left/right
    Vector3 movement; // Store movement applied to player
    Animator animator; // Reference to Animator component for player
    public float gravity = -9.81f;  // Gravity constant

    [SerializeField]
    private GameObject _playerChar; // Represents the player

    bool isGrounded = false; // Whether or not the player is on the ground
    Vector3 velocity; // How fast the velocity of falling is (used for gravity)

    // Awake is called regardless of when game is started.
    // This sets up the layermasks to set the floor and the Animator.  Also, gets the player's rigidbody.
    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Updates the game in response to any game physics.  This will focus on the player's movement.
    void FixedUpdate()
    {
        PlayerMove(); // Anytime the player pushes a keyboard button, the things in this function will activate
    }

    // Activated whenever the player utilizes the keyboard buttons.  Focuses on movement.
   void PlayerMove()
    {
        // Checks to see that player is grounded and has gravity pull them down just a bit
        isGrounded = controller.isGrounded;
        if (isGrounded && movement.y < 0)
        {
            movement.y = -2f;
        }

        float verticalMove = Input.GetAxis("Vertical");  // Allows input from W and S keys
        float horizontalMove = Input.GetAxis("Horizontal");  // Allows input from W and S keys

        // If the player presses the W or S keys, the player will either run forward, walk backward, or stop if no keys are pressed
        if (verticalMove > 0) // If player presses W, player will run forward (with animation)
        {
            RunForward();
            AnimatingRunningForward(verticalMove);
        }
        else if (verticalMove < 0) // If player presses S, player will walk backward (with animation)
        {
            WalkBackward();
            AnimatingWalkingBackward(verticalMove);
        }
        else // If player stops pressing any directional key, the player will stop moving and stop its animation to return to idle
        {
            AnimatingRunningForward(0);
            AnimatingWalkingBackward(0);
        }

        if (horizontalMove < 0) // If player presses A, player will strafe left (with animation)
        {
            StrafingLeft();
            AnimatingStrafingLeft(horizontalMove);
        }
        else if (horizontalMove > 0) // If player presses D, player will strafe right (with animation)
        {
            StrafingRight();
            AnimatingStrafingRight(horizontalMove);
        }
        else // If player stops pressing any directional key, the player will stop moving and stop its animation to return to idle
        {
            AnimatingStrafingLeft(0);
            AnimatingStrafingRight(0);
        }

        // Gravity pulls down player
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Player will move forward whenever the W key is pressed
    void RunForward()
    {
        movement = transform.forward;  // Player moves forward
        movement = movement.normalized * verticalSpeed * Time.deltaTime;  // Player moves forward in the direction they are facing given the speed and time of the game

        controller.Move(movement); // Moves the player's character controller with respect to the movement vector
    }

    // Allows the appropriate animation for running forwards
    void AnimatingRunningForward(float verticalAxis)
    {
        bool moving = verticalAxis != 0f;
        animator.SetBool("isRunning", moving);
    }

    // Player will move backward whenever the S key is pressed
    void WalkBackward()
    {
        movement = -transform.forward; // Player moves backward
        movement = movement.normalized * horizontalSpeed * Time.deltaTime;  // (even though it should be vertical speed, we are using horizontalSpeed since we want character to walk slow)

        controller.Move(movement); // Moves the player's character controller with respect to the movement vector
    }

    // Allows the appropriate animation for walking backwards
    void AnimatingWalkingBackward(float verticalAxis)
    {
        bool moving = verticalAxis != 0f;
        animator.SetBool("isMovingBack", moving);
    }

    // Player will strafe left whenever the A key is pressed
    void StrafingLeft()
    {
        movement = -transform.right; // Player strafes left
        movement = movement.normalized * horizontalSpeed * Time.deltaTime; // Player moves to the left of the direction they are facing given the speed and time of the game

        controller.Move(movement); // Moves the player's character controller with respect to the movement vector
    }

    // Allows the appropriate animation for strafing left
    void AnimatingStrafingLeft(float horizontalAxis)
    {
        bool moving = horizontalAxis != 0f;
        animator.SetBool("isMovingLeft", moving);
    }

    // Player will strafe right whenever the D key is pressed
    void StrafingRight()
    {
        movement = transform.right; // Player strafes right
        movement = movement.normalized * horizontalSpeed * Time.deltaTime; // Player moves to the right of the direction they are facing given the speed and time of the game

        controller.Move(movement); // Moves the player's character controller with respect to the movement vector
    }

    // Allows the appropriate animation for strafing right
    void AnimatingStrafingRight(float horizontalAxis)
    {
        bool moving = horizontalAxis != 0f;
        animator.SetBool("isMovingRight", moving);
    }
}
