using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine
    {
        public GameObject Character;
        public string sentence;
        public float letterWaitingTime;
    }

    public DialogueLine[] dialogueLines;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        dialogueManager.StartDialogue(dialogueLines);
        Destroy(gameObject);
    }
}