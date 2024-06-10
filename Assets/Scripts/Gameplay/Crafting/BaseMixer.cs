using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using reciets;
using UnityEngine;
using UnityEngine.UI;

public class BaseMixer : MonoBehaviour
{
    // [SerializeField] private Bar craftingBar;


    private CharacterInteraction characterI = null;

    [SerializeField] private List<ItemSlot> slots = new List<ItemSlot>();
    [SerializeField] private List<ItemSlot> resultSlots = new List<ItemSlot>();
    [SerializeField] private CookingMethod cookingMethod;
    private Dictionary<int, bool> isIngredientAddedMap = new Dictionary<int, bool>();
    // 
    private Dictionary<int, ItemDetail> ingredients = new Dictionary<int, ItemDetail>();

    public CharacterInteraction CharacterI { get => characterI; set => characterI = value; }

    protected virtual void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            isIngredientAddedMap[i] = false;
            int index = i;
            // Debug.Log("index: " + index);
            slots[i].button.onClick.AddListener(() => ToggleIngredient(index));
            // nihahah[i].isFilled = false;
        }
        for (int i = 0; i < resultSlots.Count; i++)
        {
            int index = i;
            resultSlots[i].button.onClick.AddListener(() => TakeResult(index));
            resultSlots[i].DisableInteraction();
        }

    }
    protected bool IsAnyIngredientAdded()
    {
        return isIngredientAddedMap.Values.Any(value => value);
    }
    protected ItemDetail getIngredientinSlot(int slot)
    {
        return ingredients[slot];
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
        ingredients[buttonId] = itemDetail;
        slots[buttonId].SetSlot(itemDetail.sprite, itemDetail);
        characterI.RemoveInventory();
        isIngredientAddedMap[buttonId] = true;

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
        isIngredientAddedMap[buttonId] = false;
    }
    public void TakeResult(int buttonId)
    {
        if (characterI.GetInventory() != null)
        {
            return;
        }


        characterI.SetInventory(resultSlots[buttonId].TakeItem());
        resultSlots[buttonId].ResetSlot();
        resultSlots[buttonId].DisableInteraction();

    }

    private string Parse(CookingMethod cookingMethod, int method)
    {
        string result;
        switch (cookingMethod)
        {
            case CookingMethod.StoveOven:
                switch (method)
                {
                    case 0:
                        result = "oven";
                        break;
                    case 1:
                        result = "fry";

                        break;
                    case 2:
                        result = "saute";

                        break;
                    case 3:
                        result = "boil";
                        break;
                    default:
                        result = "";
                        break;
                }
                break;
            case CookingMethod.Mixer:
                switch (method)
                {
                    case 0:
                        result = "mix";
                        break;
                    case 1:
                        result = "shake";

                        break;
                    default:
                        result = "";
                        break;
                }
                break;
            case CookingMethod.CuttingStation:
                switch (method)
                {
                    case 0:
                        result = "skin";
                        break;
                    case 1:
                        result = "cut";
                        break;
                    default:
                        result = "";
                        break;
                }
                break;
            default:
                result = "";
                break;
        }
        return result;
    }
    public void DisableSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].DisableInteraction();
        }
    }

    public void EnableSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].EnableInteraction();

        }
    }
    protected virtual void Process(int method)
    {
        // get the result by sending string

        if (characterI.GetInventory() != null)
        {
            return;
        }

        string result;

        result = Parse(cookingMethod, method);

        int taste = 0;
        int nutrition = 0;
        int popularity = 0;
        // crafting logic here
        // 0 index blyad
        var ingredient2 = ingredients.OrderBy(x => x.Key);
        foreach (var ingredient in ingredient2)
        {
            // Debug.Log(ingredient.Key + " " + ingredient.Value);
            // -1 is reserved lmao
            if (ingredient.Value != null && ingredient.Key != -1)
            {
                if (ingredient.Value is IngredientDetail)
                {
                    result += ingredient.Value.itemName;
                    Debug.Log(ingredient.Key);
                    IngredientDetail a = (IngredientDetail)ingredient.Value;
                    switch (a.ingredientTrait)
                    {
                        case IngredientTrait.TASTE:
                            taste += a.value;
                            break;
                        case IngredientTrait.NUTRITION:
                            nutrition += a.value;
                            break;
                        case IngredientTrait.POPULARITY:
                            popularity += a.value;
                            break;
                        default:
                            break;
                    }
                    // return garbageResults[0];
                }
            }
        }
        Debug.Log("sending string to manager: " + result);
        ItemDetail item = ItemCraftManager.Instance.CombineByName(result, taste, nutrition, popularity);
        // Debug.Log();

        ingredients.Clear();
        for (int i = 0; i < slots.Count; i++)
        {
            isIngredientAddedMap[i] = false;
            slots[i].ResetSlot();
        }
        for (int i = 0; i < resultSlots.Count; i++)
        {
            resultSlots[i].SetSlot(item.sprite, item);
            resultSlots[i].EnableInteraction();

            // Debug.Log("is enabled?: " + resultSlots[i].button.enabled);
        }
        // ingredients[-1] = item;
    }
}
