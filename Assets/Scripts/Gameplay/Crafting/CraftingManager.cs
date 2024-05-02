using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class CraftingManager : MonoBehaviour
    {
        private bool isCrafting = false;
        [SerializeField] private List<ItemDetail> ingredients = new List<ItemDetail>();
        [SerializeField] private List<CraftingRecipe> recipes = new List<CraftingRecipe>();
        [SerializeField] private List<FoodDetail> garbageResults = new List<FoodDetail>();
        [SerializeField] private int maxIngredient = 3;
        [SerializeField] public int maxItem = 5;
        public bool isDone = false;

        private void Awake()
        {
            for (int i = 0; i < recipes.Count; i++)
            {
                foreach (ItemDetail item in recipes[i].ingredient)
                {

                    recipes[i].abbrv += item.itemName;
                }
                recipes[i].abbrv.Trim();

                Debug.Log(recipes[i].abbrv);
            }
            // if slow sort first
        }
        public void AddMaterial(ItemDetail ingredientDetail)
        {
            if (ingredients.Count >= maxItem)
            {
                return;
            }
            ingredients.Add(ingredientDetail);
            isDone = false;
        }
        public void GetResult(CharacterInteraction characterInteraction)
        {
            // if (ingredients.Count >= maxIngredient)
            // {
            if (ingredients.Count <= 0)
            {
                return;
            }
            FoodDetail foodDetail = CombineByName();
            characterInteraction.SetInventory(foodDetail);
            ingredients.Clear();
            isDone = true;
            return;
            // }
        }
        public FoodDetail CombineByName()
        {
            // I think we should add a ssystem where extra mats will add up to the taste
            // like adding more rice would add up the portion ad stuff
            string result = "";
            int taste = 0;
            int nutrition = 0;
            int popularity = 0;
            // crafting logic here
            // 0 index blyad
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients != null)
                {
                    if (i < maxIngredient)
                    {
                        result += ingredients[i].itemName;
                        continue;
                    }
                    if (ingredients[i] is IngredientDetail)
                    {
                        IngredientDetail a = (IngredientDetail)ingredients[i];
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

            result.Trim();
            Debug.Log(result);
            // find and motify stats
            FoodDetail foodResult;
            for (int i = 0; i < recipes.Count; i++)
            {
                if (result == recipes[i].abbrv)
                {
                    // Debug.Log("ketemu");
                    // malah keadd
                    foodResult = Instantiate(recipes[i].result);
                    foodResult.AddTrait(taste, nutrition, popularity);
                    Debug.Log(foodResult.itemName + " taste" + foodResult.detailDict[IngredientTrait.TASTE]
                    + " nutrition" + foodResult.detailDict[IngredientTrait.NUTRITION]
                    + " popularity" + foodResult.detailDict[IngredientTrait.POPULARITY]
                    );
                    return foodResult;
                }
            }
            Debug.Log("ngak ketemu");

            // bool contains = Regex.IsMatch("Hello1 Hello", @"(^|\s)Hello(\s|$)");
            // randomize garbage I guess
            return garbageResults[0];
        }
        public FoodDetail CombineByStat()
        {
            int taste = 0;
            int nutrition = 0;
            int popularity = 0;
            // deny if not fooditem
            // crafting logic here
            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients != null)
                {
                    if (ingredients[i] is not IngredientDetail)
                    {
                        ingredients.Clear();
                        popularity -= 1;
                        continue;
                        // return garbageResults[0];
                    }
                    IngredientDetail a = (IngredientDetail)ingredients[i];
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

                }
            }
            Debug.Log(taste + " " + nutrition + " " + popularity);
            // find and motify stats
            for (int i = 0; i < recipes.Count; i++)
            {
                // match the thing
                if (
                    recipes[i].result.detailDict[IngredientTrait.TASTE] == taste &&
                    recipes[i].result.detailDict[IngredientTrait.POPULARITY] == popularity &&
                    recipes[i].result.detailDict[IngredientTrait.NUTRITION] == nutrition
                )
                {
                    return recipes[i].result;
                }
            }
            Debug.Log("ngak ketemu");

            // bool contains = Regex.IsMatch("Hello1 Hello", @"(^|\s)Hello(\s|$)");
            return garbageResults[0];
        }

    }
    [System.Serializable]
    public class CraftingRecipe
    {
        public ItemDetail[] ingredient;
        public FoodDetail result;
        public string abbrv;
        // CraftingRecipe()
        // {
        //     foreach (ItemDetail item in ingredient)
        //     {
        //         abbrv += item.name;
        //         Debug.Log("a");
        //     }
        // }
    }
}
