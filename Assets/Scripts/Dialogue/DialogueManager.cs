using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public bool isDialogueActive;
    public Animator animator;
    private Queue<string> listOfSentences;
    private Queue<GameObject> listOfSentenceCharacters;

    public Canvas canvas;
    public GameObject dialogueBox;
    private GameObject currentCharacter;
    
    void Start()
    {
        listOfSentences = new Queue<string>();
        listOfSentenceCharacters = new Queue<GameObject>();
    }


    public void StartDialogue(DialogueTrigger.DialogueLine []DialogueLine)
    {
        isDialogueActive = true;
        listOfSentences.Clear();
        listOfSentenceCharacters.Clear();

        foreach (DialogueTrigger.DialogueLine dialogueLine in DialogueLine)
        {
            listOfSentences.Enqueue(dialogueLine.sentence);
            listOfSentenceCharacters.Enqueue(dialogueLine.Character);
        }

        StartCoroutine(WaitTextAppears());
        DisplayNextSentence();

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
            Debug.Log("fechou");
            isDialogueActive = false;
            animator.SetBool("IsOpen",false);
            return;
        }
        
        OpenDialogue(listOfSentenceCharacters.Dequeue());

        string sentence = listOfSentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void OpenDialogue (GameObject character) 
    {   
        if (currentCharacter != character)
        {            
            CalculateDialogueBoxPosition(character);
        }

        animator.SetBool("IsOpen",true);            
        currentCharacter = character;
    }

    IEnumerator TypeSentence (string sentence)
    {
        TextMeshProUGUI Dialogue = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        Dialogue.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            Dialogue.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void CalculateDialogueBoxPosition (GameObject character) 
    {
        Vector3 characterWorldPosition = character.transform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(characterWorldPosition) + new Vector3(0,220,0);

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        RectTransform dialogueBoxRectTransform = dialogueBox.GetComponent<RectTransform>();

        Vector2 canvasBounds = new Vector2(canvasRectTransform.rect.width, canvasRectTransform.rect.height);
        Vector2 dialogueBoxSize = dialogueBoxRectTransform.rect.size;

        // Calculate the position relative to the canvas
        Vector2 canvasDialoguePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, canvas.worldCamera, out canvasDialoguePosition);

        // Set the dialogue box position based on the canvasDialoguePosition
        dialogueBoxRectTransform.anchoredPosition = canvasDialoguePosition;

        // Check if the dialogue box exceeds the canvas bounds
        Vector2 dialoguePosition = dialogueBoxRectTransform.anchoredPosition;
        float leftBound = -canvasBounds.x * 0.5f + dialogueBoxSize.x * 0.5f;
        float rightBound = canvasBounds.x * 0.5f - dialogueBoxSize.x * 0.5f;
        float bottomBound = -canvasBounds.y * 0.5f + dialogueBoxSize.y * 0.5f;
        float topBound = canvasBounds.y * 0.5f - dialogueBoxSize.y * 0.5f;

        if (dialoguePosition.x < leftBound)
            dialoguePosition.x = leftBound;
        else if (dialoguePosition.x > rightBound)
            dialoguePosition.x = rightBound;

        if (dialoguePosition.y < bottomBound)
            dialoguePosition.y = bottomBound;
        else if (dialoguePosition.y > topBound)
            dialoguePosition.y = topBound;

        // Set the final dialogue box position within the canvas bounds
        dialogueBoxRectTransform.anchoredPosition = dialoguePosition;
    }
}
