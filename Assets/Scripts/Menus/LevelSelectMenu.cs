using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    [SerializeField] private Canvas loadingScreen;
    [SerializeField] private TextMeshProUGUI loadingText;
    [SerializeField] private List<LevelSelectCard> levelSelectCards = new List<LevelSelectCard>();
    private void Start()
    {
        loadingText.text = "Loading";
        for (int i = 0; i < levelSelectCards.Count; i++)
        {
            levelSelectCards[i].SetScore(
            PlayerPrefs.GetFloat("score")
            );
        }
    }
    public void PlayLevel(int sceneId)
    {
        // GameManager.Instance.LoadScene("GameScene" + level);
        StartCoroutine(loadasync(sceneId));
    }
    IEnumerator loadasync(int sceneid)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneid);
        loadingScreen.gameObject.SetActive(true);
        while (!loading.isDone)
        {
            float loadvalue = Mathf.Clamp01(loading.progress / 0.9f);
            loadingText.text += ".";
            // 125*8";
            //Debug.Log("Sudah load " + loadvalue);
            yield return null;
        }
    }
}