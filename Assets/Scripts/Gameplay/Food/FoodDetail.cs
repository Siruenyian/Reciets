using System;
using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

using UnityEngine;
using UnityEngine.Rendering;

namespace reciets
{
    [CreateAssetMenu(menuName = "Item/Food")]
    public class FoodDetail : ItemDetail
    {
        [SerializedDictionary("Trait", "Value")]
        public AYellowpaper.SerializedCollections.SerializedDictionary<IngredientTrait, int> detailDict;

        // public FoodDetail()
        // {
        //     detailDict.Add(IngredientTrait.TASTE, 0);
        //     detailDict.Add(IngredientTrait.NUTRITION, 0);
        //     detailDict.Add(IngredientTrait.POPULARITY, 0);

        // }

        public void AddTrait(int taste, int nutrition, int popularity)
        {
            detailDict[IngredientTrait.TASTE] += taste;
            detailDict[IngredientTrait.NUTRITION] += nutrition;
            detailDict[IngredientTrait.POPULARITY] += popularity;
        }
    }
}
