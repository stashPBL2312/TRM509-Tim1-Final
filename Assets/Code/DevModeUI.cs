using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevModeUI : MonoBehaviour
{
    // Serialized fields for developer mode UI and pause menu buttons
    [SerializeField] private GameObject _devUI;        // Existing Dev Mode UI
    [SerializeField] private GameObject _btnResume;    // New Resume Button for Pause Menu
    [SerializeField] private GameObject _btnQuitGame;  // New Quit Button for Pause Menu

    private bool _isPaused = false;  // Track the current pause state

    void Start()
    {
        // Initialize the dev UI state based on the Developer Mode setting
        _devUI.SetActive(GameManager.Instance.DevMode);

        // Ensure the pause menu buttons (Resume/Quit) are hidden when the game starts
        _btnResume.SetActive(false);
        _btnQuitGame.SetActive(false);
    }

    void Update()
    {
        // Toggle Developer UI with the 'S' key
        if (Input.GetKeyDown(KeyCode.S))
        {
            _devUI.SetActive(GameManager.Instance.ToggleDevMode());
        }

        // Handle Pause Menu with the 'Escape' key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                ResumeGame();  // If already paused, resume the game
            }
            else
            {
                PauseGame();   // If not paused, pause the game
            }
        }
    }

    // Method to handle pausing the game
    public void PauseGame()
    {
        Time.timeScale = 0;           // Freeze game time
        _isPaused = true;             // Mark game as paused

        // Show the pause menu buttons (Resume/Quit)
        _btnResume.SetActive(true);
        _btnQuitGame.SetActive(true);

        // Show Developer UI if DevMode is active
        _devUI.SetActive(true);
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1;           // Resume game time
        _isPaused = false;            // Mark game as unpaused

        // Hide the pause menu buttons (Resume/Quit)
        _btnResume.SetActive(false);
        _btnQuitGame.SetActive(false);

        // Continue showing Developer UI if DevMode is still active
        _devUI.SetActive(GameManager.Instance.DevMode);
    }

    // Method to quit the game (this method will be assigned to the Quit button)
    public void QuitGame()
    {
#if UNITY_EDITOR
        // If in the editor, log the quit action instead of quitting
        Debug.Log("Quit Game triggered.");
#else
        Application.Quit();  // For builds, this quits the game
#endif
    }
}

