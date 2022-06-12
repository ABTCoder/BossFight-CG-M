using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossManager : EnemyManager
{
    //HANDLE SWITCHING PHASE
    //HANDLE SWITCHING ATTACK PATTERNS
    
    private BossStats bossStats;
    private EnemyAnimatorManager bossAnimatorManager;
    private BossCombatStanceState bossCombatStanceState;

    private BossRoomCutscene cs;

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        bossStats = GetComponent<BossStats>();
        bossAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        cs = GameObject.Find("BossRoom trigger").GetComponent<BossRoomCutscene>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isRotatingWithRootMotion = bossAnimatorManager.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = bossAnimatorManager.anim.GetBool("isInteracting");
        canRotate = bossAnimatorManager.anim.GetBool("canRotate");
    }

    protected override void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, bossStats, bossAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
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

    public bool HasPhaseShifted()
    {
        return bossCombatStanceState.hasPhaseShifted;
    }
}
