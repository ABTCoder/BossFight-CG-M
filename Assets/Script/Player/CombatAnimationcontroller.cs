using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatAnimationcontroller : MonoBehaviour
{
    Animator playerAnimator;
    private int comboStep = 0;
    private bool comboPossible;
    private MovementController attackController;
    private CharacterMovement attackComponent;
    private bool isAttacking = false;
    private bool isBlocking = false;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        attackComponent = GetComponentInParent<CharacterMovement>();
        attackController = attackComponent.getMovement();

        attackController.Main.BaseAttack.performed += NormalAttack;
        attackController.Main.ShieldBlock.performed += Block;
    }

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
        playerAnimator.SetBool("isAttacking", false);
        comboPossible = false;
        comboStep = 0;
    }

    public void NormalAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("BaseAttack1");
        Debug.Log(comboStep);
        isAttacking = true;
        playerAnimator.SetBool("isAttacking", true);
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

    public void Block(InputAction.CallbackContext ctx)
    {

        isBlocking = true;
        playerAnimator.SetBool("isBlocking", true);
        attackController.Main.ShieldBlock.canceled += StopBlocking;

    }

    public void StopBlocking(InputAction.CallbackContext ctx) 
    {
        
        isBlocking = false;
        playerAnimator.SetBool("isBlocking", false);
    }

    public bool getIsBlocking()
    {
        return isBlocking;
    }

    public bool getIsAttacking() 
    {
        return isAttacking;
    }
}
