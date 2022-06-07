using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour
{
    private Animator playerAnimator;
    private MovementController playerInput;
    private CharacterMovement componentCharacterMovement;

    private int comboStep = 0;

    private bool comboPossible = false; // It determinates the possibility to concatenate the attacks
    private bool isAttacking = false;
    private bool isBlocking = false;
    private bool isRolling = false;
    private bool isSliding = false; // It prevents the interruption of the roll's animation


    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        componentCharacterMovement = GetComponentInParent<CharacterMovement>();
        playerInput = componentCharacterMovement.getMovement();

        EventCombatAddListners();
    }


    private void FixedUpdate()
    {
        MovementAnimationHandler();
    }

    private void MovementAnimationHandler()
    {
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();
        if (!(getIsAttacking()) && !(getIsBlocking()) && !(getIsRolling()))
        {
            if (charMove.y > 0 && charMove.x == 0)
            {
                // Move forward
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);
            }
            else if (charMove.y < 0 && charMove.x == 0)
            {
                // Move backward
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);
            }
            else if (charMove.y == 0 && charMove.x > 0)
            {
                // Move Right
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", true);
                playerAnimator.SetBool("isWalkingLeft", false);
            }
            else if (charMove.y == 0 && charMove.x < 0)
            {
                // Move Left
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingLeft", true);
                playerAnimator.SetBool("isWalkingRight", false);
            }
            else if (charMove.y < 0 && charMove.x < 0)
            {
                // Move Backward Left
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingLeft", true);
            }
            else if (charMove.y < 0 && charMove.x > 0)
            {
                // Move Backward Right
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                playerAnimator.SetBool("isWalkingBackward", true);
                playerAnimator.SetBool("isWalkingRight", true);
            }
            else if (charMove.y > 0 && charMove.x < 0)
            {
                // Move Forward Left
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingLeft", true);
            }
            else if (charMove.y > 0 && charMove.x > 0)
            {
                // Move Forward Right
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingLeft", false);
                playerAnimator.SetBool("isWalkingForward", true);
                playerAnimator.SetBool("isWalkingRight", true);
            }
            else if (charMove.y == 0 && charMove.x == 0)
            {
                // Idle
                playerAnimator.SetBool("isWalkingForward", false);
                playerAnimator.SetBool("isWalkingBackward", false);
                playerAnimator.SetBool("isWalkingRight", false);
                playerAnimator.SetBool("isWalkingLeft", false);
            }
        }
        else if (!(getIsAttacking()) && !(getIsBlocking()) && getIsRolling() && !(getIsSliding()))
        {
            playerAnimator.SetBool("isWalkingForward", false);
            playerAnimator.SetBool("isWalkingBackward", false);
            playerAnimator.SetBool("isWalkingRight", false);
            playerAnimator.SetBool("isWalkingLeft", false);
            setTrueIsSliding(); // This method set a boolean that prevents the interruption of the roll's animation

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


    private void EventCombatAddListners()
    {
        // Reads the input of the player and performs different combat actions due the input
        playerInput.Main.BaseAttack.performed += NormalAttack;
        playerInput.Main.ShieldBlock.performed += Block;
        playerInput.Main.DodgeRoll.performed += DodgeRoll;
    }


    #region Handler's methods
    private void ComboPossible()
    {
        comboPossible = true;
    }

    private void NextAttack()
    {
        if (comboStep == 2)
        {
            Debug.Log("BaseAttack2");
            playerAnimator.Play("PlayerAttack02");
        }
        else if (comboStep == 3)
        {
            Debug.Log("BaseAttack3");
            playerAnimator.Play("PlayerAttack03");
        }
    }

    private void ResetCombo()
    {
        isAttacking = false;
        playerAnimator.SetBool("isDoingAction", false);
        comboPossible = false;
        comboStep = 0;
    }

    public void NormalAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("BaseAttack1");
        Debug.Log(comboStep);
        isAttacking = true;
        playerAnimator.SetBool("isDoingAction", true);
        if (!isBlocking && !isRolling)
        {
            if (comboStep == 0)
            {
                playerAnimator.Play("PlayerAttack01");
                comboStep = 1;
            }
            else
            {
                if (comboPossible)
                {
                    comboPossible = false; //Evita di duplicare l'input
                    comboStep += 1;
                }
            }
        }
    }

    public void Block(InputAction.CallbackContext ctx)
    {

        isBlocking = true;
        playerAnimator.SetBool("isBlocking", true);
        playerInput.Main.ShieldBlock.canceled += StopBlocking;

    }

    public void StopBlocking(InputAction.CallbackContext ctx)
    {

        isBlocking = false;
        playerAnimator.SetBool("isBlocking", false);
    }

    public void DodgeRoll(InputAction.CallbackContext ctx)
    {
        if (!isRolling)
        {
            playerAnimator.SetBool("isDoingAction", true);
            componentCharacterMovement.setStartRollPos();
            isRolling = true;
        }

    }

    private void ResetRoll()
    {
        isRolling = false;
        isSliding = false;
        playerAnimator.SetBool("isDoingAction", false);
    }

    public void setTrueIsSliding()
    {
        isSliding = true;
    }

    public bool getIsSliding()
    {
        return isSliding;
    }

    public bool getIsRolling()
    {
        return isRolling;
    }

    public bool getIsBlocking()
    {
        return isBlocking;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }

    #endregion
}
