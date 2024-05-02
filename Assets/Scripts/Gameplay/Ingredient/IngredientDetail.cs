using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    [CreateAssetMenu(menuName = "Item/Ingredient")]
    public class IngredientDetail : ItemDetail
    {
        public IngredientTrait ingredientTrait = IngredientTrait.NUTRITION;
        // public Sprite sprite;
        public int value = 0;
        // [SerializeField][TextArea] public string description;
    }
}
