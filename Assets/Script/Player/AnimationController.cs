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
    private Vector2 move = Vector2.zero;
    private float x = 0;
    private float y = 0;


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
        if (isRolling && !playerAnimator.IsInTransition(0))
        {
            playerAnimator.Play("RollBlend");
            //Debug.Log("FORCE FIX");
        }
        
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();
        if (playerInput.Main.ShieldBlock.IsPressed()) Block();
        if (!isAttacking && !isRolling && !isBlocking)
        {
            if (!playerAnimator.IsInTransition(0))
                playerAnimator.CrossFade("Movement Blend Tree", 0.2f);
            move = Vector2.Lerp(move, charMove, Time.deltaTime*8f);
            

            playerAnimator.SetFloat("X", move.x);
            playerAnimator.SetFloat("Y", move.y);
        }

    }

    public bool AnimatorIsPlaying()
    {
        return playerAnimator.GetCurrentAnimatorStateInfo(0).length >
               playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    private void EventCombatAddListners()
    {
        // Reads the input of the player and performs different combat actions due the input
        playerInput.Main.BaseAttack.performed += NormalAttack;
        playerInput.Main.ShieldBlock.canceled += StopBlocking;
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
            playerAnimator.CrossFade("PlayerAttack02", 0.2f);
        }
        else if (comboStep == 3)
        {
            playerAnimator.CrossFade("PlayerAttack03", 0.2f);
        }
    }

    private void ResetCombo()
    {
        isAttacking = false;
        comboPossible = false;
        comboStep = 0;
    }

    public void NormalAttack(InputAction.CallbackContext ctx)
    {
        if (!isBlocking && !isRolling)
        {
            isAttacking = true;
            if (comboStep == 0)
            {
                playerAnimator.CrossFade("PlayerAttack01", 0.2f);
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

    public void Block()
    {
        if (!isAttacking && !isRolling && !isBlocking)
        {
            isBlocking = true;
            playerAnimator.CrossFade("ShieldBlock01_Loop", 0.075f);
        }
    }

    public void StopBlocking(InputAction.CallbackContext ctx)
    {

        isBlocking = false;
    }

    public void DodgeRoll(InputAction.CallbackContext ctx)
    {
        Vector2 charMove = playerInput.Main.Move.ReadValue<Vector2>();

        if (!isRolling && !isAttacking && !isBlocking && (charMove != Vector2.zero))
        {
            playerAnimator.SetFloat("X", charMove.x);
            playerAnimator.SetFloat("Y", charMove.y);
            playerAnimator.CrossFade("RollBlend", 0.2f);
            componentCharacterMovement.setStartRollPos();
            isRolling = true;
        }

    }

    public void ResetRoll()
    {
        isRolling = false;
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
