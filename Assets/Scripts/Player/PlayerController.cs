using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private DialogueManager dialogueManager;

    public float speed;
    public float jumpingPower;
    public float horizontal;
    private Transform groundCheck;
    private LayerMask groundLayer;

    private bool isGrounded;
    private bool isWalking;
    private bool IsWalkingEnabled;
    private bool IsMeditating;

    private void Awake()
    {
        groundCheck = transform.GetChild(0);
        groundLayer = LayerMask.GetMask("Floor");

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        HandleInput();
        // HandleJump();
        HandleDialogueInteraction();

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

    private void HandleDialogueInteraction () 
    {
        if (dialogueManager.isDialogueActive)
        {
            IsWalkingEnabled = false;
            if (Input.GetButtonDown("Interact"))
            {
                dialogueManager.DisplayNextSentence();
            }    
        } 
        else
        {
            IsWalkingEnabled = true;
        }
    }
}