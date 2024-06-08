using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class SpecialiteManager : MonoBehaviour
    {
        // Start is called before the first frame update

        [SerializeField] private List<Specialite> specialites = new List<Specialite>();
        private void Awake()
        {
            // load from menu here
        }
        public void ActivateAbility(CharacterInteraction characterInteraction)
        {
            // check on hand
            ItemDetail itemonHand = characterInteraction.GetInventory();
            if (itemonHand is FoodDetail)
            {
                // activate specialite ability
                FoodDetail food = (FoodDetail)itemonHand;
                food.detailDict[IngredientTrait.TASTE] += specialites[0].detailDict[IngredientTrait.TASTE];
                food.detailDict[IngredientTrait.NUTRITION] += specialites[0].detailDict[IngredientTrait.NUTRITION];
                food.detailDict[IngredientTrait.POPULARITY] += specialites[0].detailDict[IngredientTrait.POPULARITY];
                Debug.Log(food.itemName + " taste" + food.detailDict[IngredientTrait.TASTE]
                    + " nutrition" + food.detailDict[IngredientTrait.NUTRITION]
                    + " popularity" + food.detailDict[IngredientTrait.POPULARITY]
                    );
                characterInteraction.SetInventory(food);
                return;
            }
            // unable to actiavte ability
            return;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
