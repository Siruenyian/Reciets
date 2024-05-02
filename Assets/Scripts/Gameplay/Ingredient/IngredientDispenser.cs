using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class IngredientDispenser : MonoBehaviour, Iinteractable
    {
        // Start is called before the first frame update
        [SerializeField] private IngredientDetail ingredientDetail;
        [SerializeField] private int amount = 10;
        public void Interact(CharacterInteraction characterInteraction)
        {
            // pickup logic disini
            if (amount <= 0)
            {
                Debug.Log("abis bang");
                return;
            }
            if (characterInteraction.IsInventoryFull())
            {
                if (characterInteraction.GetInventory() == ingredientDetail)
                {
                    characterInteraction.RemoveInventory();
                    amount += 1;
                    return;
                }
                Debug.Log("tangan penuh, dimount dl ya");
                return;
            }



            amount -= 1;

            characterInteraction.SetInventory(ingredientDetail);
            // Debug.Log("pickup gt ya");
            // Debug.Log(ingredientDetail.ingredientTrait);
            // Debug.Log(ingredientDetail.value);
        }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider collision)
        {
            // Debug.Log("test");
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = this;
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = null;
            }
        }
    }
}
