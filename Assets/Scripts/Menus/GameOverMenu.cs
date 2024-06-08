using System;
using TMPro;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private Canvas gameoverMenu;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Bar scoreBar;

    private void Start()
    {
        // scoreBar.maxValue = 100;
    }

    public void ShowCanvas(float score, float bestTime)
    {
        gameoverMenu.gameObject.SetActive(true);
        TimeSpan time = TimeSpan.FromSeconds(bestTime);
        timeText.text = $"Time taken: {time.Minutes:00}:{time.Seconds:00}";
        scoreText.text = $"Rep points: {score:0}";
        PlayerPrefs.SetFloat("score", score);
        scoreBar.value = score;
    }

}