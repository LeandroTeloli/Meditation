using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public bool isDialogueActive;
    public Animator animator;
    private Queue<string> listOfSentences;

    // Start is called before the first frame update
    void Start()
    {
        listOfSentences = new Queue<string>();
    }

    public void StartDialogue(DialogueTrigger.DialogueLine []DialogueLine)
    {
        animator.SetBool("IsOpen",true);

        isDialogueActive = true;

        listOfSentences.Clear();

        foreach (DialogueTrigger.DialogueLine dialogueLine in DialogueLine)
        {
            listOfSentences.Enqueue(dialogueLine.sentence);
        }

        StartCoroutine(WaitTextAppears());
    }

    IEnumerator WaitTextAppears()
    {
        yield return new WaitForSeconds(0.5f);
        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (listOfSentences.Count == 0)
        {
            isDialogueActive = false;
            animator.SetBool("IsOpen",false);
            return;
        }

        string sentence = listOfSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        TextMeshProUGUI Dialogue = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        foreach (char letter in sentence.ToCharArray())
        {
            Dialogue.text += letter;
            yield return new WaitForSeconds(0.04f);
        }
    }
}
