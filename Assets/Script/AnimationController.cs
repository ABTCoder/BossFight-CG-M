using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private MovementController movement;
    private CharacterMovement componentCharacterMovement;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        componentCharacterMovement = GetComponent<CharacterMovement>();
        movement = componentCharacterMovement.getMovement();

    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();

        // If player move isWalking to true
        if(charMove.y > 0 && charMove.x == 0)
        {
            animator.SetBool("isWalkingForward", true);
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            Debug.Log("I'm running!");
        }
        else if (charMove.y < 0 && charMove.x == 0)
        {
            animator.SetBool("isWalkingBackward", true);
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            Debug.Log("I'm running backward!");
        }
        else if(charMove.y == 0 && charMove.x > 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingRight", true);
            animator.SetBool("isWalkingLeft", false);
        }
        else if (charMove.y == 0 && charMove.x < 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingLeft", true);
            animator.SetBool("isWalkingRight", false);
        }
        else if (charMove.y < 0 && charMove.x < 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingBackward", true);
            animator.SetBool("isWalkingLeft", true);
        }
        else if (charMove.y < 0 && charMove.x > 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingBackward", true);
            animator.SetBool("isWalkingRight", true);
        }
        else if (charMove.y > 0 && charMove.x < 0)
        {
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingForward", true);
            animator.SetBool("isWalkingLeft", true);
        }
        else if (charMove.y > 0 && charMove.x > 0)
        {
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingLeft", false);
            animator.SetBool("isWalkingForward", true);
            animator.SetBool("isWalkingRight", true);
        }
        else if (charMove.y == 0 && charMove.x == 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBackward", false);
            animator.SetBool("isWalkingRight", false);
            animator.SetBool("isWalkingLeft", false);
            Debug.Log("I'm chilling!");
        }
        

    }
}
