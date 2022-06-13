using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider health;

    private void Awake()
    {
        health = GetComponent<Slider>();
    }

    public void SetHealth(int maxHealth)
    {
        health.maxValue = maxHealth;
        health.value = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        health.value -= damage;
    }

    public void Heal(int hp)
    {
        health.value += hp;
    }
}
