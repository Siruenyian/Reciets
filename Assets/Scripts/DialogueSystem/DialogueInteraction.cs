using reciets;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    public NPC nPC;

    public DialogueObj dialogueObj { get; set; }
    public bool isDone;
    private void Start()
    {
        /*        bgSr = background.GetComponent<SpriteRenderer>();
                npcSr = GetComponent<SpriteRenderer>();*/
        dialogueObj = nPC.npcDialogue;

    }
    public void Interact(CharacterInteraction player)
    {
        //Debug.Log("Interacted"+dialogueObj);
        if (player.DialogueUI.isOpen)
        {
            return;
        }
        isDone = false;
        if (TryGetComponent(out DialogueResponseEvent responseEvent) && responseEvent.DialogueObj == dialogueObj)
        {
            player.DialogueUI.AddResponseEvents(responseEvent.Events);
        }

        player.DialogueUI.showDialogue(dialogueObj, dialogueObj.Dialoguepicleft, dialogueObj.Dialoguepicright);
        isDone = true;

    }


}