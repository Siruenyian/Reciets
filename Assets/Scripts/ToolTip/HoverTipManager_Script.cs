using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class HoverTipManager_Script : MonoBehaviour
{
    public TextMeshProUGUI tipText;
    public RectTransform tipWindow;
    [SerializeField][Range(0.0f, 10.0f)] private float offsetX = 1f;
    [SerializeField][Range(0.0f, 10.0f)] private float offsetY = 1f;
    [SerializeField] private float padding = 20f;
    // Start is called before the first frame update
    public static Action<string, Vector2> OnMouseHover;

    public static Action OnMouseLoseFocus;
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }
    void Start()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth + padding, tipText.preferredHeight + padding);
        tipWindow.gameObject.SetActive(true);
        Debug.Log(mousePos.x + " " + mousePos.y);
        tipWindow.transform.position = new Vector2(mousePos.x + offsetX, mousePos.y + offsetY);
        //
    }
    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
