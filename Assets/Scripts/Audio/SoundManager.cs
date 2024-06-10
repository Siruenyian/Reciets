using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        // Ensure that only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioMixer bgmMixer;
    [SerializeField] private AudioMixer sfxMixer;
    // Start is called before the first frame update
    // Update is called once per frame
    public void Play(AudioClip sound)
    {
        bgmSource.Stop();
        bgmSource.clip = sound;
        bgmSource.Play();
    }
    public void Stop()
    {
        bgmSource.Stop();
    }
    public void PlaySFX(AudioClip audio)
    {
        sfxSource.PlayOneShot(audio);
    }

    public void SetVolume(float sliderval)
    {
        // audioSlider.value = globalvol;
        // globalvol = sliderval;
        float mixerval =
        Mathf.Log10(sliderval) * 20;

        if (mixerval == float.NegativeInfinity || mixerval < -60f)
        {
            bgmMixer.SetFloat("BGMMaster", -50f);
            sfxMixer.SetFloat("SFXMaster", -50f);
            return;

        }
        bgmMixer.SetFloat("BGMMaster", mixerval);
        sfxMixer.SetFloat("SFXMaster", mixerval);
    }
}
