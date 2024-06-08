using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class CollideTip : MonoBehaviour
{
    public string tipToShow;
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    [SerializeField] private GameObject tipSignifier;

    private void OnTriggerStay(Collider other)
    {
        ShowTip(gameObject.name);
    }
    private void OnTriggerExit(Collider other)
    {
        HideTip();
    }

    void Start()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
        tipSignifier.SetActive(false);

    }
    private void ShowTip(string tip)
    {
        tipText.text = tip;
        // 129, 41
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth + 20, tipWindow.sizeDelta.y);
        tipWindow.gameObject.SetActive(true);
        tipSignifier.SetActive(true);
    }
    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
        tipSignifier.SetActive(false);
    }
}
