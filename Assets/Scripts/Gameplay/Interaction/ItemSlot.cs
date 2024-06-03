using System.Collections;
using reciets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ItemDetail itemDetail = null;
    private bool isFilled = false;
    [HideInInspector] public Button button;
    private Sprite defaultSprite;
    public int stock = 0;
    public bool IsFilled { get => isFilled; set => isFilled = value; }
    private void Awake()
    {
        button = GetComponent<Button>();
        defaultSprite = button.image.sprite;
    }
    public void EnableInteraction()
    {

        button.interactable = true;
    }
    public void DisableInteraction()
    {
        button.interactable = false;
    }
    public void SetSlot(Sprite newSprite, ItemDetail newItemDetail)
    {
        itemDetail = newItemDetail;
        isFilled = true;
        if (button.image != null)
        {
            button.image.sprite = newSprite;
        }
    }
    public void ResetSlot()
    {
        if (button.image != null)
        {
            button.image.sprite = defaultSprite;
        }
        itemDetail = null;
        isFilled = false;
    }
    public ItemDetail TakeItem()
    {
        if (button.image != null)
        {
            button.image.sprite = defaultSprite;
        }
        button.interactable = true;
        isFilled = false;
        return itemDetail;
    }
    public ItemDetail TakeItemMultiple()
    {
        button.interactable = true;
        return itemDetail;
    }

    private float timeToWait = 2f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        // StopAllCoroutines();
        ShowMessage();
        // StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // StopAllCoroutines();
        HoverTipManager_Script.OnMouseLoseFocus();
    }

    private void ShowMessage()
    {
        // Debug.Log(Input.mousePosition);
        // Camera.main.ScreenToWorldPoint(Input.mousePosition)
        if (itemDetail != null)
        {
            HoverTipManager_Script.OnMouseHover($"{itemDetail.itemName:yea}\n{itemDetail.description:yea}", Input.mousePosition);
        }
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);
        ShowMessage();
    }


}