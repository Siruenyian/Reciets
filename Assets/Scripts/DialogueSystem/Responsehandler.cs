using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Responsehandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtontmp;


    [SerializeField] private RectTransform responseContainer;

    DialogueActivator dialogueActivator;
    private DialogueUI dialogueUI;
    private List<GameObject> tempResponseButoons = new List<GameObject>();

    private ResponseEvent[] responseEvents;

    private void Start()
    {
        responseButtontmp.gameObject.SetActive(false);
        dialogueUI = GetComponent<DialogueUI>();
        dialogueActivator = GetComponent<DialogueActivator>();
    }

    public void AddResponseEvent(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }
    public void ShowResponses(Response[] responses)
    {
        float responseBoxheight = 0;
        float spacing = responseContainer.GetComponent<VerticalLayoutGroup>().spacing;
        for (int i = 0; i < responses.Length; i++)
        {
            Response response = responses[i];
            int responseIndex = i;

            GameObject responseButton = Instantiate(responseButtontmp.gameObject, responseContainer);
            responseButton.gameObject.SetActive(true);
            tempResponseButoons.Add(responseButton);
            responseButton = responseButton.transform.GetChild(0).gameObject;
            responseButton.GetComponent<TMP_Text>().text = response.Responsetext;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickedResponse(response, responseIndex));

            responseBoxheight += responseButtontmp.sizeDelta.y + spacing;
            // responseBoxheight += spacing;
            // Debug.Log(responseButtontmp.sizeDelta.y + " " + spacing);
        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxheight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(Response response, int responseIndex)
    {
        responseBox.gameObject.SetActive(false);
        foreach (GameObject button in tempResponseButoons)
        {
            Destroy(button);

        }
        tempResponseButoons.Clear();
        /*Cek jika ada response event
         jika iya execute eventnya*/
        if (responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
            // return;
        }
        responseEvents = null;

        if (response.Dialogueobject)
        {
            dialogueUI.showDialogue(response.Dialogueobject, response.Dialogueobject.Dialoguepicleft, response.Dialogueobject.Dialoguepicright);
        }
        else
        {
            Debug.Log("closing dialogue");
            dialogueUI.CloseDialogue();
            // return;
        }
        //this little shit shit ruined 2 of my games
        // dialogueUI.showDialogue(response.Dialogueobject, response.Dialogueobject.Dialoguepicright, response.Dialogueobject.Dialoguepicleft);
    }
}
