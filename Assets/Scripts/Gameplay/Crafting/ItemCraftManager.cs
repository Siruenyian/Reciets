using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class ItemCraftManager : MonoBehaviour
    {
        public static ItemCraftManager Instance { get; private set; }
        private bool isCrafting = false;
        [SerializeField] private List<ItemRecipe> recipes = new List<ItemRecipe>();
        [SerializeField] private List<ItemDetail> garbageResults = new List<ItemDetail>();

        public bool isDone = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            for (int i = 0; i < recipes.Count; i++)
            {
                recipes[i].abbrv += recipes[i].cookingMethod.ToString().ToLower();
                foreach (ItemDetail item in recipes[i].ingredient)
                {
                    // test this
                    recipes[i].abbrv += item.itemName;
                }
                recipes[i].abbrv.Trim();

            }
        }

        public ItemDetail CombineByName(string itemAbbrv, int taste, int nutrition, int popularity)
        {


            itemAbbrv = itemAbbrv.Trim();
            Debug.Log(itemAbbrv);
            // find and motify stats
            FoodDetail foodResult;
            for (int i = 0; i < recipes.Count; i++)
            {
                if (itemAbbrv == recipes[i].abbrv)
                {
                    if (recipes[i].result is FoodDetail)
                    {
                        foodResult = (FoodDetail)Instantiate(recipes[i].result);
                        // foodResult.AddTrait(taste, nutrition, popularity);
                        return foodResult;

                    }
                    return Instantiate(recipes[i].result);

                }
            }
            Debug.Log("ngak ketemu");
            return garbageResults[Random.Range(0, garbageResults.Count - 1)];
        }


    }
    public enum CookingMethod
    {
        Oven,
        Fry,
        Saute,
        Boil,
        Cut,
        Skin,
        Mix,
        Shake
    }
    [System.Serializable]
    public class ItemRecipe
    {
        public ItemDetail[] ingredient;
        public ItemDetail result;
        public CookingMethod cookingMethod;
        public int timetoProcess = 0;
        [HideInInspector] public string abbrv;
    }
}
