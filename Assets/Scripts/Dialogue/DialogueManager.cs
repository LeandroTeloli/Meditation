using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public bool isDialogueActive;
    private Animator dialogueAnimator;
    private Animator interactionButtonAnimator;
    private Queue<string> listOfSentences;
    private Queue<GameObject> listOfSentenceCharacters;
    private Queue<float> listOfLetterWaitingTimes;

    public Canvas canvas;
    public GameObject dialogueBox;
    private GameObject currentCharacter;
    private TextMeshProUGUI DialogueText;
    
    void Start()
    {
        listOfSentences = new Queue<string>();
        listOfSentenceCharacters = new Queue<GameObject>();
        listOfLetterWaitingTimes = new Queue<float>();
        DialogueText = dialogueBox.GetComponentInChildren<TextMeshProUGUI>();
        
        GameObject paperBox = GameObject.Find("PaperBox");
        dialogueAnimator =  paperBox.GetComponent<Animator>();

        GameObject interactionButton = GameObject.Find("InteractionButton");
        interactionButtonAnimator = interactionButton.GetComponent<Animator>();
    }


    public void StartDialogue(DialogueTrigger.DialogueLine []DialogueLine)
    {        
        isDialogueActive = true;
        DialogueText.alignment = TextAlignmentOptions.Left;
        listOfSentences.Clear();
        listOfSentenceCharacters.Clear();

        foreach (DialogueTrigger.DialogueLine dialogueLine in DialogueLine)
        {
            listOfSentences.Enqueue(dialogueLine.sentence);
            listOfSentenceCharacters.Enqueue(dialogueLine.Character);
            listOfLetterWaitingTimes.Enqueue(dialogueLine.letterWaitingTime);
        }

        DisplayNextSentence();
    }
    public void DisplayNextSentence() 
    {
        if (listOfSentences.Count == 0)
        {
            isDialogueActive = false;
            interactionButtonAnimator.SetBool("IsEnabled",false);            
            dialogueAnimator.SetBool("IsOpen",false);
            StopAllCoroutines();
            DialogueText.text = " ";
            return;
        }
        
        GameObject character = listOfSentenceCharacters.Dequeue();

        OpenDialogue(character);

        string sentence = listOfSentences.Dequeue();
        Debug.Log(sentence);

        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, listOfLetterWaitingTimes.Dequeue(), character));
    }

    public void OpenDialogue (GameObject character) 
    {   

        if (currentCharacter != character)
        {
            CalculateDialogueBoxPosition(character);
        }

        dialogueAnimator.SetBool("IsOpen",true);            
        currentCharacter = character;
    }

    IEnumerator TypeSentence (string sentence, float letterWaitingTime, GameObject character)
    {
        interactionButtonAnimator.SetBool("IsEnabled",false);           
        
        //Verifica se é a primeira frase do diálogo
        if (DialogueText.text == " ")
        {            
            Debug.Log("Primeira frase");
            //Adiciona um tempo até a abertura do dialogbox para mostrar o texto
            yield return new WaitForSeconds(0.8f);
        }

        DialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (letter == '.')
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(letterWaitingTime + 0.3f);
            } 
            else if (letter == ',')
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(letterWaitingTime + 0.15f);
            } 
            else
            {
                DialogueText.text += letter;
                yield return new WaitForSeconds(letterWaitingTime);
            }
        }

        interactionButtonAnimator.SetBool("IsEnabled",true);           

    }

    private void CalculateDialogueBoxPosition (GameObject character) 
    {
        Vector3 characterWorldPosition = character.transform.position;
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(characterWorldPosition) + new Vector3(0,140,0);

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
