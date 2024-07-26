using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCard : MonoBehaviour
{
    [SerializeField] private Bar scoreBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button playButton;
    private void Start()
    {
        scoreText.text = $"Points: 0/100";
    }
    public void SetScore(float score)
    {
        scoreBar.value = score;
        scoreText.text = $"Points: {score:0}/100";
    }
}