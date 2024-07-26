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
        PlayerData loadedData = SaveSystem.LoadPlayer();
        foreach (var entry in loadedData.LevelsAndScores)
        {
            int level = entry.Key;
            float score = entry.Value;
            Debug.Log($"Level {level}: Score {score}");
        }
        if (loadedData == null)
        {
            PlayerData newplayer = new PlayerData();
            for (int i = 1; i <= levelSelectCards.Count; i++)
            {
                newplayer.SetScore(i, 0);
            }
            SaveSystem.SavePlayer(newplayer);
        }
        loadingText.text = "Loading";
        for (int i = 1; i <= levelSelectCards.Count; i++)
        {
            levelSelectCards[i - 1].SetScore(loadedData.GetScore(i));
            // levelSelectCards[i].SetScore(
            // PlayerPrefs.GetFloat("score")
            // );
        }
    }
    public void Savelevel()
    {
        // error gtw
        PlayerData loadedData = SaveSystem.LoadPlayer();
        for (int i = 1; i <= levelSelectCards.Count; i++)
        {
            loadedData.SetScore(i, 50);
            levelSelectCards[i - 1].SetScore(loadedData.GetScore(i));
        }
        SaveSystem.SavePlayer(loadedData);
    }
    public void ResetSave()
    {
        PlayerData newplayer = new PlayerData();
        for (int i = 1; i <= levelSelectCards.Count; i++)
        {
            newplayer.SetScore(i, 0);
            levelSelectCards[i - 1].SetScore(0);

        }
        SaveSystem.SavePlayer(newplayer);
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