using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class CraftingInteraction : MonoBehaviour, Iinteractable
    {
        [SerializeField] private CraftingManager craftingManager;
        [SerializeField] private Bar craftingBar;
        private void Awake()
        {
            craftingBar.maxValue = craftingManager.maxItem;
        }
        public void Interact(CharacterInteraction characterInteraction)
        {
            Debug.Log("crafting");
            if (characterInteraction.GetInventory() == null)
            {
                return;
            }
            craftingManager.AddMaterial(characterInteraction.GetInventory());
            characterInteraction.RemoveInventory();

            craftingBar.Increase(1.0f);
            // craftingManager.GetResult(characterInteraction);
        }

        // Start is called before the first frame update
        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("test");
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

