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

    private void OnTriggerEnter2D(Collider2D other)
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogueLines);
        // Destroy(gameObject);
    }
}