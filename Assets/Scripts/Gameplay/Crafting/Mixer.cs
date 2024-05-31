using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using reciets;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour, Iinteractable
{
    // [SerializeField] private Bar craftingBar;

    [SerializeField] private Canvas canvas;
    [SerializeField] private CinemachineVirtualCamera transitionCam = null;

    private LayerMask layersToDisable;
    private CharacterInteraction characterI = null;

    [SerializeField] private Sprite defaultslotSprite;
    [SerializeField] private List<Image> slots = new List<Image>();
    [SerializeField] private Button resultSlot;
    [SerializeField] private CookingMethod cookingMethod;
    // [SerializeField] private List<ItemDetail> ingredients = new List<ItemDetail>();
    private Dictionary<int, bool> isIngredientAddedMap = new Dictionary<int, bool>();
    // 
    private Dictionary<int, ItemDetail> ingredients = new Dictionary<int, ItemDetail>();
    private void Start()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            isIngredientAddedMap[i] = false;
            // nihahah[i].isFilled = false;
        }
        resultSlot.interactable = false;
        layersToDisable = 1 << LayerMask.NameToLayer("Player");
        canvas.gameObject.SetActive(false);
        transitionCam.gameObject.SetActive(false);
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
        transitionCam.gameObject.SetActive(true);
    }

    public void CloseOverlay()
    {
        canvas.gameObject.SetActive(false);
        characterI.UnfreezeMovement();
        Camera.main.cullingMask |= layersToDisable;
        transitionCam.gameObject.SetActive(false);
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
        slots[buttonId].sprite = itemDetail.sprite;
        characterI.RemoveInventory();
        isIngredientAddedMap[buttonId] = true;


        // ItemDetail itemDetail = characterI.GetInventory();
        // nihahah[buttonId].itemDetail = itemDetail;
        // Debug.Log("this button is " + slots[buttonId].gameObject.name);
        // slots[buttonId].sprite = itemDetail.sprite;
        // characterI.RemoveInventory();
        // nihahah[buttonId].isFilled = true;

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
        slots[buttonId].sprite = defaultslotSprite;
        // do stuff
        characterI.SetInventory(itemDetail);
        isIngredientAddedMap[buttonId] = false;


        // ItemDetail itemDetail = nihahah[buttonId].itemDetail;
        // nihahah[buttonId].itemDetail = null;
        // slots[buttonId].sprite = defaultslotSprite;
        // characterI.SetInventory(itemDetail);
        // nihahah[buttonId].isFilled = true;
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

    public virtual void Process(int method)
    {
        // get the result by sending string

        if (characterI.GetInventory() != null)
        {
            return;
        }

        string result;
        // switch (method)
        // {
        //     case 0:
        //         result = "mix";
        //         break;
        //     case 1:
        //         result = "shake";

        //         break;
        //     default:
        //         result = "";
        //         break;
        // }
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
            slots[i].sprite = defaultslotSprite;
        }
        // pause/timer or something
        resultSlot.interactable = true;
        resultSlot.gameObject.GetComponent<Image>().sprite = item.sprite;
        // characterI.SetInventory(item);
        ingredients[-1] = item;
    }
    private IEnumerator WaitProcess()
    {
        yield return null;
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
public enum CookingMethod
{
    StoveOven, Mixer, CuttingStation
}