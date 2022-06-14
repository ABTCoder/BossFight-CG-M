using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public PursueTargetState pursueTargetState;
    public EnemyAttackAction currentAttack;

    [SerializeField] private ColliderAttack[] enemyColliders;
    
    public bool hasPerformedAttack = false;
    
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        RotateTowardsTargetWhilstAttacking(enemyManager);

        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            return pursueTargetState;
        }

        //ATTACK!
        //Roll for a combo chance
        if (!hasPerformedAttack)
        {
            for (int i = 0; i < enemyColliders.Length; i++)
            {
                if (currentAttack.colliderName == enemyColliders[i].name || currentAttack.colliderName == "All")
                    enemyColliders[i].SetDamage(currentAttack.attackDamage);
                else
                {
                    enemyColliders[i].SetDamage(0);
                }
                enemyColliders[i].resetCollided();
            }
            enemyAnimatorManager.anim.SetBool("isAttacking", true);
            AttackTarget(enemyAnimatorManager, enemyManager);
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager)
    {
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
        currentAttack = null;
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
