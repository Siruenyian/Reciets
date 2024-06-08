using System.Collections.Generic;
using reciets;
using UnityEngine;

public class ExtraItemStorage : MonoBehaviour, Iinteractable
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();
    private CharacterInteraction characterI = null;
    private LayerMask layersToDisable;
    private Dictionary<int, bool> isIngredientAddedMap = new Dictionary<int, bool>();
    private Dictionary<int, ItemDetail> ingredients = new Dictionary<int, ItemDetail>();

    public CharacterInteraction CharacterI { get => characterI; set => characterI = value; }

    protected virtual void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            int index = i;
            slots[i].button.onClick.AddListener(() => ToggleIngredient(index));
        }
    }
    // Method to toggle ingredient based on button identifier
    public void ToggleIngredient(int buttonId)
    {
        if (isIngredientAddedMap.ContainsKey(buttonId) && isIngredientAddedMap[buttonId])
        {
            RemoveIngredient(buttonId);
        }
        else
        {
            AddIngredient(buttonId);
        }
    }

    private void AddIngredient(int buttonId)
    {
        // || isIngredientAddedMap[buttonId] == true
        if (characterI.GetInventory() == null)
        {
            return;
        }
        ItemDetail itemDetail = characterI.GetInventory();
        slots[buttonId].SetSlot(itemDetail.sprite, itemDetail);
        characterI.RemoveInventory();
    }

    private void RemoveIngredient(int buttonId)
    {
        if (ingredients.Count == 0 || characterI.GetInventory() != null)
        {
            return;
        }
        ItemDetail itemDetail = ingredients[buttonId];
        ingredients.Remove(buttonId);
        slots[buttonId].ResetSlot();
        characterI.SetInventory(itemDetail);
    }



    public void Interact(CharacterInteraction characterInteraction)
    {
        OpenOverlay();
    }

    public void OpenOverlay()
    {
        canvas.gameObject.SetActive(true);
        characterI.FreezeMovement();
        Camera.main.cullingMask &= ~layersToDisable;
    }

    public void CloseOverlay()
    {
        canvas.gameObject.SetActive(false);
        characterI.UnfreezeMovement();
        Camera.main.cullingMask |= layersToDisable;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
        {
            characterInteraction.Interactable = this;
            characterI = characterInteraction;

        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
        {
            characterInteraction.Interactable = null;
            characterI = null;
        }
    }
}