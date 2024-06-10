using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cutter : BaseMixer
{
    [SerializeField] private Bar progressBar;
    [SerializeField] private float maxspamHit = 5f;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private float timeoutDuration = 2f;
    [SerializeField] private float progressIncrement = 1f;
    private Coroutine timeoutCoroutine;
    private bool isCooking = false;

    protected override void Start()
    {
        toggleButton.onClick.AddListener(ToggleCooking);
        progressBar.maxValue = maxspamHit;
        UpdatePressText(0);
        UpdateButtonLabel();
        base.Start();
    }
    private void UpdatePressText(float number)
    {
        // Use an f-string to format the number value
        temperatureText.text = $"Choping level: {number:0}";
    }
    private void ToggleCooking()
    {
        if (isCooking)
        {
            UpdateProgress();
        }
        else
        {
            StartCooking();
        }
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
        }
        timeoutCoroutine = StartCoroutine(TimeoutCoroutine());
    }

    private void UpdateProgress()
    {
        progressBar.Increase(progressIncrement);
        UpdatePressText(progressBar.value);
        if (progressBar.value >= maxspamHit)
        {
            StopCooking();
        }
    }

    private void StartCooking()
    {
        if (isCooking) return;

        base.DisableSlots();
        isCooking = true;
        progressBar.Reset();
        UpdateButtonLabel();
        timeoutCoroutine = StartCoroutine(TimeoutCoroutine());
    }

    private void StopCooking()
    {
        isCooking = false;
        base.EnableSlots();
        float progress = progressBar.value;
        progressBar.Reset();
        UpdateButtonLabel();
        // 1st param is in time
        DetermineResult(progress / maxspamHit);
    }
    private void UpdateButtonLabel()
    {
        TextMeshProUGUI buttonText = toggleButton.GetComponentInChildren<TextMeshProUGUI>();
        if (isCooking)
        {
            buttonText.text = "Press to Cook";
        }
        else
        {
            buttonText.text = "Stop Cooking";
        }
    }

    private IEnumerator TimeoutCoroutine()
    {
        yield return new WaitForSeconds(timeoutDuration);

        // Reset the progress if the timeout is reached
        if (isCooking)
        {
            Debug.Log("Timeout reached, resetting progress");
            isCooking = false;
            progressBar.Reset();
            base.EnableSlots();
            UpdateButtonLabel();
        }
    }

    private void DetermineResult(float progress)
    {
        if (!IsAnyIngredientAdded())
        {
            return;
        }
        base.Process(1);
        CountDownTimer.Instance.ReduceTime(300);
    }
}