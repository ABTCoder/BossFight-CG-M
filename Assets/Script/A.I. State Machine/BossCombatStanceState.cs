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
        
            List<EnemyAttackAction> tempAttacksArray = new List<EnemyAttackAction>();
            for (int i = 0; i < secondPhaseAttacks.Length; i++)
            {
                if (distanceFromTarget <= secondPhaseAttacks[i].maximumDistanceNeededToAttack
                    && distanceFromTarget >= secondPhaseAttacks[i].minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= secondPhaseAttacks[i].maximumAttackAngle)
                    {
                        tempAttacksArray.Add(secondPhaseAttacks[i]);
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
        else
        {
            base.GetNewAttack(enemyManager);
        }
    }
}
