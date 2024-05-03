using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class CustomerInteraction : MonoBehaviour, Iinteractable
    {
        [SerializeField] Customer customer;
        [SerializeField] private DialogueInteraction dialogueInteraction;

        public void Interact(CharacterInteraction characterInteraction)
        {
            // talk and submit
            Debug.Log("Interacted with Customer!");

            if (characterInteraction.GetInventory() == null)
            {
                Debug.Log("too bad bro");
                return;
            }
            bool isConfirmed = customer.Check(
            characterInteraction.GetInventory()
            );
            Debug.Log("Food is " + isConfirmed);
            if (isConfirmed)
            {
                characterInteraction.RemoveInventory();
                dialogueInteraction.Interact(characterInteraction);
            }
            else
            {
                Debug.Log("is garabage");
            }
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
