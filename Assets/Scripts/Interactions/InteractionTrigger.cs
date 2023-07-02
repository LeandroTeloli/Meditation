using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public enum Interactions
    {
        Meditate,
        Option2
    }

    [SerializeField]
    public Interactions interactionOptions;

    private InteractionManager interactionManager;


    private void Start()
    {
        interactionManager = FindObjectOfType<InteractionManager>();
    }

    public void OnTriggerEnter2D (Collider2D other) 
    {
        interactionManager.ShowInteractionBox(interactionOptions.ToString(), gameObject);
    }

    public void OnTriggerExit2D (Collider2D other) 
    {
        interactionManager.HideInteractionBox();
    }
}