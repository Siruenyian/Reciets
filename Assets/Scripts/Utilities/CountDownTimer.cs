using UnityEngine;
using System;
using TMPro;
public class CountDownTimer : MonoBehaviour
{
    public event Action OnCountdownEnd;
    private bool timerActive;
    [SerializeField] private int startSeconds = 300;
    public float fastesttime;
    float currentTime;
    public TextMeshProUGUI crTimeText;
    // public TextMeshProUGUI bsTimeText;
    public string timeString { get; private set; }
    public int StartSeconds { get => startSeconds; set => startSeconds = value; }

    // Start is called before the first frame update
    void Awake()
    {
        currentTime = StartSeconds;
        // TimeSpan bestTime = TimeSpan.FromSeconds(SaveSystem.LoadPlayer().bestTime);
        // bsTimeText.text = $"Best Time: {00:00}:{00:00}";
    }
    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;

            if (currentTime <= 0)
            {
                StopTimer();
                OnCountdownEnd?.Invoke();
            }
        }
        //fastest time
        fastesttime = StartSeconds - currentTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeString = $"{time.Minutes:00}:{time.Seconds:00}";
        crTimeText.text = timeString;
    }

    public void StartTimer()
    {
        timerActive = true;
    }
    public void StopTimer()
    {
        timerActive = false;
    }
    public void ResetTimer()
    {
        timerActive = false;
        currentTime = StartSeconds;
    }
    public float GetFastestTime()
    {
        return fastesttime;
    }
}
