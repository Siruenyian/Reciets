using System.Collections;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace reciets
{
    [CreateAssetMenu(menuName = "Specialite/Base")]
    public class Specialite : ScriptableObject
    {
        // Start is called before the first frame update
        public string SPName;
        public Sprite sprite;
        public int skillCooldown;
        [SerializeField][TextArea] public string description;
        [SerializedDictionary("Trait", "Value")]
        public AYellowpaper.SerializedCollections.SerializedDictionary<IngredientTrait, int> detailDict;
        public void ExtraAbility(MonoBehaviour coroutineRunner)
        {
            Debug.Log("extraaaaa");
            CountDownTimer.Instance.AddTime(300);
            coroutineRunner.StartCoroutine(ExtraAbilityCoroutine(3f));
        }
        private IEnumerator ExtraAbilityCoroutine(float duration)
        {
            // Set time scale to 0 to stop time
            Time.timeScale = 0.5f;

            // Wait for the specified duration in real-time seconds
            yield return new WaitForSecondsRealtime(duration);

            // Restore time scale to 1 to resume normal time
            Time.timeScale = 1;
        }
        public void Boost(CharacterInteraction characterInteraction)
        {
            // check on hand
            ItemDetail itemonHand = characterInteraction.GetInventory();
            if (itemonHand is FoodDetail)
            {
                // activate specialite ability
                FoodDetail food = (FoodDetail)itemonHand;
                food.detailDict[IngredientTrait.TASTE] += detailDict[IngredientTrait.TASTE];
                food.detailDict[IngredientTrait.NUTRITION] += detailDict[IngredientTrait.NUTRITION];
                food.detailDict[IngredientTrait.POPULARITY] += detailDict[IngredientTrait.POPULARITY];
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
    }
}
