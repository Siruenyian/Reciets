using System;
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
    [SerializeField] private Toggle shooktoggleButton;
    private Coroutine cookingCoroutine;
    private bool isCooking = false;
    private float sliderValue;
    [SerializeField] private bool isShook = true;
    protected override void Start()
    {
        progressBar.maxValue = processDuration;
        toggleButton.onClick.AddListener(ToggleCooking);
        tempSlider.onValueChanged.AddListener(OnSliderValueChanged);
        shooktoggleButton.onValueChanged.AddListener(OnToggleShook);
        UpdateWaterText(tempSlider.value);
        UpdateButtonLabel();
        base.Start();
    }

    private String parse(float level)
    {
        String b = "";
        switch (Mathf.FloorToInt(level))
        {

            case 1:
                b = "low";
                break;
            case 2:
                b = "medium";
                break;
            case 3:
                b = "high";
                break;
            default:
                b = " no water";
                break;
        }
        return b;
    }

    private void UpdateWaterText(float level)
    {
        // Use an f-string to format the level value
        temperatureText.text = $"Water amount: {level:0.0}/ {parse(level):'no water'}";
    }

    private void OnToggleShook(bool value)
    {
        // Use an f-string to format the level value
        isShook = value;
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
        // Perform operations based on the new slider value
        sliderValue = value;
        UpdateWaterText(value);

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
        if (!IsAnyIngredientAdded())
        {
            return;
        }
        if (progress < 0.5f)
        {
            Debug.Log("Under-biled result");
            base.Process(-1);
            // Create alternate result for under-cooked
        }
        else if (progress >= 0.5f && progress < 1f)
        {
            Debug.Log("Partially boiled result");
            if (waterAmount >= 2 && isShook)
            {
                if (base.getIngredientinSlot(0).itemName == "Noodle And Veggies")
                {
                    base.Process(3);
                    CountDownTimer.Instance.ReduceTime(1200);
                }
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
                CountDownTimer.Instance.ReduceTime(1200);

            }
            else
            {
                base.Process(-1);
            }
        }
    }
}
