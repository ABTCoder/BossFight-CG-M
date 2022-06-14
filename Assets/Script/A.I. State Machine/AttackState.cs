using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public CombatStanceState combatStanceState;
    public PursueTargetState pursueTargetState;
    public EnemyAttackAction currentAttack;

    [SerializeField] private ColliderAttack[] enemyColliders;

    public bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;
    
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        RotateTowardsTargetWhilstAttacking(enemyManager);

        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            return pursueTargetState;
        }

        //ATTACK WITH COMBO!
        if (willDoComboOnNextAttack && enemyManager.canDoCombo)
        {
            enemyAnimatorManager.anim.SetBool("isAttacking", true);
            AttackTargetWithCombo(enemyAnimatorManager, enemyManager);
            for (int i =0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].resetCollided();
            }
        }

        //ATTACK!
        //Roll for a combo chance
        if (!hasPerformedAttack)
        {
            enemyAnimatorManager.anim.SetBool("isAttacking", true);
            AttackTarget(enemyAnimatorManager, enemyManager);
            RollForComboChance(enemyManager);
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                enemyColliders[i].resetCollided();
            }
        }

        if (willDoComboOnNextAttack && hasPerformedAttack)
        {
            return this; //Goes back up to preform the combo
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
    {
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
    {
        willDoComboOnNextAttack = false;
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if (enemyManager.allowAIToPerformCombos && comboChance >= enemyManager.comboLikelyHood)
        {
            if (currentAttack.comboAction != null)
            {
                willDoComboOnNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else
            {
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }
        }
        else
        {
            currentAttack = null;
        }
    }
    
    private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
    {
        //Rotate manually
        if (enemyManager.canRotate && enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }
    
}
