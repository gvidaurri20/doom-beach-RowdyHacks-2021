using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * EnemyManager.cs
 * This program focuses on spawning the enemies
 *
 */
public class EnemyManager : MonoBehaviour
{
    public PlayerLife playerHealth;  // Reference to the PlayerHealth script for the player's current health
    public GameObject enemy; // Reference to the enemy
    public float spawnTime = 3f; // How long between enemy spawns (in seconds)
    public Transform[] spawnPoints;  // The spawn points for the enemies
    public static bool maxEnemiesReached; // Boolean to state whether the max number of enemies have spawned for a particular level

    // Start is called before the first frame update
    void Start()
    {
        maxEnemiesReached = false;
        InvokeRepeating("Spawn", spawnTime, spawnTime);  // Repeats the Spawn() function.  Begins at a particular interval and repeats every interval amount of seconds.
    }

    // Randomly spawns an enemy
    void Spawn()
    {
        // Will only spawn enemies in the level if the amount of enemies currently in the game is less than the specified amount per level
        if (maxEnemiesReached == false)
        {
            // If the player is dead, stop spawning new enemies
            if (playerHealth.playerCurrentHealth <= 0f)
            {
                return;
            }
            
            int spawnPointIndex = Random.Range(0, spawnPoints.Length); // Randomly place enemies around

            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);  // Spawn the enemy
            EnemiesPerRoundManager.enemyCount++; // Increases the counter for the number of enemies on screen at a time

            // If the player is at the boss level, then only 1 enemy will spawn before reaching max.
            if (EnemiesPerRoundManager.isBossLevel == true)
            {
                maxEnemiesReached = true;
            }
            // If the amount of enemies that have been spawned thus far match the level's limit, then no more enemies should spawn
            else if (EnemiesPerRoundManager.enemyCount == EnemiesPerRoundManager.maxNumEnemiesPerRound)
            {
                maxEnemiesReached = true;
            }

        }
        else if(maxEnemiesReached == true && EnemiesPerRoundManager.reachedNextRound == true) // If the player advances to the next level, then reset the enemy count to prepare for next level
        {
            EnemiesPerRoundManager.enemyCount = 0;
            maxEnemiesReached = false;
            EnemiesPerRoundManager.reachedNextRound = false;
        }
    }
}
