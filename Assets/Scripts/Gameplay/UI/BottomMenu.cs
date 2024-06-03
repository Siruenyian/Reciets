using reciets;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{
    [SerializeField] ItemSlot itemSlot;


    public void SetItemSprite(ItemDetail itemDetail)
    {
        itemSlot.SetSlot(itemDetail.sprite, itemDetail);
    }
    public void RemoveItemSprite()
    {
        itemSlot.ResetSlot();
    }
}