using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * PlayerLife.cs
 * 
 * This program focuses on things related to the player's health.
 *
 */
public class PlayerLife : MonoBehaviour
{
    public int playerStartingHealth = 100; // How much health player has at the start
    public int playerCurrentHealth; // The current amount of health the player has
    public Slider healthSlider; // Reference to the UI element for health slider on the screen canvas
    public AudioClip playerDeathClip;  // The audio that plays when the player dies
    public deathMenu deathMenu;
    Animator animator;  // Reference to animator component
    AudioSource playerHurtAudio;  // Reference to audio source for player
    PlayerMovement playerMovement;  // Reference to the PlayerMovement script
    PlayerShootingLaser playerShooting;  // Reference to the PlayerShootingLaser script
    public static bool isDead;  // Determines if player is dead or not
    public bool increaseDefPowerUpOn = false; // Determines whether the player currently has power up activated to reduce attacks

    //public DeathMenu deathMenu; // Reference to the DeathMenu script to access the death screen


    // Awake is called regardless of when game is started.
    // Sets up the components from the player in order to use in other functions
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerHurtAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShootingLaser>();
        playerCurrentHealth = playerStartingHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
    }

    // Called anytime the player takes damage from an enemy.
    public void TakeDamage(int amount)
    {
        if (increaseDefPowerUpOn == false)
            playerCurrentHealth = playerCurrentHealth - amount; // Subtracts the damage from the player's current health
        else
            playerCurrentHealth = playerCurrentHealth - (amount / 2); // If increase defense power up is on, then amount of damage sustained is reduced by half

        healthSlider.value = playerCurrentHealth; // Updates the health slider on screen with new health after damage
        playerHurtAudio.Play();  // Plays the audio clip for the player getting hurt
        if(playerCurrentHealth <= 0 && !isDead) // If the player runs out of health, this will execute
        {
            Death();  // To death sequence
        }
    }

    // Called anytime the player acquires a health power up
    public void RestoreHealth()
    {
        healthSlider.value = playerCurrentHealth; // Updates the health slider on screen with new health after restoration
    }

    // Called whenever the player dies
   public void Death()
    {
        isDead = true;  // Sets the boolean for death to true

        playerShooting.DisableEffects();

        animator.SetTrigger("death"); // Sets the death trigger for the death animation to show

        // Plays the death audio clip when player dies
        playerHurtAudio.clip = playerDeathClip;
        playerHurtAudio.Play();

        playerMovement.enabled = false;  // Disables movement for player since they are dead
        playerShooting.enabled = false;  // Disables shooting for player since they are dead

        StartCoroutine(WaitForDeathAnimation()); // Allows the death animation to play out before Death Menu appears
        deathMenu.ToggleEndMenu ();
    }

    // Waits for player's fall and die animation
    private IEnumerator WaitForDeathAnimation()
    {
        // yield on a new YieldInstruction that waits for 4 seconds.
        yield return new WaitForSeconds(4f);
        //deathMenu.ToggleEndMenu(EnemiesPerRoundManager.currentLevel, EnemiesPerLevelManager.enemiesLeft); // Brings up the death screen with the information about the current level and the amount of enemies left


        // ****************************************************************************
        // ****************************************************************************
        // TODO: LOAD THE DEATH MENU (Game Over Screen) SCREEN HERE! THE LINE ABOVE
        // WAS MY PREVIOUS VERSION OF WHAT I USED IN MY ASSIGNMENT SO YOU CAN GET AN
        // IDEA OF WHAT TO PUT.  NOTICE AT THE TOP OF THIS PROGRAM I HAD TO COMMENT 
        // OUT THE DEATH MENU TOO. BUT YEAH, DO SOMETHING SIMILAR.  DELETE THIS
        // COMMENT ONCE YOU DO THAT    - GABE
        // ****************************************************************************
        // ****************************************************************************
    }
}
