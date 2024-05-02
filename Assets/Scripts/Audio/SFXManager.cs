using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SFX
{
    Type,
    Walk
}
[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static AudioSource audioSource;
    public static AudioClip jump, run, type;
    // Start is called before the first frame update
    void Start()
    {
        jump = Resources.Load<AudioClip>("SFX/jump");
        run = Resources.Load<AudioClip>("SFX/mcwalk");
        type = Resources.Load<AudioClip>("SFX/typing4");
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    public static void PlaySound(SFX sFX)
    {
        switch (sFX)
        {

            case SFX.Walk:
                audioSource.PlayOneShot(run);
                break;
            case SFX.Type:
                audioSource.PlayOneShot(type);
                break;
            default:
                break;
        }
    }

}
