using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator playerAnimator;
    private MovementController movement;
    private CharacterMovement componentCharacterMovement;
    private CombatAnimationcontroller combatController;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        combatController = GetComponent<CombatAnimationcontroller>();
        componentCharacterMovement = GetComponentInParent<CharacterMovement>();
        movement = componentCharacterMovement.getMovement();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 charMove = movement.Main.Move.ReadValue<Vector2>();
        if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()) && !(combatController.getIsRolling()))
        {
            // If player move isWalking to true
            if (charMove.y > 0 && charMove.x == 0)
            {
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);

                //Debug.Log("I'm running!");
            }
            else if (charMove.y < 0 && charMove.x == 0)
            {
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                //Debug.Log("I'm running backward!");
            }
            else if (charMove.y == 0 && charMove.x > 0)
            {
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", true);
                playerAnimator.SetBool("isWalkingLeft", false);
            }
            else if (charMove.y == 0 && charMove.x < 0)
            {
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingLeft", true);
                playerAnimator.SetBool("isWalkingRight", false);
            }
            else if (charMove.y < 0 && charMove.x < 0)
            {
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingLeft", true);
            }
            else if (charMove.y < 0 && charMove.x > 0)
            {
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingRight", true);
            }
            else if (charMove.y > 0 && charMove.x < 0)
            {
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingLeft", true);
            }
            else if (charMove.y > 0 && charMove.x > 0)
            {
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingRight", true);
            }
            else if (charMove.y == 0 && charMove.x == 0)
            {
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                //Debug.Log("I'm chilling!");
            }
        }
        else if (!(combatController.getIsAttacking()) && !(combatController.getIsBlocking()) && combatController.getIsRolling() && !(combatController.getIsSliding()))
        {
            playerAnimator.SetBool("isWalkingForward", false);
            playerAnimator.SetBool("isWalkingBackward", false);
            playerAnimator.SetBool("isWalkingRight", false);
            playerAnimator.SetBool("isWalkingLeft", false);
            combatController.setTrueIsSliding();

            if (charMove.y > 0 && charMove.x == 0)
            {
                playerAnimator.Play("RollForward");
            }
            else if (charMove.y < 0 && charMove.x == 0)
            {
                playerAnimator.Play("RollBackward");
            }
            else if (charMove.y == 0 && charMove.x > 0)
            {
                playerAnimator.Play("RollRight");
            }
            else if (charMove.y == 0 && charMove.x < 0)
            {
                playerAnimator.Play("RollLeft");
            }
            else if (charMove.y < 0 && charMove.x < 0)
            {
                playerAnimator.Play("RollBackwardLeft");
            }
            else if (charMove.y < 0 && charMove.x > 0)
            {
                playerAnimator.Play("RollBackwardRight");
            }
            else if (charMove.y > 0 && charMove.x < 0)
            {
                playerAnimator.Play("RollForwardLeft");
            }
            else if (charMove.y > 0 && charMove.x > 0)
            {
                playerAnimator.Play("RollForwardRight");
            }
            else if (charMove.y == 0 && charMove.x == 0)
            {
                playerAnimator.Play("RollBackward");
            }
        }

    }
}
