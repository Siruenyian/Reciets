using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class CraftingActivator : MonoBehaviour, Iinteractable
    {
        [SerializeField] private CraftingManager craftingManager;
        [SerializeField] private Bar craftingBar;
        public void Interact(CharacterInteraction characterInteraction)
        {
            if (characterInteraction.GetInventory() != null)
            {
                return;
            }
            // check if there mats are there
            craftingManager.GetResult(characterInteraction);
            craftingBar.Reset();
        }

        // Start is called before the first frame update
        private void OnTriggerStay(Collider collision)
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

