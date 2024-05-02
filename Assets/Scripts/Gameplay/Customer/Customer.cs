using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private FoodDetail desiredFood;
        [SerializeField] private int taste;
        [SerializeField] private int nutrition;
        [SerializeField] private int popularity;
        private void Awake()
        {
            Modifier();
        }
        public void Modifier()
        {
            desiredFood.detailDict[IngredientTrait.TASTE] += taste;
            desiredFood.detailDict[IngredientTrait.NUTRITION] += nutrition;
            desiredFood.detailDict[IngredientTrait.POPULARITY] += popularity;
        }
        public void Show()
        {
            // show speech bubble
        }
        public bool Check(ItemDetail itemDetail)
        {
            if (itemDetail is FoodDetail)
            {
                FoodDetail food = (FoodDetail)itemDetail;
                if (food != desiredFood)
                {
                    return false;
                }
                Debug.Log("accepted food " + food.itemName + "\n taste" + food.detailDict[IngredientTrait.TASTE]
                    + " nutrition" + food.detailDict[IngredientTrait.NUTRITION]
                    + " popularity" + food.detailDict[IngredientTrait.POPULARITY]
                    );
                return true;
            }
            return false;
        }
    }
}
