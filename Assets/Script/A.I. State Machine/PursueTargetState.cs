using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;
    
    //Chase the target
    //If within attack range, return combat stance state
    //If target is out of range, return this state and continue to chase target
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.navMeshAgent.isStopped = false;
        
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        HandleRotateTowardsTarget(enemyManager);
        
        if (enemyManager.isInteracting)
            return this;
        
        //If we are preforming an action, stop our movement!
        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            enemyAnimatorManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        if (distanceFromTarget <= enemyManager.maximumAggroRadius)
        {
            return combatStanceState;
        }
        else
        {
            return this;
        }
    }
    
    private void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        //Rotate with pathfinding (NavMesh)
        Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;
            
        enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
        enemyManager.enemyRigidBody.velocity = targetVelocity;
        enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
    }
}
