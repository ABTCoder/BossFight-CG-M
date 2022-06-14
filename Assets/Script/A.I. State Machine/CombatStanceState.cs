using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public EnemyAttackAction[] baseAttacks;
    public PursueTargetState pursueTargetState;
    public RotateTowardsTargetState rotateTowardsTargetState;
    public DamageState damageState;

    //Check for attack range
    //Potentially circle player or walk around them
    //If in attack range return attack state
    //If we are in cool down after attacking, return this state and continue circling player
    //If the player runs out of range, return pursue target state
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.navMeshAgent.isStopped = true;

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        attackState.hasPerformedAttack = false;

        if(enemyManager.damageTaken)
        {
            enemyManager.damageTaken = false;
            return damageState;
        }
        enemyManager.damageTaken = false;

        if (enemyManager.isInteracting)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 0);
            enemyAnimatorManager.anim.SetFloat("Horizontal", 0);
            return this;
        }
        
        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            return pursueTargetState;
        }

        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            return attackState;
        }
        else
        {
            GetNewAttack(enemyManager);
        }
        
        return rotateTowardsTargetState;
    }

    protected void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        //Rotate with pathfinding (NavMesh)
        Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        direction.y = 0;
        direction.Normalize();

        if (direction == Vector3.zero)
        {
            direction = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, 0.05f * enemyManager.rotationSpeed * Time.deltaTime);
    }
    
    protected virtual void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        
        List<EnemyAttackAction> tempAttacksArray = new List<EnemyAttackAction>();
        for (int i = 0; i < baseAttacks.Length; i++)
        {
            if (distanceFromTarget <= baseAttacks[i].maximumDistanceNeededToAttack
                && distanceFromTarget >= baseAttacks[i].minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= baseAttacks[i].maximumAttackAngle)
                {
                    tempAttacksArray.Add(baseAttacks[i]);
                }
            }
        }

        if (tempAttacksArray.Count > 0)
        {
            EnemyAttackAction enemyAttackAction = tempAttacksArray[Random.Range(0, tempAttacksArray.Count)];
        
            if (attackState.currentAttack != null)
                return;
            else
            { 
                attackState.currentAttack = enemyAttackAction;
            }
        }
    }
}
