using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
 * EnemiesPerRoundManager.cs
 * 
 * This program focuses on the amount of enemies left per round in a level.  This also focuses on increasing rounds in levels
 * and how many enemies will get displayed. In addition, will figure out what should be displayed in the HUD display for
 * the current level and enemies left.  Also has functions for displaying the current power-up effect message.
 * Will also load the next level (scene) after 5 rounds of enemies.
 *
 */
public class EnemiesPerRoundManager : MonoBehaviour
{
    public static int enemiesLeft; // The amount of enemies left on the round
    public static int maxNumEnemiesPerRound; // This will hold the max amount of set of enemies per round
    public static int enemyCount; // The amount of enemies currently on screen
    public static int currentRound;  // The current round of the player
    public static bool reachedNextRound; // Boolean to refer to when the player reaches the next round

    public Text currentRoundText; // References the text that displays the current round of the player
    public Text passedToNextRoundText; // References the text that displays when the player advances to the next round within a level
    private bool _levelUpDisplay = false; // Represents if round up display text should be displayed or not.
    public Text powerUpTextBox; // References the text that displays which power up the player has acquired
    public Text enemiesLeftText;  // Reference to the text component containing the amount of enemies left in the round

    private float timer = 4; // Represents the amount of time that certain level up or power up messages will appear (set to 4 seconds)
    private bool timerStart = false; // Represents when the 4 second timer should start

    public static bool isBossLevel = false; // Boolean to determine if this is the last level in the game.

    void Awake()
    {
        if(isBossLevel == true)  // If the player is at the boss level, then this will be different
        {
            enemiesLeft = 1; // Begins the game by only allowing 10 enemies at most on the first level
            maxNumEnemiesPerRound = enemiesLeft; // Holds the original amount of enemies left from the current round
            currentRound = 1; // Begins the game at the first level
            enemyCount = 0;  // At the beginning of the game, there are no enemies in the game
            reachedNextRound = false; // Player hasn't advanced to the next round at this moment
        }
        else
        {
            enemiesLeft = 10; // Begins the game by only allowing 10 enemies at most on the first level
            maxNumEnemiesPerRound = enemiesLeft; // Holds the original amount of enemies left from the current round
            currentRound = 1; // Begins the game at the first level
            enemyCount = 0;  // At the beginning of the game, there are no enemies in the game
            reachedNextRound = false; // Player hasn't advanced to the next round at this moment
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // Deactivates the text boxes in the HUD for power-ups and next round messages since the player doesn't use them yet
        passedToNextRoundText.gameObject.SetActive(_levelUpDisplay);
        powerUpTextBox.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isBossLevel == true)
        {
            currentRoundText.text = "Current Round: " + currentRound + "/1"; // What is displayed on the Current Round text component
            enemiesLeftText.text = "Enemies Left: " + 1; // What is displayed on the Enemies Left text component
        }
        else
        {
            currentRoundText.text = "Current Round: " + currentRound + "/5"; // What is displayed on the Current Round text component
            enemiesLeftText.text = "Enemies Left: " + enemiesLeft; // What is displayed on the Enemies Left text component
        }
        
        // If the player kills all enemies on the level, this will advance them to the next round turn on the level up message to show
        if(enemiesLeft <= 0)
        {
            currentRound++;
            enemiesLeft = maxNumEnemiesPerRound + 5; // At every round, the amount of enemies the player needs to kill will be increased by 5 each time
            maxNumEnemiesPerRound = enemiesLeft; // Holds onto the max amount of enemies per round
            reachedNextRound = true;
            _levelUpDisplay = true;
        }

        timer = timer + Time.deltaTime; // Timer increases every second of game (used only to display messages on screen for a certain period of time)

        // If the player has completed a round, this will turn on the round up display text
        if (_levelUpDisplay == true && timerStart == false)
        {
            passedToNextRoundText.gameObject.SetActive(_levelUpDisplay);
            timer = 0;
            timerStart = true;
        }

        // This makes sure that the round up display text will only display for 4 seconds when a player enters a new round, before turning it off again
        if (((int)timer) >= 4)
        {
            _levelUpDisplay = false;
            passedToNextRoundText.gameObject.SetActive(_levelUpDisplay);
            timerStart = false;
        }



        // After 5 rounds, the level is over.  You beat the game! :) 
        if (currentRound > 5)
        {
            _levelUpDisplay = false;
            currentRoundText.text = "Current Round: X"; // What is displayed on the Current Round text component (no more rounds)
            enemiesLeftText.text = "Enemies Left: " + 0; // What is displayed on the Enemies Left text component (no more enemies)
            passedToNextRoundText.text = "You beat the game! :) ";
            passedToNextRoundText.gameObject.SetActive(true);
            timerStart = false;

            StartCoroutine(WaitForLevelUpMessageOnScreen());  // Wait for level up text to display
        }
    }

    // Allows message on screen for a few seconds letting player know that they passed onto the next level before it changes scenes
    private IEnumerator WaitForLevelUpMessageOnScreen()
    {
        // yield on a new YieldInstruction that waits for 4 seconds.
        yield return new WaitForSeconds(4f);
           #if UNITY_EDITOR
                   UnityEditor.EditorApplication.isPlaying = false;
               #else
                   Application.Quit();
               #endif
    }
    
}
