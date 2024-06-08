using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game state variables
    public bool isGamePaused { get; private set; }

    private void Awake()
    {
        // Ensure that only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize game state variables
        isGamePaused = false;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("MainGameScene");
        isGamePaused = false;
        Time.timeScale = 1f; // Ensure game time is running
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isGamePaused = false;
        Time.timeScale = 1f;
    }
    // Method to reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Method to toggle game pause state
    public void TogglePauseGame()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f; // Stop time
    }

    private void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f; // Resume time
    }

    public void QuitGame()
    {
        Application.Quit();
        // If in the editor, stop play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}