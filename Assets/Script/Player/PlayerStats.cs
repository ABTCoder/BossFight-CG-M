using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private GameManager gameManager;
    [SerializeField] private HealthBar healthBar;
    private AnimationController combatController;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        combatController = GetComponentInChildren<AnimationController>();
    }
    
    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public override void TakeDamage(int damage)
    {
        if (combatController.getIsBlocking())
        {
            damage = Mathf.RoundToInt(damage * 0.6f);
            combatController.PlayShieldHit();
        }
        else combatController.DamageHit();
        currentHealth = currentHealth - damage;
        healthBar.TakeDamage(damage);
        //Debug.Log("The player get a damage of " + damage);
        if (currentHealth <= 0)
        {
            gameManager.GameOver();
            Debug.Log("Player's health is 0!");
        }
    }
}
