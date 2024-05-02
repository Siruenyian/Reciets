using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class IngredientSpawner : MonoBehaviour, Iinteractable
    {
        public void Interact(CharacterInteraction characterInteraction)
        {
            Debug.Log("spawn");
            //spawn pickupable here
            // Instantiate()
        }

        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = this;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = null;
            }
        }
    }
}
