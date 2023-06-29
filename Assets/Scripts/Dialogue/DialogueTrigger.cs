using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [System.Serializable]
    public struct DialogueLine
    {
        public string characterName;
        public string sentence;
    }

    public DialogueLine[] dialogueLines;

    private void OnTriggerEnter2D(Collider2D other)
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogueLines);
        Destroy(gameObject);
    }
}