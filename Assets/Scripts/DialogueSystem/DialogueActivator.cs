using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using reciets;
/*Script untuk handle aktivasi dialog*/
public class DialogueActivator : MonoBehaviour, Iinteractable
{
    public NPC nPC;
    /*private SpriteRenderer npcSr;
    private SpriteRenderer bgSr;*/
    private DialogueObj dialogueObj { get; set; }

    public GameObject background;
    private void Start()
    {
        /*        bgSr = background.GetComponent<SpriteRenderer>();
                npcSr = GetComponent<SpriteRenderer>();*/
        dialogueObj = nPC.npcDialogue;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction player))
        {
            Debug.Log("collided!!");
            player.Interactable = this;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out CharacterInteraction player))
        {
            if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                Debug.Log("exit");

                player.Interactable = null;
            }

        }
    }

    public void UpdateDialogueObject(NPC nPC)
    {
        dialogueObj = nPC.npcDialogue;
        dialogueObj.Dialoguepicleft = nPC.npcDialogue.Dialoguepicleft;
        //sDebug.Log(dialogueObj, dialogueObj.Dialoguepic);
    }

    //function bdy dari interface
    public void Interact(CharacterInteraction player)
    {
        //Debug.Log("Interacted"+dialogueObj);

        if (player.DialogueUI.isOpen)
        {
            return;
        }
        if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueObj == dialogueObj)
        {
            player.DialogueUI.AddResponseEvents(responseEvent.Events);
        }

        player.DialogueUI.showDialogue(dialogueObj, dialogueObj.Dialoguepicleft, dialogueObj.Dialoguepicright);
    }
}
