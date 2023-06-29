// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// [System.Serializable]
// public class Dialogue
// {
//     // public string title;
//     [TextArea(2,5)]
//     public string[] sentences;
// }

using UnityEngine;
using UnityEditor;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string characterName;
        public string sentence;
    }

    [SerializeField]
    public DialogueLine[] dialogueLines;
}