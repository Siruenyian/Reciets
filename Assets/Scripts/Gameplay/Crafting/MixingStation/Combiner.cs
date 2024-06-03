using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combiner : BaseMixer
{
    [SerializeField] private Bar progressBar;
    [SerializeField] private float processDuration = 5f;
    [SerializeField] private Button toggleButton;
    private Coroutine cookingCoroutine;
    private bool isCooking = false;

    protected override void Start()
    {
        toggleButton.onClick.AddListener(ToggleCooking);
        progressBar.maxValue = processDuration;
        UpdateButtonLabel();
        base.Start();
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
        DetermineResult(progress / processDuration);
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

    private IEnumerator Cook()
    {
        float elapsedTime = 0f;
        while (elapsedTime < processDuration)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = elapsedTime; // Adjust based on your cooking speed
            yield return null;
        }
        StopCooking();
    }
    private void DetermineResult(float progress)
    {
        if (progress >= 1f)
        {
            base.Process(0);
        }
    }
}