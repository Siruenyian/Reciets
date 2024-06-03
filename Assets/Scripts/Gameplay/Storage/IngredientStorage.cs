using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using reciets;
using UnityEngine;
using UnityEngine.UI;

public class IngredientStorage : MonoBehaviour, Iinteractable
{
    [SerializeField] private Canvas canvas;
    private CharacterInteraction characterI = null;
    private List<ItemSlot> slots = new List<ItemSlot>();
    [SerializeField] private List<IngredientDetail> ingredients = new List<IngredientDetail>();
    public CharacterInteraction CharacterI { get => characterI; set => characterI = value; }
    private LayerMask layersToDisable;
    private void Start()
    {
        layersToDisable = 1 << LayerMask.NameToLayer("Player");
        Debug.Log(slots.Count);
        slots = GetComponentsInChildren<ItemSlot>().ToList();
        canvas.gameObject.SetActive(false);
        for (int i = 0; i < ingredients.Count; i++)
        {
            int index = i;
            slots[i].button.onClick.AddListener(() => RemoveIngredient(index));
            slots[i].stock = 10;

            slots[i].SetSlot(ingredients[i].sprite, ingredients[i]);
        }
    }

    private void RemoveIngredient(int buttonId)
    {
        if (slots.Count == 0 || characterI.GetInventory() != null)
        {
            return;
        }
        // if stock is 0
        // slots[buttonId].ResetSlot();
        if (slots[buttonId].stock <= 0)
        {
            slots[buttonId].DisableInteraction();
            return;
        }
        characterI.SetInventory(slots[buttonId].TakeItemMultiple());
        slots[buttonId].stock -= 1;
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
