using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public IdleState IdleState;
    
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.damageAnimRecoveryTime = 2.5f;
        enemyAnimatorManager.PlayTargetAnimation("TakeDamage", true);
        enemyManager.soundManager.PlayAudioEffect(enemyManager.soundManager.damageAudioClips);
        if (enemyManager.currentTarget != null)
            return rotateTowardsTargetState;
        else return IdleState;
    }
}
