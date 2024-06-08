using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Oven : BaseMixer
{
    [SerializeField] private Bar progressBar;
    [SerializeField] private Slider tempSlider;
    [SerializeField] private Slider timeSlider;
    [SerializeField] private float processDuration = 5f;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private TextMeshProUGUI timeText;
    private Coroutine cookingCoroutine;
    private bool isCooking = false;
    private float tempsliderValue;
    private float timesliderValue;

    protected override void Start()
    {
        toggleButton.onClick.AddListener(ToggleCooking);
        progressBar.maxValue = processDuration;
        tempSlider.onValueChanged.AddListener(OnTempSliderValueChanged);
        timeSlider.onValueChanged.AddListener(OnTimeSliderValueChanged);
        UpdateButtonLabel();
        base.Start();
    }
    private void Update()
    {
        UpdateTemperatureText(tempSlider.value);
        UpdateTimeText(timeSlider.value);
    }
    private void UpdateTemperatureText(float temperature)
    {
        // Use an f-string to format the temperature value
        temperatureText.text = $"Temperature: {temperature * 60:0.0}°C";
    }
    private void UpdateTimeText(float temperature)
    {
        // Use an f-string to format the temperature value
        timeText.text = $"Time: {temperature * 30:0.0} minutes";
    }
    private void ToggleCooking()
    {
        if (isCooking)
        {
            StopCooking();
        }
        else
        {
            StartCooking();
        }
    }
    private void StartCooking()
    {
        if (isCooking) return;
        cookingCoroutine = StartCoroutine(Cook(0));
        isCooking = true;
        UpdateButtonLabel();
    }

    private void StopCooking()
    {
        if (!isCooking) return;
        StopCoroutine(cookingCoroutine);
        progressBar.Reset();
        isCooking = false;
        UpdateButtonLabel();
    }
    private void UpdateButtonLabel()
    {
        TextMeshProUGUI buttonText = toggleButton.GetComponentInChildren<TextMeshProUGUI>();
        if (isCooking)
        {
            buttonText.text = "Stop";
        }
        else
        {
            buttonText.text = "Start";
        }
    }

    private void OnTempSliderValueChanged(float value)
    {
        // Handle the value change event
        // Debug.Log("Slider value changed: " + value);
        // Perform operations based on the new slider value
        tempsliderValue = value;
        UpdateTemperatureText(value);
    }
    private void OnTimeSliderValueChanged(float value)
    {
        // Handle the value change event
        // Debug.Log("Slider value changed: " + value);
        // Perform operations based on the new slider value
        timesliderValue = value;
        UpdateTimeText(value);
    }

    private IEnumerator Cook(int method)
    {
        float elapsedTime = 0f;
        base.DisableSlots();
        while (elapsedTime < processDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime;
            // / processDuration;
            Debug.Log(progress);
            progressBar.value = progress;

            yield return null;
        }

        progressBar.Reset();
        if (tempsliderValue == 3 && timesliderValue == 1)
        {

            base.Process(method);
        }
        else
        {
            base.Process(-1);
        }
        base.EnableSlots();
        isCooking = false;
        UpdateButtonLabel();
    }
}