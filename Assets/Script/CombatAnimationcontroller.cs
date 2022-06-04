using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationcontroller : MonoBehaviour
{
    Animator playerAnimator;
    private int comboStep;
    private bool comboPossible;
    private MovementController normalAttack;
    private CharacterMovement componentCharacterMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponentInChildren<Animator>();

        componentCharacterMovement = GetComponent<CharacterMovement>();
        normalAttack = componentCharacterMovement.getMovement();
    }

    private void ComboPossible()
    {
        comboPossible = true;
    }

    private void NextAttack()
    {
        if (comboStep == 2)
            playerAnimator.Play("1H-RH@Attack04");
        else if (comboStep == 3)
            playerAnimator.Play("1H-RH@Attack03");
    }

    private void ResetCombo()
    {
        comboPossible = false;
        comboStep = 0;
    }

    private void NormalAttack() 
    {
        if (comboStep == 0)
        {
            playerAnimator.Play("1H - RH@Attack01");
            comboStep = 1;
            return;
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

    // Update is called once per frame
    void Update()
    {
        Vector2 attack = normalAttack.Main.Attack.ReadValue<Vector2>();
        
    }
}
