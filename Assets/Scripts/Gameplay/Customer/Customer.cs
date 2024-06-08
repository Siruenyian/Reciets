using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class Customer : MonoBehaviour
    {
        [Header("Customer Part")]
        [SerializeField] private FoodDetail desiredFood;
        [SerializeField] private int tasteModifier;
        [SerializeField] private int nutritionModifier;
        [SerializeField] private int popularityModifier;

        private void Awake()
        {
            Modifier();
        }

        public void Modifier()
        {
            desiredFood.detailDict[IngredientTrait.TASTE] += tasteModifier;
            desiredFood.detailDict[IngredientTrait.NUTRITION] += nutritionModifier;
            desiredFood.detailDict[IngredientTrait.POPULARITY] += popularityModifier;
        }

        public bool Compare(FoodDetail foodDetail1, FoodDetail foodDetail2)
        {
            Debug.Log(foodDetail1.detailDict[IngredientTrait.TASTE] + " " + foodDetail2.detailDict[IngredientTrait.TASTE]);
            Debug.Log(foodDetail1.detailDict[IngredientTrait.POPULARITY] + " " + foodDetail2.detailDict[IngredientTrait.POPULARITY]);
            Debug.Log(foodDetail1.detailDict[IngredientTrait.NUTRITION] + " " + foodDetail2.detailDict[IngredientTrait.NUTRITION]);
            if (
                foodDetail1.detailDict[IngredientTrait.TASTE] == foodDetail2.detailDict[IngredientTrait.TASTE]
            &&
                foodDetail1.detailDict[IngredientTrait.POPULARITY] == foodDetail2.detailDict[IngredientTrait.POPULARITY]
            &&
                foodDetail1.detailDict[IngredientTrait.NUTRITION] == foodDetail2.detailDict[IngredientTrait.NUTRITION]

            )
            {

                return true;
            }
            return false;

        }
        public bool Check(ItemDetail itemDetail)
        {
            if (itemDetail is FoodDetail)
            {
                FoodDetail food = (FoodDetail)itemDetail;
                Debug.Log(food.name + " " + desiredFood.name + " ");
                // somethin wong here
                Debug.Log("foodcomparison" + Compare(food, desiredFood));
                // if (Compare(food, desiredFood))
                // {
                //     Debug.Log("food is not desired food");
                //     return true;
                // }
                if (food.itemName == desiredFood.itemName)
                {
                    Debug.Log("accepted food " + food.itemName + "\n taste" + food.detailDict[IngredientTrait.TASTE]
                   + " nutrition" + food.detailDict[IngredientTrait.NUTRITION]
                   + " popularity" + food.detailDict[IngredientTrait.POPULARITY]
                   );
                    return true;
                }
                Debug.Log("food is not desired food");

                return false;
            }
            return false;
        }
    }
}
