using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public IdleState IdleState;
    
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.damageAnimRecoveryTime = 0.8f;
        enemyAnimatorManager.PlayTargetAnimation("TakeDamage", true);
        enemyManager.PlayAudioEffect(enemyManager.damageGrunts);
        if (enemyManager.currentTarget != null)
            return rotateTowardsTargetState;
        else return IdleState;
    }
}
