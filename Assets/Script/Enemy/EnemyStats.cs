using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{

    private EnemyManager enemyManager;
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    
    public override void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        enemyManager.damageTaken = true;
        Debug.Log("Enemy Damage");
        if (currentHealth <= 0)
        {
            Debug.Log("The enemy is dead!");
        }
    }
    
}
