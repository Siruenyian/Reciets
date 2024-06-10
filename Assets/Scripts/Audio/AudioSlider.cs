using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    [SerializeField] private String paramName;
    private Slider audioSlider;
    private void Start()
    {
        //globalvol = currentvol;
        audioSlider = GetComponent<Slider>();
        audioSlider.onValueChanged.AddListener(SetVolume);
        audioSlider.maxValue = 1;
        audioSlider.value = 0.5f;
    }

    public void SetVolume(float sliderval)
    {
        // audioSlider.value = globalvol;
        // globalvol = sliderval;
        float mixerval =
        Mathf.Log10(sliderval) * 20;

        if (mixerval == float.NegativeInfinity || mixerval < -60f)
        {
            mixer.SetFloat(paramName, -50f);
            return;

        }
        mixer.SetFloat(paramName, mixerval);

        Debug.Log(Mathf.Log10(sliderval) * 20);
    }

}
