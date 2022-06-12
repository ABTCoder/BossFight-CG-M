using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCombatStanceState : CombatStanceState
{
    [Header("Second Phase Attacks")]
    public bool hasPhaseShifted;
    public EnemyAttackAction[] secondPhaseAttacks;

    protected override void GetNewAttack(EnemyManager enemyManager)
    {
        if (hasPhaseShifted)
        {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < secondPhaseAttacks.Length; i++)
            {
                EnemyAttackAction bossAttackAction = secondPhaseAttacks[i];

                if (distanceFromTarget <= bossAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= bossAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= bossAttackAction.maximumAttackAngle)
                    {
                        maxScore += bossAttackAction.attackScore;
                    }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < secondPhaseAttacks.Length; i++)
            {
                EnemyAttackAction bossAttackAction = secondPhaseAttacks[i];

                if (distanceFromTarget <= bossAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget >= bossAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= bossAttackAction.maximumAttackAngle)
                    {
                        if (attackState.currentAttack != null)
                            return;

                        temporaryScore += bossAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        { 
                            attackState.currentAttack = bossAttackAction;
                        }
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
