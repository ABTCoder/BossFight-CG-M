using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : EnemyStats
{

    private BossManager bossManager;

    void Start()
    {

        bossManager = GetComponent<BossManager>();
        
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        healthBar.gameObject.SetActive(false);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 20;
        return maxHealth;
    }
    
    public override void TakeDamage(int damage, Transform other = null)
    {
        currentHealth = currentHealth - damage;
        healthBar.TakeDamage(damage);
        bossManager.damageTaken = true;
        if (!bossManager.HasPhaseShifted() && currentHealth <= maxHealth / 2)
        {
            bossManager.ShiftToSecondPhase();
        }
        else if (currentHealth <= 0)
        {
            bossManager.BossDeath();
            Debug.Log("The BOSS is dead! Fuck yeah!");
        }
    }
    
}
