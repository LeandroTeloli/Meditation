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
        public bool IsInteraction;
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
        if (!dialogueLines[0].IsInteraction)
        {        
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        dialogueManager.DisplayNextSentence();
    }
    
}