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
            if (Input.GetButtonDown("Interact"))
            {
                animator.SetBool("IsMeditating", !animator.GetBool("IsMeditating"));   
                animator.SetBool("IsWalking", !animator.GetBool("IsMeditating"));   

                dialogBox.GetComponent<Animator>().SetBool("IsOpen", !animator.GetBool("IsMeditating"));
            }
            
        }
    }
    
}
