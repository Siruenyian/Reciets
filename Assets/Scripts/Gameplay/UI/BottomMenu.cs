using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{
    [SerializeField] Image itemslotImage;
    Sprite originalSprite;

    private void Start()
    {
        originalSprite = itemslotImage.sprite;
    }
    public void SetItemSprite(Sprite itemSprite)
    {
        itemslotImage.sprite = itemSprite;
    }
    public void RemoveItemSprite()
    {
        itemslotImage.sprite = null;
    }
}