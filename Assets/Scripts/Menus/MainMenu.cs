using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas levelmenuCanvas;
    [SerializeField] private Canvas settingsmenuCanvas;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        SoundManager.Instance.Play(audioClip);
        SoundManager.Instance.SetVolume(0.5f);
    }
    public void OpenLevelmenu()
    {
        levelmenuCanvas.gameObject.SetActive(true);
    }
    public void CloseLevelMenu()
    {
        levelmenuCanvas.gameObject.SetActive(false);
    }
    public void OpenSettingsMenu()
    {
        settingsmenuCanvas.gameObject.SetActive(true);
    }
    public void CloseSettingsMenu()
    {
        settingsmenuCanvas.gameObject.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}