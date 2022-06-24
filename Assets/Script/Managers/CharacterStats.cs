using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;
    public bool hasFloatingLifebar = false;

    public abstract void TakeDamage(int damage, Transform other = null);
}
