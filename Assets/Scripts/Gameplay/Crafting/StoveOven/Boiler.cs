using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Boiler : BaseMixer
{

    [SerializeField] private Bar progressBar;
    [SerializeField] private Slider tempSlider;
    [SerializeField] private float processDuration = 5f;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI temperatureText;
    private Coroutine cookingCoroutine;
    private bool isCooking = false;
    private float sliderValue;

    protected override void Start()
    {
        toggleButton.onClick.AddListener(ToggleCooking);
        progressBar.maxValue = processDuration;
        tempSlider.onValueChanged.AddListener(OnSliderValueChanged);
        UpdateTemperatureText(tempSlider.value);
        base.Start();
    }
    private void UpdateTemperatureText(float temperature)
    {
        // Use an f-string to format the temperature value
        temperatureText.text = $"Water amount: {temperature:0.0}°C";
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

        cookingCoroutine = StartCoroutine(Cook());
        base.DisableSlots();
        isCooking = true;
        UpdateButtonLabel();
    }

    private void StopCooking()
    {
        if (!isCooking) return;
        StopCoroutine(cookingCoroutine);
        base.EnableSlots();
        float progress = progressBar.value;
        progressBar.Reset();
        isCooking = false;
        UpdateButtonLabel();
        // 1st param is in time
        DetermineResult(progress / processDuration, sliderValue);
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

    private void OnSliderValueChanged(float value)
    {
        // Handle the value change event
        Debug.Log("Slider value changed: " + value);
        // Perform operations based on the new slider value
        sliderValue = value;
        UpdateTemperatureText(value);

    }


    private IEnumerator Cook()
    {
        float elapsedTime = 0f;
        while (elapsedTime < processDuration)
        {
            progressBar.value += Time.deltaTime; // Adjust based on your cooking speed
            yield return null;
        }
        StopCooking();
    }
    private void DetermineResult(float progress, float waterAmount)
    {
        if (progress < 0.5f)
        {
            Debug.Log("Under-biled result");
            base.Process(-1);
            // Create alternate result for under-cooked
        }
        else if (progress >= 0.5f && progress < 1f)
        {
            Debug.Log("Partially boiled result");
            if (waterAmount >= 2)
            {

                base.Process(3);
            }
            else
            {
                base.Process(-1);
            }
        }
        else
        {
            Debug.Log("Fully boiled result");
            if (waterAmount == 3)
            {

                base.Process(3);
            }
            else
            {
                base.Process(-1);
            }
        }
    }
}
