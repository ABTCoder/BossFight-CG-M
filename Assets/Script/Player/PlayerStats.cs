using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private GameManager gameManager;
    private HealthBar healthBar;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        healthBar = GameObject.Find("HealthBar player").GetComponent<HealthBar>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
    }
    
    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        healthBar.SetHealth(maxHealth);
        return maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public override void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        healthBar.TakeDamage(damage);
        Debug.Log("The player get a damage of " + damage);
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
            Debug.Log("Player's health is 0!");
        }
    }
}
