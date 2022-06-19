using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class BossManager : EnemyManager
{
    //HANDLE SWITCHING PHASE
    //HANDLE SWITCHING ATTACK PATTERNS
    
    private BossStats bossStats;
    private EnemyAnimatorManager bossAnimatorManager;
    private BossCombatStanceState bossCombatStanceState;

    private BossRoomCutscene cs;

    [SerializeField] private GameObject Boss2PhaseCutscene;
    [SerializeField] private Canvas ui;
    [SerializeField] private AudioSource musicAudioSource;
    private PlayableDirector Boss2PhaseCutsceneDirector;
    [SerializeField] private GameObject modelPhaseOne;
    [SerializeField] private GameObject modelPhaseTwo;
    [SerializeField] private GameObject firstAxe;
    [SerializeField] private GameObject secondAxe;
    [SerializeField] private GameObject greatsword;

    private void Awake()
    {
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        bossStats = GetComponent<BossStats>();
        bossAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        cs = GameObject.Find("BossRoom trigger").GetComponent<BossRoomCutscene>();

        Boss2PhaseCutsceneDirector = Boss2PhaseCutscene.GetComponent<PlayableDirector>();

        soundManager = GetComponent<CharacterSoundManager>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();


        isRotatingWithRootMotion = bossAnimatorManager.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = bossAnimatorManager.anim.GetBool("isInteracting");
        canRotate = bossAnimatorManager.anim.GetBool("canRotate");
        isAttacking = bossAnimatorManager.anim.GetBool("isAttacking");
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
        Boss2PhaseCutsceneDirector.Play();
        ui.enabled = false;
        musicAudioSource.Stop();
        CharacterMovement.LockControls();
        modelPhaseOne.SetActive(false);
        modelPhaseTwo.SetActive(true);
        firstAxe.SetActive(false);
        secondAxe.SetActive(false);
        greatsword.SetActive(true);
        transform.localScale += Vector3.one * 0.2f;

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
