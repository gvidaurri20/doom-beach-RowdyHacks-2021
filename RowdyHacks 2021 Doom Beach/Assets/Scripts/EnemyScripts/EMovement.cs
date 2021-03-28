using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/*
 * EnemyMovement.cs
 * This program focuses on anything related to enemies moving
 *
 */
public class EMovement : MonoBehaviour
{
    Transform player; // What the enemy is moving towards.  (Should be the player)
    PlayerLife playerHealth;  // Reference to the PlayerHealth script for the health of the player
    EnemyLife enemyHealth;  // Reference to the EnemyHealth script for the health of the current enemy
    NavMeshAgent nav; // The NavMeshAgent for the current enemy GameObject

    // Awake is called regardless of when game is started.
    // This finds the player object with the appropriate tag and sets up the health of the player/current enemy.  It also sets up the NavMeshAgent.
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerLife>();
        enemyHealth = GetComponent<EnemyLife>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // If both the enemy and the player are still alive, the enemy will follow the player, otherwise the enemy should just die in place.
        if (enemyHealth.enemyCurrentHealth > 0 && playerHealth.playerCurrentHealth > 0)
        {
            nav.SetDestination(player.position);  // Tells the enemy to head towards this position (which is the player).
        }
        else
        {
            nav.enabled = false;
        }
    }
}
