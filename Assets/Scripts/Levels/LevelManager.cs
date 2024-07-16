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
    private int scoreMult = 1;
    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        GameObject customer = customerManager.SpawnCustomer();
        customerInteraction = customer.GetComponentInChildren<CustomerInteraction>();
        FoodDetail a = customerInteraction.customer.DesiredFood;
        scoreMult = ParseFood(a);
        customerInteraction.OrderDone += HandleOrderDone;
        customerManager.GoIn();
        countdownTimer.SetStartSec(5400);
        countdownTimer.StartTimer();
        SoundManager.Instance.Play(audioClip);
        // SoundManager.Instance.SetVolume(0.5f);

    }
    private int ParseFood(FoodDetail food)
    {
        int val = 1;
        switch (food.itemName)
        {
            case "Tahu Telur":
                val = 15;
                break;
            case "Mie Ongklok":
                val = 25;
                break;
            case "Satay Bandeng":
                val = 30;
                break;
            case "Nasi Lengko":
                val = 25;
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

    private void HandleOrderDone(bool isDone)
    {
        float score = PlayerPrefs.GetFloat("score");
        if (isDone)
        {
            customerinStage -= 1;
            // makin lama makin mendekati 1
            score += (1 - countdownTimer.fastesttime / countdownTimer.StartSeconds) * scoreMult;
            Debug.Log((1 - countdownTimer.fastesttime / countdownTimer.StartSeconds));
        }
        else
        {
            score += 0;
            Debug.Log("you lose!");
        }
        Debug.Log("Score is: " + score + " " + countdownTimer.GetFastestTime());
        countdownTimer.StopTimer();
        gameOverMenu.ShowCanvas(score, countdownTimer.GetFastestTime());
    }

    private void TimeRanOut()
    {
        float score = PlayerPrefs.GetFloat("score");
        Debug.Log("you lose!");
        score += 0;
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