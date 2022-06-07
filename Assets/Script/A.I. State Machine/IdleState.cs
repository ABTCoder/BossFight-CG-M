using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Look for a potential target
        //Switch to the pursue target state if target is found
        //If not return this state
        return this;
    }
}
