using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.5f;
    public float jumpingPower = 3f;
    private bool isFacingRight = true;
    private Transform floorCheck;
    private LayerMask floorLayer;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        floorCheck = transform.GetChild(0);
        floorLayer = LayerMask.GetMask("Floor");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("IsWalking", false);

    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        bool isOnFloor = IsOnFloor();


        //Jump

        if (Input.GetButtonDown("Jump") && isOnFloor)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }


        //Walk

        if (horizontal != 0f)
        {
            // Player is walking
            animator.SetBool("IsWalking", true);  
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            // Player is idle
            animator.SetBool("IsWalking", false);

            if (isOnFloor) {
                rb.velocity = new (0f, rb.velocity.y);
            }
        }

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Flip();
        }

    }

    private bool IsOnFloor()
    {
        return Physics2D.OverlapCircle(floorCheck.position, 0.2f, floorLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}