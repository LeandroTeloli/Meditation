using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public bool isDialogueActive;
    public bool canInteract;
    private Animator dialogueBoxAnimator;
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
        
        GameObject paperBox = dialogueBox.transform.Find("PaperBox")?.gameObject;
        dialogueBoxAnimator =  paperBox.GetComponent<Animator>();

        GameObject paperImage = paperBox.transform.Find("PaperImage")?.gameObject;
        interactionButtonAnimator = paperImage.GetComponentInChildren<Animator>();
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
            dialogueBoxAnimator.SetBool("IsOpen",false);
            StopAllCoroutines();
            DialogueText.text = " ";
            return;
        }
        
        GameObject character = listOfSentenceCharacters.Dequeue();

        OpenDialogue(character);

        string sentence = listOfSentences.Dequeue();

        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence, listOfLetterWaitingTimes.Dequeue(), character));            
    }

    public void OpenDialogue (GameObject character) 
    {   

        if (currentCharacter != character)
        {
            CalcModalPosition.CalculateModalPosition(character, canvas, dialogueBox, new Vector2(0,23));
        }

        dialogueBoxAnimator.SetBool("IsOpen",true);            
        currentCharacter = character;
    }

    IEnumerator TypeSentence (string sentence, float letterWaitingTime, GameObject character)
    {
        interactionButtonAnimator.SetBool("IsEnabled",false);     
        canInteract = true;      
        
        //Verifica se é a primeira frase do diálogo
        if (DialogueText.text == " ")
        {            
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
        canInteract = true;      

    }
}
