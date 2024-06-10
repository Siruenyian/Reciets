using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Skinner : BaseMixer
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
            progressBar.value += Time.deltaTime; // Adjust based on your cooking speed
            yield return null;
        }
        StopCooking();
    }
    private void DetermineResult(float progress)
    {
        if (!IsAnyIngredientAdded())
        {
            return;
        }
        base.Process(0);
        CountDownTimer.Instance.ReduceTime(900);
    }
}