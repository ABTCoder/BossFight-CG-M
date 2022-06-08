using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : CharacterStats
{
    private HealthBar healthBar;

    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        healthBar = GameObject.Find("UI").transform.Find("HealthBar Boss").GetComponent<HealthBar>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        healthBar.SetHealth(maxHealth);
        return maxHealth;
    }
    
    public override void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.TakeDamage(damage);
        Debug.Log("BOSS Damage");
        if (currentHealth <= 0)
        {
            Debug.Log("The BOSS is dead! Fuck yeah!");
        }
    }
    
}
