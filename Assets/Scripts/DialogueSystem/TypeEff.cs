using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypeEff : MonoBehaviour
{
    [SerializeField] private float typespeed = 50f;
    public bool isRunning { get; private set; }


    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>(){'.','!','?' }, 0.6f),
        new Punctuation(new HashSet<char>(){',',':',';' }, 0.3f )


    };

    private Coroutine typingCoroutine;
    //driver func
    public void Run(string texttotype, TMP_Text textlabel)
    {
        typingCoroutine = StartCoroutine(routine: TypeText(texttotype, textlabel));
    }

    public void stop()
    {
        if (isRunning)
        {
            StopCoroutine(typingCoroutine);
            isRunning = false;
        }

    }


    private IEnumerator TypeText(string texttotype, TMP_Text textlabel)
    {
        isRunning = true;
        textlabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;
        while (charIndex < texttotype.Length)
        {
            int lastCharIndex = charIndex;


            t += Time.deltaTime * typespeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, texttotype.Length);
            //typing in the text


            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool islast = i >= texttotype.Length - 1;

                textlabel.text = texttotype.Substring(0, i + 1);
                // SFXManager.PlaySound(SFX.Type);
                SFXManager.audioSource.PlayOneShot(SFXManager.type, Mathf.Clamp(textlabel.text[i], 0, 1));
                yield return new WaitForSeconds(0.05f);
                if (IsPunctuation(texttotype[i], out float waitTime) && !islast && !IsPunctuation(texttotype[i + 1], out _))
                {

                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null;
        }
        isRunning = false;


    }
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuaionCategory in punctuations)
        {
            if (punctuaionCategory.Punctuations.Contains(character))
            {
                waitTime = punctuaionCategory.waitTime;
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float waitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            this.waitTime = waitTime;
        }
    }
}
