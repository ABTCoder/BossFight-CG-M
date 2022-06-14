using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public EnemyAttackAction[] baseAttacks;
    public PursueTargetState pursueTargetState;
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
        
        return this;
    }

    protected void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        //Rotate with pathfinding (NavMesh)
        Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;
        
        enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
        enemyManager.enemyRigidBody.velocity = targetVelocity;
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
    }
    
    protected virtual void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        EnemyAttackAction enemyAttackAction = baseAttacks[Random.Range(0, baseAttacks.Length)];

        if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
            && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
        {
            if (viewableAngle <= enemyAttackAction.maximumAttackAngle)
            {
                if (attackState.currentAttack != null)
                    return;
                else
                { 
                    attackState.currentAttack = enemyAttackAction;
                }
            }
        }
    }
}
