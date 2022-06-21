using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    private EnemyManager enemyManager;
    private EnemyAnimatorManager enemyAnimatorManager;
    
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    
    public override void TakeDamage(int damage, Transform other = null)
    {
        currentHealth = currentHealth - damage;
        enemyManager.damageTaken = true;
        Debug.Log("Enemy Damage");
        if (currentHealth <= 0)
        {
            Debug.Log("The enemy is dead!");
            enemyAnimatorManager.PlayTargetAnimation("Death", true);
            enemyManager.isDead = true;
        }
    }
    
}
