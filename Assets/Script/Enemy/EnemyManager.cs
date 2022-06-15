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
    public bool isDead = false;
    public bool damageTaken;
    public bool isAttacking = false;
    public float rotationSpeed = 15;
    public float maximumAggroRadius = 1.5f;

    [Header("A.I. Settings")]
    public float detectionRadius = 20;
    public float maximumDetectionAngle = 50;
    public float currentRecoveryTime = 0;
    public float damageAnimRecoveryTime = 0;

    private Renderer renderer;
    private Color materialColor;
    [SerializeField] private Material fadeMaterial;
    private bool changedMaterial = false;


    //Sound stuff
    public CharacterSoundManager soundManager;

    private void Awake()
    {
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        renderer = GetComponentInChildren<Renderer>();
        soundManager = GetComponent<CharacterSoundManager>();
    }
    
    private void Start()
    {
        enemyRigidBody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        if (!isDead)
        {
            HandleStateMachine();
        }
        else
        {
            if(!changedMaterial)
            {
                renderer.material = fadeMaterial;
                changedMaterial = true;
                soundManager.PlayAudioEffect(soundManager.deathCriesAudioClips);
            }
                
            materialColor = renderer.material.color;
            float fadeAmount = (float)(materialColor.a - (0.3f * Time.deltaTime));
            materialColor = new Color(materialColor.r, materialColor.g, materialColor.b, fadeAmount);
            renderer.material.color = materialColor;
            if (materialColor.a < 0)
                Destroy(gameObject);
        }

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
        if (damageAnimRecoveryTime > 0)
        {
            damageAnimRecoveryTime -= Time.deltaTime;
        }
    }

    public virtual bool isEnded()
    {
        return false;
    }

}
