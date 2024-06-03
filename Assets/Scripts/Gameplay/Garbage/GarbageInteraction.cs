using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class GarbageInteraction : MonoBehaviour, Iinteractable
    {

        public void Interact(CharacterInteraction characterInteraction)
        {
            Debug.Log("yah dibuang");
            characterInteraction.RemoveInventory();
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

