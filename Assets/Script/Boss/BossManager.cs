using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManager : EnemyManager
{
    //HANDLE SWITCHING PHASE
    //HANDLE SWITCHING ATTACK PATTERNS
    
    private EnemyAnimatorManager bossAnimatorManager;
    private BossCombatStanceState bossCombatStanceState;
    
    private BossRoomCutscene cs;

    private void Awake()
    {
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        bossAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        cs = GameObject.Find("BossRoom trigger").GetComponent<BossRoomCutscene>();
    }

    public void ShiftToSecondPhase()
    {
        //PLAY AN ANIMATION /W AN EVENT THAT TRIGGERS PARTICLE FX/WEAPON FX
        //SWITCH ATTACK ACTIONS

        bossAnimatorManager.PlayTargetAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }

    public override bool isEnded()
    {
        return cs.IsEnded();
    }
}
