using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using reciets;
using UnityEngine;
using UnityEngine.UI;

public class CraftingBase : MonoBehaviour
{
    // [SerializeField] private Bar craftingBar;


    private CharacterInteraction characterI = null;

    [SerializeField] private Sprite defaultslotSprite;
    [SerializeField] private List<Button> slots = new List<Button>();
    [SerializeField] private Button resultSlot;
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
            Debug.Log("" + index);
            slots[i].onClick.AddListener(() => ToggleIngredient(index));
            // nihahah[i].isFilled = false;
        }
        resultSlot.onClick.AddListener(TakeResult);
        resultSlot.interactable = false;

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
        Debug.Log("mixer got item! " + itemDetail.name);
        ingredients[buttonId] = itemDetail;
        Debug.Log("this button is " + slots[buttonId].gameObject.name);
        slots[buttonId].image.sprite = itemDetail.sprite;
        characterI.RemoveInventory();
        isIngredientAddedMap[buttonId] = true;

    }

    private void RemoveIngredient(int buttonId)
    {
        if (ingredients.Count == 0 || characterI.GetInventory() != null)
        {
            return;
        }
        // sussy
        ItemDetail itemDetail = ingredients[buttonId];
        ingredients.Remove(buttonId);
        slots[buttonId].image.sprite = defaultslotSprite;
        // do stuff
        characterI.SetInventory(itemDetail);
        isIngredientAddedMap[buttonId] = false;
    }
    public void TakeResult()
    {
        if (characterI.GetInventory() != null)
        {
            return;
        }
        ItemDetail itemDetail = ingredients[-1];
        characterI.SetInventory(itemDetail);
        resultSlot.gameObject.GetComponent<Image>().sprite = null;
        resultSlot.interactable = false;
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
            slots[i].interactable = false;
        }
    }

    public void EnableSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].interactable = true;
        }
    }
    public virtual void Process(int method)
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
        foreach (var ingredient in ingredients)
        {
            // Debug.Log(ingredient.Key + " " + ingredient.Value);
            // -1 is reserved lmao
            if (ingredient.Value != null && ingredient.Key != -1)
            {
                if (ingredient.Value is IngredientDetail)
                {
                    result += ingredient.Value.itemName;
                    Debug.Log(result);
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
            slots[i].image.sprite = defaultslotSprite;
        }
        // pause/timer or something
        resultSlot.interactable = true;
        resultSlot.gameObject.GetComponent<Image>().sprite = item.sprite;
        // characterI.SetInventory(item);
        ingredients[-1] = item;
    }
}
public enum CookingMethod
{
    StoveOven, Mixer, CuttingStation
}