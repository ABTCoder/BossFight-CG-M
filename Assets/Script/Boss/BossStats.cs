using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats
{
    [SerializeField] private HealthBar healthBar;

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
        if (currentHealth <= 0)
        {
            Debug.Log("The BOSS is dead! Fuck yeah!");
        }
    }
    
}
