using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeditate : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogBox;
    private Animator animator;
    private bool isReadyToMeditate = false;

    [SerializeField]
    PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToMeditate)
        {   
            if (Input.GetButtonDown("Interact") && playerController.isOnFloor())
            {
                animator.SetBool("IsMeditating", !animator.GetBool("IsMeditating"));   
                animator.SetBool("IsWalking", !animator.GetBool("IsMeditating"));   

                if (animator.GetBool("IsMeditating"))
                {
                    dialogBox.GetComponent<Animator>().SetBool("IsOpen", false);
                }
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      isReadyToMeditate = true;
      GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
      isReadyToMeditate = false;
      dialogBox.GetComponent<Animator>().SetBool("IsOpen", false);
    }
    
}
