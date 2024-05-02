using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DialogueResponseEventEditor))]
public class DialogueResponseEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        DialogueResponseEvent responseEvent = (DialogueResponseEvent)target;

        if (GUILayout.Button("Refresh"))
        {
            responseEvent.OnValidate();
        }
    }
}
