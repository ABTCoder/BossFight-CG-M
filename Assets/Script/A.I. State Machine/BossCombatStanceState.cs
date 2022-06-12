using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatStanceState : CombatStanceState
{
    [Header("Second Phase Attacks")]
    public bool hasPhaseShifted = false;
    public EnemyAttackAction[] secondPhaseAttacks;

    protected override void GetNewAttack(EnemyManager enemyManager)
    {
        if (hasPhaseShifted)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            
            EnemyAttackAction bossAttackAction = secondPhaseAttacks[Random.Range(0, secondPhaseAttacks.Length)];

            if (distanceFromTarget <= bossAttackAction.maximumDistanceNeededToAttack
                && distanceFromTarget >= bossAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= bossAttackAction.maximumAttackAngle)
                {
                    if (attackState.currentAttack != null)
                        return;
                    else
                    { 
                        attackState.currentAttack = bossAttackAction;
                    }
                }
            }
        }
        else
        {
            base.GetNewAttack(enemyManager);
        }
    }
}
