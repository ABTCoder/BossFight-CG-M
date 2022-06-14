using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    private EnemyAnimatorManager enemyAnimationManager;
    private EnemyStats enemyStats;
    
    public State currentState;
    public CharacterStats currentTarget;
    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidBody;
    
    public bool isInteracting;
    public bool damageTaken;
    public bool isAttacking = false;
    public float rotationSpeed = 15;
    public float maximumAggroRadius = 1.5f;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;

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
        canRotate = enemyAnimationManager.anim.GetBool("canRotate");
        isAttacking = enemyAnimationManager.anim.GetBool("isAttacking");
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
    }

    public virtual bool isEnded()
    {
        return false;
    }
}
