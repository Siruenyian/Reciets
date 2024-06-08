using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System;
using reciets;

public class Bar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    // public TextMeshProUGUI craftingText;
    public float maxValue = 100f;
    public float value;
    float lerpSpeed;
    [SerializeField] private Color startColor = Color.blue;
    [SerializeField] private Color endColor = Color.white;
    [SerializeField] private float lerpMultiplier = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        // craftingText.text = "Ingredients: " + ingredients + "%";
        if (value > maxValue)
        {
            value = maxValue;
        }
        lerpSpeed = lerpMultiplier * Time.deltaTime;
        Filler();
        ColorChanger();
    }

    void Filler()
    {

        barImage.fillAmount = Mathf.Lerp(barImage.fillAmount, (value / maxValue), lerpSpeed);
    }

    void ColorChanger()
    {
        Color staminaColor = Color.Lerp(startColor, endColor, (value / maxValue));
        barImage.color = staminaColor;
    }

    public void Decrease(float dcPoint)
    {
        if (value - dcPoint > 0)
        {
            value -= dcPoint;
            //onStaminaChanged?.Invoke();
        }
        else
        {
            value = 0;
        }
    }

    public void Increase(float inPoint)
    {
        if (value < maxValue)
        {
            value += inPoint;
        }
    }

    public void Reset()
    {
        value = 0;
    }
}




