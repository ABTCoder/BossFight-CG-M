using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    // Start is called before the first frame update
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyManager.currentRecoveryTime = 2f;
        enemyAnimatorManager.PlayTargetAnimation("TakeDamage", true);

        return rotateTowardsTargetState;
    }
}
