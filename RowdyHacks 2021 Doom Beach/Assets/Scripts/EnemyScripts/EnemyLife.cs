using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * EnemyLife.cs
 * This program focuses on the current enemy's health and how they take damage from player.
 * Allows game to remove enemy when they die as well.
 * 
 */
public class EnemyLife : MonoBehaviour
{
    public int enemyStartingHealth = 100; // How much health this current enemy has at the start
    public int enemyCurrentHealth; // The current amount of health this enemy has
    public AudioClip enemyDeathClip; // The audio that plays when this enemy dies

    Animator animator; // Reference to animator component
    AudioSource enemyAudio; // Reference to audio source for this enemy
    CapsuleCollider capsuleCollider;  // Reference to this enemy's capsule collider
    bool enemyIsDead; // Determines if this enemy is dead or not

    // Awake is called regardless of when game is started.
    // Sets up the components from this enemy in order to use in other functions
    void Awake()
    {
        animator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        enemyCurrentHealth = enemyStartingHealth;
    }

    // Called anytime this enemy takes damage from the player.
    public void TakeDamage(int amount) //, Vector3 hitPoint)
    {
        if (enemyIsDead)  // If the enemy is dead, then there is no reason to execute the rest of this function
            return;

        enemyAudio.Play();  // Plays the audio clip for the enemy getting hurt
        enemyCurrentHealth = enemyCurrentHealth - amount; // Subtracts the damage from the player's current health

        if (enemyCurrentHealth <= 0) // If this enemy runs out of health, this will execute
        {
            EnemyDeath();  // To death sequence
        }
    }

    // Called whenever the enemy dies
    void EnemyDeath()
    {
        enemyIsDead = true;  // Sets the boolean for death to true

        capsuleCollider.isTrigger = true; // IF the enemy dies, they are no longer an obstacle

        animator.SetTrigger("enemyDead"); // Sets the death trigger for the death animation to show

        // Plays the death audio clip when this enemy dies
        enemyAudio.clip = enemyDeathClip;
        enemyAudio.Play();
    }

    // Called whenever it is time to remove the enemy from the game after they die
    public void RemoveEnemy()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        EnemiesPerRoundManager.enemiesLeft = EnemiesPerRoundManager.enemiesLeft - 1; // Removes 1 enemy from the amount the player needs to progress to the next level
        Destroy(gameObject, 0.5f);
    }
}
