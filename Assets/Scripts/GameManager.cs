using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}


public class GameManager : MonoBehaviour
{
    [SerializeField] private Bar scoreBar;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject singleton = new GameObject("GameManager");
                    _instance = singleton.AddComponent<GameManager>();
                }

                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private GameState currentState;
    public event System.Action<GameState> OnGameStateChanged;
    public GameState CurrentState
    {
        get { return currentState; }
        private set
        {
            currentState = value;
            OnGameStateChanged?.Invoke(currentState);
        }
    }

    public void ChangeState(GameState newState)
    {
        CurrentState = newState;

        switch (newState)
        {
            case GameState.MainMenu:
                HandleMainMenu();
                break;
            case GameState.Playing:
                HandlePlaying(0);
                break;
            case GameState.Paused:
                HandlePaused();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    private void HandleMainMenu()
    {
        // Handle main menu logic
        Debug.Log("Main Menu");
        SceneManager.LoadScene("MainMenu");
    }

    private void HandlePlaying(int level)
    {
        // Handle playing logic
        Debug.Log("Playing");
        SceneManager.LoadScene("GameScene" + level);
    }

    private void HandlePaused()
    {
        // Handle paused logic
        Debug.Log("Paused");
    }

    private void HandleGameOver()
    {
        // Handle game over logic
        Debug.Log("Game Over");
    }

    // Utility methods
    public void StartGame()
    {
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Playing)
        {
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; // Stop the game time
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            ChangeState(GameState.Playing);
            Time.timeScale = 1f; // Resume the game time
        }
    }

    public void EndGame()
    {
        ChangeState(GameState.GameOver);
    }

    public void ReturnToMainMenu()
    {
        ChangeState(GameState.MainMenu);
    }
}