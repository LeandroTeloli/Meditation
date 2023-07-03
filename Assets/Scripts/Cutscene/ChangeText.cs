using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{
    [SerializeField]
    public string[] Sentences;

    public void ShowNextSentence()
    {
        TextMeshProUGUI cutsceneText = gameObject.GetComponent<TextMeshProUGUI>();
        string currentText = cutsceneText.text;

        if (currentText == "")
        {
            cutsceneText.text = Sentences[0];
        } 
        else
        {
            foreach (string sentence in Sentences)
            {
                if (sentence == currentText)
                {
                    int currentIndex = System.Array.IndexOf(Sentences, sentence);

                    // Verifica se há um próximo texto disponível na lista de Sentences
                    if (currentIndex < Sentences.Length - 1)
                    {
                        string nextText = Sentences[currentIndex + 1];
                        cutsceneText.text = nextText;
                    }
                    break;
                }
            }
        }
    }
}
