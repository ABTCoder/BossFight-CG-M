using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyManager enemyManager;

    [SerializeField] public ColliderAttack[] enemyColliders;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyManager.enemyRigidBody.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyManager.enemyRigidBody.velocity = velocity;
        
        if (enemyManager.isRotatingWithRootMotion)
        {
            enemyManager.transform.rotation *= anim.deltaRotation;
        }
    }

    public void SetCanDoDamageTrue()
    {
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].GetComponent<Collider>().enabled = true;
        }
    }

    public void ResetCanDoDamage()
    {
        for (int i = 0; i < enemyColliders.Length; i++)
        {
            enemyColliders[i].GetComponent<Collider>().enabled = false;
        }
    }
}
