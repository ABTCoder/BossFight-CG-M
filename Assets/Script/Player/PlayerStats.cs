using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class PlayerStats : CharacterStats
{
    private GameManager gameManager;
    private AnimationController animationController;
    private CharacterMovement characterMovement;
    private Rigidbody rigidBody;
    

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
        animationController = GetComponentInChildren<AnimationController>();
        characterMovement = GetComponent<CharacterMovement>();
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

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public override void TakeDamage(int damage, Transform other = null)
    {
        if (damage > 0 && !animationController.GetIsRolling())
        {
            if (animationController.GetIsBlocking())
            {
                damage = Mathf.RoundToInt(damage * 0.6f);
                animationController.PlayShieldHit();
            }
            else
            {
                animationController.DamageHit();
                characterMovement.SetAttacker(other.position);
            }
            currentHealth = currentHealth - damage;
            healthBar.TakeDamage(damage);
            if (currentHealth <= 0)
            {
                CharacterMovement.LockControls();
                gameManager.GameOver();
            }
        }
    }

    public void HealPlayer(int hp)
    {
        currentHealth += hp;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        healthBar.Heal(hp);
    }
}
