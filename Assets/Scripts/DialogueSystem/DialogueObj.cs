using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObj : ScriptableObject
{
    [SerializeField] public Sprite Dialoguepicleft;
    [SerializeField] public Sprite Dialoguepicright;
    [SerializeField] public Sprite background;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    public bool isEnd;
    public bool isLeft;
    public bool isRight;


    public string[] Dialogue => dialogue;

    public bool hasresponses => Responses != null && Responses.Length > 0;
    //getter utk responses
    /*Public array of responses yang ngepoint ke responses yg di serialized*/
    public Response[] Responses => responses;
    //continue if no response
    [Header("Continued to")]
    [SerializeField] public DialogueObj dialogueObj;
}
