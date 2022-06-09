using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats
{
    [SerializeField] private HealthBar healthBar;

    private BossManager bossManager;

    void Start()
    {
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
    
    public override void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.TakeDamage(damage);
        //Debug.Log(currentHealth);
        /*if (currentHealth <= maxHealth / 2)
        {
            bossManager.ShiftToSecondPhase();
        }
        else*/ if (currentHealth <= 0)
        {
            Debug.Log("The BOSS is dead! Fuck yeah!");
        }
    }
    
}
