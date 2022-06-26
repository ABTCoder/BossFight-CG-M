using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public IdleState IdleState;
    
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.navMeshAgent.isStopped = true;
        enemyManager.navMeshAgent.velocity = Vector3.zero;
        enemyManager.damageAnimRecoveryTime = enemyManager.maxDamageRecoveryTime;
        enemyAnimatorManager.PlayTargetAnimation("TakeDamage", true);
        enemyManager.soundManager.PlayAudioEffect(enemyManager.damageAudioClips);
        if (enemyManager.currentTarget != null)
            return rotateTowardsTargetState;
        else return IdleState;
    }
}
