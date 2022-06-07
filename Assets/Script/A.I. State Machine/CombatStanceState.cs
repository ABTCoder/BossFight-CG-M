using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Check for attack range
        //Potentially circle player or walk around them
        //If in attack range return attack state
        //If we are in cool down after attacking, return this state and continue circling player
        //If the player runs out of range, return pursue target state
        return this;
    }
}
