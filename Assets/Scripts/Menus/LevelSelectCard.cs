using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectCard : MonoBehaviour
{
    [SerializeField] private Bar scoreBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button playButton;

    public void SetScore(float score)
    {
        scoreBar.value = score;
        scoreText.text = $"Score: {score:00}";
    }
}