using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Fryer : CraftingBase
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
        temperatureText.text = $"Oil amount: {temperature:0.0}°C";
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
        if (sliderValue == 3)
        {
            base.Process(1);
        }
        else if (sliderValue == 2)
        {
            base.Process(2);
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