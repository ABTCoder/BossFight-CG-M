using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : IdleState
{
    protected override State HandleSwitchingToNextState(EnemyManager enemyManager)
    {
        if (enemyManager.currentTarget != null && enemyManager.isEnded())
        {
            return pursueTargetState;
        }
        else
        {
            return this;
        }
    }
}
