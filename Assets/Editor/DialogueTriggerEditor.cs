using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DialogueTrigger))]
public class DialogueTriggerEditor : Editor
{
    private SerializedProperty dialogueLinesProp;

    private void OnEnable()
    {
        dialogueLinesProp = serializedObject.FindProperty("dialogueLines");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw the default inspector for DialogueTrigger
        DrawDefaultInspector();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dialogue Lines");

        // Display a custom inspector for each dialogue line
        for (int i = 0; i < dialogueLinesProp.arraySize; i++)
        {
            SerializedProperty dialogueLineProp = dialogueLinesProp.GetArrayElementAtIndex(i);

            EditorGUILayout.LabelField("Dialogue Line " + (i + 1));

            EditorGUI.indentLevel++;
            SerializedProperty characterNameProp = dialogueLineProp.FindPropertyRelative("characterName");
            characterNameProp.stringValue = EditorGUILayout.TextField("Character Name", characterNameProp.stringValue);

            SerializedProperty sentenceProp = dialogueLineProp.FindPropertyRelative("sentence");
            sentenceProp.stringValue = EditorGUILayout.TextField("Sentence", sentenceProp.stringValue);
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        serializedObject.ApplyModifiedProperties();
    }
}