using System;
using reciets;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private CountDownTimer countdownTimer;
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private GameOverMenu gameOverMenu;
    private CustomerInteraction customerInteraction;
    private int customerinStage = 1;
    [SerializeField] private AudioClip audioClip;
    private int foodTime = 1;

    public int currentLevel;
    private PlayerData loadedData;
    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        GameObject customer = customerManager.SpawnCustomer();
        customerInteraction = customer.GetComponentInChildren<CustomerInteraction>();
        FoodDetail a = customerInteraction.customer.DesiredFood;
        foodTime = ParseFood(a);
        customerInteraction.OrderDone += HandleOrderDone;
        customerManager.GoIn();
        countdownTimer.SetStartSec(5400);
        countdownTimer.StartTimer();
        SoundManager.Instance.Play(audioClip);
        // SoundManager.Instance.SetVolume(0.5f);

        loadedData = SaveSystem.LoadPlayer();
        if (loadedData == null)
        {
            // If no save data exists, initialize new player data
        }

    }

    private int ParseFood(FoodDetail food)
    {
        int val = 1;
        switch (food.itemName)
        {
            case "Tahu Telur":
                val = 1800;
                break;
            case "Mie Ongklok":
                val = 3900;
                break;
            case "Satay Bandeng":
                val = 4200;
                break;
            case "Nasi Lengko":
                val = 2100;
                break;
            default:
                break;
        }
        return val;
    }
    public void TogglePauseGame()
    {
        if (GameManager.Instance.isGamePaused)
        {
            pauseCanvas.gameObject.SetActive(false);
        }
        else
        {
            pauseCanvas.gameObject.SetActive(true);
        }
        GameManager.Instance.TogglePauseGame();
    }
    private void Awake()
    {
        countdownTimer.OnCountdownEnd += TimeRanOut;
    }

    private void OnDestroy()
    {
        customerInteraction.OrderDone -= HandleOrderDone;
        countdownTimer.OnCountdownEnd -= TimeRanOut;
    }
    private int CalculateScore(float foodTime, float elapsedTime)
    {
        // Normalize the time for the food item
        // Ensure elapsedTime does not exceed foodTime for efficiency calculation
        float effectiveElapsedTime = Math.Min(elapsedTime, foodTime);
        float efficiency = (effectiveElapsedTime / foodTime) * 100;

        // Calculate penalty if elapsedTime exceeds foodTime
        float penalty = (elapsedTime > foodTime) ? (elapsedTime - foodTime) / (float)foodTime * 100 : 0;

        // Calculate final score, ensuring it does not go below 1
        int finalScore = Math.Clamp((int)(efficiency - penalty), 1, 100);

        return finalScore;

    }
    private void HandleOrderDone(bool isDone)
    {
        float score = PlayerPrefs.GetFloat("score");
        countdownTimer.StopTimer();
        if (isDone)
        {
            customerinStage -= 1;
            // makin lama makin mendekati 1
            score = (1 - countdownTimer.elapsedTime / foodTime) * 100;
            // score = CalculateScore(foodTime, countdownTimer.elapsedTime);
            // 
            // Debug.Log((1 - countdownTimer.fastesttime / countdownTimer.StartSeconds));
        }
        else
        {
            // score += 0;
            // score = 0;
            Debug.Log(countdownTimer.elapsedTime);
            score = (1 - (countdownTimer.elapsedTime / foodTime)) * 100;
            // 1- 10/35
            // score = CalculateScore(foodTime, 40);

            Debug.Log("you lose!");
        }
        Debug.Log("Score is: " + score + " " + countdownTimer.GetFastestTime());
        foreach (var entry in loadedData.LevelsAndScores)
        {
            int level = entry.Key;
            float scorea = entry.Value;
            Debug.Log($"Level {level}: Score {scorea}");
        }
        if (loadedData.GetScore(currentLevel) < score)
        {
            loadedData.SetScore(currentLevel, score);
            SaveSystem.SavePlayer(loadedData);
        }
        foreach (var entry in SaveSystem.LoadPlayer().LevelsAndScores)
        {
            int level = entry.Key;
            float scorea = entry.Value;
            Debug.Log($"Level {level}: Score {scorea}");
        }
        gameOverMenu.ShowCanvas(score, countdownTimer.GetFastestTime());
    }

    private void TimeRanOut()
    {
        float score = PlayerPrefs.GetFloat("score");
        Debug.Log("you lose!");
        score += 0;
        // loadedData.SetScore(currentLevel, score);
        gameOverMenu.ShowCanvas(score, countdownTimer.GetFastestTime());
        // tampilan lose disini or something
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMenu()
    {
        GameManager.Instance.LoadMenu();
    }
}