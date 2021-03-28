using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * EnemyAttack.cs
 * This program focuses on the attacks of the enemies
 *
 */
public class EnemyAttack : MonoBehaviour
{
    public float timeAttackDelay = 0.5f;  // The amount of time between each enemy attack
    public int attackDamage = 10;  // How much damage each successful hit does to the player on their health

    Animator animator;  // Reference to animator component
    GameObject player;  // Represents the player
    PlayerLife playerHealth;  // Reference to the amount of health the player currently has
    EnemyLife enemyHealth;  // Reference to the amount of health this enemy currently has
    bool playerInRange;  // Boolean to tell whether or not the player is within the hit range of the player (sphere collider)
    float timer;  // Timer will be used to make sure everything runs within good timed intervals (not too fast/slow etc)

    // Awake is called regardless of when game is started.
    // Sets up the components from the player and enemy in order to use in other functions
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerLife>();
        enemyHealth = GetComponent<EnemyLife>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime; // Timer is constantly increasing

        // Allows the enemy to attack given that enough time has passed, the player is within range of their collider, the enemy is still alive (obviously), and that the player doesn't currently possess an invincibility power up
        if (timer >= timeAttackDelay && playerInRange && enemyHealth.enemyCurrentHealth > 0)// && PowerUps.invincibilityOn == false)
        {
            EnemyAttackPlayer();
        }

        // If the player dies, then the appropriate animation will be displayed showing player death
        if (playerHealth.playerCurrentHealth <= 0)
        {
            animator.SetTrigger("PlayerDead");
        }                                                
    }

    // Function is called anytime player is within the sphere collider of the enemy.  Let's enemy know player is within range of attack.
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
            animator.SetBool("isAttacking", true);
        }
    }

    // Function is called anytime player leaves the sphere collider of the enemy.  Let's enemy know player is outside range of attack.
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            animator.SetBool("isAttacking", false);
        }
    }

    // Deals with the enemy attacking the player and having the player take damage.
    void EnemyAttackPlayer()
    {
        timer = 0f;  // Timer is set back to zero to make sure the enemy doesn't attack too many times all at once
        
        // If the player's current health is greater than 0, this will allow the enemy to attack and damage the player
        // (Basically you can only attack the player if they still have health left)
        if(playerHealth.playerCurrentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);  // Damage done to player is based off the enemy that did the attacking
        }
    }
}
