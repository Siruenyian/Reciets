using UnityEngine;
using System;
using TMPro;
public class CountDownTimer : MonoBehaviour
{
    public event Action OnCountdownEnd;
    private bool timerActive;
    private int startSeconds = 5400;
    public float elapsedTime;
    [HideInInspector] public float currentTime;
    public TextMeshProUGUI crTimeText;
    // public TextMeshProUGUI bsTimeText;
    public string timeString { get; private set; }
    public int StartSeconds { get => startSeconds; set => startSeconds = value; }

    // Start is called before the first frame update
    public static CountDownTimer Instance { get; private set; }
    [SerializeField] private Bar countdownBar;
    void Awake()
    {
        currentTime = StartSeconds;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
        // TimeSpan bestTime = TimeSpan.FromSeconds(SaveSystem.LoadPlayer().bestTime);
        // bsTimeText.text = $"Best Time: {00:00}:{00:00}";
    }
    public void SetStartSec(int timeVal)
    {
        startSeconds = timeVal;
        currentTime = StartSeconds;
        countdownBar.maxValue = startSeconds;
    }
    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime - Time.deltaTime;
            countdownBar.value = currentTime;
            if (currentTime <= 0)
            {
                StopTimer();
                OnCountdownEnd?.Invoke();
            }
        }
        //fastest time
        elapsedTime = StartSeconds - currentTime;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeString = $"{time.Hours:00}:{time.Minutes:00}:{time.Seconds:00}";
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

    public void ReduceTime(float secondstoSkip)
    {
        if ((currentTime - secondstoSkip) < 0)
        {
            currentTime = 0;
            return;
        }
        currentTime -= secondstoSkip;
    }
    public void AddTime(float secondstoAdd)
    {

        currentTime += secondstoAdd;
    }
    public float GetFastestTime()
    {
        return elapsedTime;
    }
}
