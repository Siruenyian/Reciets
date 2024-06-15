using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace reciets
{
    public class CustomerInteraction : MonoBehaviour, Iinteractable
    {
        [SerializeField] public Customer customer;
        [SerializeField] private DialogueInteraction dialogueInteraction;
        [SerializeField] private DialogueObj brokeDialogue;
        [SerializeField] private DialogueObj accDialogue;
        [SerializeField] private DialogueObj rejectedDialogue;
        public bool isorderDone = false;
        private CharacterInteraction characterInteraction = null;

        public delegate void OrderDoneEventHandler(bool isSatisfied);
        public event OrderDoneEventHandler OrderDone;


        public void Interact(CharacterInteraction characterInteraction)
        {
            // talk and submit
            Debug.Log("Interacted with Customer!");
            dialogueInteraction.Interact(characterInteraction);

        }

        private bool subscribedToOrderDone = false;

        public bool IsSubscribedToOrderDone()
        {
            return subscribedToOrderDone;
        }

        public void SetSubscribedToOrderDone(bool subscribed)
        {
            subscribedToOrderDone = subscribed;
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

                isorderDone = false;
                characterInteraction.UnfreezeMovement();

                // OrderDone?.Invoke();
                return;
            }
            // change this bool to an int value so it can be passed trough
            bool isConfirmed = customer.Check(
            characterInteraction.GetInventory()
            );
            Debug.Log("Food is " + isConfirmed);
            if (isConfirmed)
            {
                characterInteraction.RemoveInventory();
                dialogueInteraction.dialogueObj.Responses[0].Dialogueobject = accDialogue;
                // isorderDone = true;
                StartCoroutine(AwaitDialogue(dialogueInteraction, true));

            }
            else
            {
                Debug.Log("is garabage");
                characterInteraction.RemoveInventory();
                dialogueInteraction.dialogueObj.Responses[0].Dialogueobject = rejectedDialogue;
                // isorderDone = false;
                StartCoroutine(AwaitDialogue(dialogueInteraction, false));

            }
        }
        private IEnumerator AwaitDialogue(DialogueInteraction dialogueInteraction, bool isSatisfied)
        {
            // unreliable soalnya dialogue buka tutup
            while (characterInteraction.DialogueUI.isOpen == true)
            {
                Debug.Log(dialogueInteraction.isDone);
                yield return null; // Wait until the condition is met
            }
            // Fire the event
            isorderDone = isSatisfied;
            OrderDone?.Invoke(isSatisfied);
        }
        // Start is called before the first frame update
        private void OnTriggerStay(Collider collision)
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