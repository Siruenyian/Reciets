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
        [SerializeField] private DialogueObj brokeDialogue;
        [SerializeField] private DialogueObj accDialogue;
        [SerializeField] private DialogueObj rejectedDialogue;
        private CharacterInteraction characterInteraction = null;
        public void Interact(CharacterInteraction characterInteraction)
        {
            // talk and submit
            Debug.Log("Interacted with Customer!");
            dialogueInteraction.Interact(characterInteraction);

        }

        public void TalktoCustomer()
        {
            if (characterInteraction.GetInventory() == null)
            {
                //trigger to another dialoge
                Debug.Log("too bad bro");
                // set dialogueobj
                // THIS, THIS IS BAD, SO BAD I DON'T EVEN WANNA LOOK AT IT
                // BUT IT WORKS??? HOW IN THE ----
                dialogueInteraction.dialogueObj.Responses[0].Dialogueobject = brokeDialogue;
                // customerMovement.MoveTo(new Vector3(10, 1, 0), () =>
                // {
                //     Debug.Log("NYAAHAHAHAHAHAH");
                // });
                // dialogueResponseEvent.DialogueObj.Responses[0].Dialogueobject = brokeDialogue;
                // dialogueResponseEvent.DialogueObj.dialogueObj = brokeDialogue;
                // characterInteraction.DialogueUI.showDialogue(brokeDialogue, brokeDialogue.Dialoguepicleft, brokeDialogue.Dialoguepicright);
                // fire dialogue again
                // dialogueInteraction.Interact(characterInteraction);
                // characterInteraction.DialogueUI.CloseDialogue();
                return;
            }
            bool isConfirmed = customer.Check(
            characterInteraction.GetInventory()
            );
            Debug.Log("Food is " + isConfirmed);
            if (isConfirmed)
            {
                characterInteraction.RemoveInventory();
                dialogueInteraction.dialogueObj.Responses[0].Dialogueobject = accDialogue;

            }
            else
            {
                Debug.Log("is garabage");
                dialogueInteraction.dialogueObj.Responses[0].Dialogueobject = rejectedDialogue;
            }
        }
        // Start is called before the first frame update
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = this;
                this.characterInteraction = characterInteraction;
            }
        }
        private void OnTriggerExit(Collider collision)
        {
            if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction characterInteraction))
            {
                characterInteraction.Interactable = null;
                this.characterInteraction = null;
            }
        }
    }
}
