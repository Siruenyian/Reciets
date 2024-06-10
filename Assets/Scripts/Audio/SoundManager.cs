using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
