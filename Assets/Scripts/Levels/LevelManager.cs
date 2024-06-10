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

    private void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
        GameObject customer = customerManager.SpawnCustomer();
        customerInteraction = customer.GetComponentInChildren<CustomerInteraction>();
        customerInteraction.OrderDone += HandleOrderDone;
        customerManager.GoIn();
        countdownTimer.SetStartSec(5400);
        countdownTimer.StartTimer();
        SoundManager.Instance.Play(audioClip);
        SoundManager.Instance.SetVolume(0.5f);

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
    float score = 0;

    private void HandleOrderDone(bool isDone)
    {
        if (isDone)
        {
            customerinStage -= 1;
            // makin lama makin mendekati 1
            score = (1 - countdownTimer.fastesttime / countdownTimer.StartSeconds) * 100;
            Debug.Log("you win!");
        }
        else
        {
            score = 0;
            Debug.Log("you lose!");
        }
        Debug.Log("Score is: " + score + " " + countdownTimer.GetFastestTime());
        countdownTimer.StopTimer();
        gameOverMenu.ShowCanvas(score, countdownTimer.GetFastestTime());
    }

    private void TimeRanOut()
    {
        Debug.Log("you lose!");
        score = 0;
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