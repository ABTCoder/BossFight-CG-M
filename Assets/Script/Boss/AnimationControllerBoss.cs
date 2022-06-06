using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerBoss : MonoBehaviour
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
        if(charMove.y > 0)
        {
            animator.SetBool("isWalkingForward", true);
            Debug.Log("I'm running!");
        }
        if (charMove.y == 0 && charMove.x == 0)
        {
            animator.SetBool("isWalkingForward", false);
            animator.SetBool("isWalkingBackward", false);
            Debug.Log("I'm chilling!");
        }
        if (charMove.y < 0)
        {
            animator.SetBool("isWalkingBackward", true);
            Debug.Log("I'm running backward!");
        }

    }
}
