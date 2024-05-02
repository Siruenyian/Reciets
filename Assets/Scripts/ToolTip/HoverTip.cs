using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTip : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    public string tipToShow;
    private float timeToWait=0.5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        HoverTipManager_Script.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        Debug.Log(Input.mousePosition);

        HoverTipManager_Script.OnMouseHover(tipToShow, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }
}
