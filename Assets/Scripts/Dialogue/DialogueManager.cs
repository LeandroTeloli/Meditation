using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    // public TextMeshProUGUI dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log(animator.GetBool("IsOpen")+ " Antes");
        animator.SetBool("IsOpen",true);
        Debug.Log(animator.GetBool("IsOpen")+ " Depois");

        titleText.text = dialogue.title;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() 
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        // dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        // animator.SetBool("IsOpen",false);
    }
}
