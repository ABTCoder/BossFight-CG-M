using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    private EnemyAnimatorManager enemyAnimationManager;
    private EnemyStats enemyStats;
    
    public State currentState;
    public CharacterStats currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidBody;

    public bool isPreformingAction;
    public bool isInteracting;
    public float rotationSpeed = 15;
    public float maximumAggroRadius = 1.5f;

    [Header("Combat Flags")]
    public bool canDoCombo;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    //The higher, and lower, respectively these angles are, the greater detection FIELD OF VIEW (basically like eye sight)
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;

    [Header("A.I. Combat Settings")] 
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;

    private void Awake()
    {
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void Start()
    {
        enemyRigidBody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        canDoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
        canRotate = enemyAnimationManager.anim.GetBool("canRotate");
    }

    protected virtual void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    protected void SwitchToNextState(State state)
    {
        currentState = state;
    }

    protected void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPreformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }
        }
    }

    public virtual bool isEnded()
    {
        return false;
    }
}
