using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject HUDdisabler;
    public Camera playerCamera;

    void Update()
    {
        // If the player presses the Escape key and the game is not currently paused and the player isn't dead, it will pause the game.
        // Otherwise if the game is currently paused and the player presses the Escape key, it will unpause the game.
        if (Input.GetKeyDown(KeyCode.Escape) && !GameIsPaused && !PlayerLife.isDead)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused)
        {
            //Resume();
        } 
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
        HUDdisabler.SetActive(false);
        playerCamera.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        HUDdisabler.SetActive(true);
        playerCamera.enabled = true;
    }
    

   public void QuitGame()
   {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
   public void RestartGame()                   
   {                                           
       SceneManager.LoadScene("doomBeach");    
   }                                           

}
