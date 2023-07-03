using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private DialogueManager dialogueManager;
    private InteractionManager interactionManager;
    private PlayableDirector meditationTimeline;

    public float speed;
    public float jumpingPower;
    public float horizontal;
    private Transform groundCheck;
    private LayerMask groundLayer;

    private bool isGrounded;
    private bool isWalking;
    private bool IsWalkingEnabled;
    private bool IsMeditating;
    private bool CantToggleMeditation;

    private void Awake()
    {
        groundCheck = transform.GetChild(0);
        groundLayer = LayerMask.GetMask("Floor");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        interactionManager = FindObjectOfType<InteractionManager>();
        meditationTimeline = FindObjectOfType<PlayableDirector>();
    }

    private void Update()
    {
        HandleInput();
        // HandleJump();
        HandleDialogue();
        HandleInteraction();

        animator.SetBool("IsWalking", isWalking);
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxisRaw("Walk");

        //Walk
        if (horizontal != 0f && IsWalkingEnabled)
        {
            // Player is walking
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            isWalking = true;
        }
        else
        {
            // Player is idle
            rb.velocity = new (0f, rb.velocity.y);   
            isWalking = false;

        }
        
        //Flip
        if (isWalking)
        {
            if (horizontal > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (horizontal < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

    }

    private void HandleJump () 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
        }
    }

    private void HandleDialogue () 
    {
        // Dialogue estará ativo quando o usuário entrar em algum ponto demarcado com script "DialogueTrigger"
        if (dialogueManager.isDialogueActive)
        {
            IsWalkingEnabled = false;

            if (Input.GetButtonDown("Interact") && dialogueManager.canInteract)
            {                
                dialogueManager.DisplayNextSentence();
            }  
        } 
        else if (!IsMeditating)
        {
            IsWalkingEnabled = true;
        }
    }

    private void HandleInteraction ()
    {
        if (Input.GetButtonDown("Interact") && (interactionManager.isInteractionActive || IsMeditating))
        {      
            IsMeditating = !IsMeditating;         

            if (IsMeditating) 
            {
                IsWalkingEnabled = false;
                animator.SetBool("IsMeditating", true);
                interactionManager.HideInteractionBox(); 
                transform.localScale = new Vector3(1f, 1f, 1f);

                meditationTimeline.time = 0f;
                meditationTimeline.Play();          
            }
            else if (!CantToggleMeditation)
            {
                IsWalkingEnabled = true;
                animator.SetBool("IsMeditating", false);  
                interactionManager.ShowInteractionBox("Meditate", interactionManager.interactionReferenceObject);    
                meditationTimeline.Pause();        
            }
        }  
    }

    public void DisableStopMeditating ()
    {
        CantToggleMeditation = true;
    }
}