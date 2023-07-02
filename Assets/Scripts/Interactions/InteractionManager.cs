using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public bool isInteractionActive;
    private TextMeshProUGUI InteractionText;
    public GameObject interactionBox;
    public GameObject interactionReferenceObject;

    public Canvas canvas;

    private Animator interactionBoxAnimator;
    private Animator interactionButtonAnimator;


    // Start is called before the first frame update
    void Start()
    {
        InteractionText = interactionBox.GetComponentInChildren<TextMeshProUGUI>();

        GameObject paperBox = interactionBox.transform.Find("PaperBox")?.gameObject;
        interactionBoxAnimator =  paperBox.GetComponent<Animator>();

        GameObject paperImage = paperBox.transform.Find("PaperImage")?.gameObject;
        interactionButtonAnimator = paperImage.GetComponentInChildren<Animator>();
    }

    public void ShowInteractionBox (string interaction, GameObject objectReference) 
    {
        isInteractionActive = true;
        InteractionText.alignment = TextAlignmentOptions.Center;
        InteractionText.text = interaction;
        interactionReferenceObject = objectReference;

        CalcModalPosition.CalculateModalPosition(interactionReferenceObject, canvas, interactionBox, new Vector2(0,40));

        interactionBoxAnimator.SetBool("IsOpen", true);
        interactionButtonAnimator.SetBool("IsEnabled", true);
    }

    public void HideInteractionBox()
    {
        isInteractionActive = false;
        interactionBoxAnimator.SetBool("IsOpen", false);
        interactionButtonAnimator.SetBool("IsEnabled", false);
    }
}
