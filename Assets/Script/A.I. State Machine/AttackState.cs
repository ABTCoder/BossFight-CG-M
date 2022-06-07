using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        //Select one of our many attacks based on attack scores
        //If the selected attack is not able to be used because of bad angle or distance, select a new attack
        //If the attack is viable, stop our movement and attack our target
        //Set our recovery timer to the attacks recovery time
        //Return the combat stance state
        return this;
    }
}
