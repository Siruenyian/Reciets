using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace reciets
{
    public class CustomerInteraction : MonoBehaviour, Iinteractable
    {
        [SerializeField] Customer customer;
        public void Interact(CharacterInteraction characterInteraction)
        {
            // talk and submit
            customer.Check(
            characterInteraction.GetInventory()
            );
            characterInteraction.RemoveInventory();
        }

        // Start is called before the first frame update
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("test");
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
