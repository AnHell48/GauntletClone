using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager //: MonoBehaviour
{
    private float currentHealth, maxHealth, damageTaken;
    private int livesLeft, livesMax;
    public float checkHealth
    {
        get{return currentHealth;}
    }
    public HealthManager(float max_Health)
    {
        maxHealth = max_Health;
        currentHealth = maxHealth;
    }

    public HealthManager(float max_Health, int lives)
    {
        maxHealth = max_Health;
        livesMax = lives;
        livesLeft = livesMax;
    }

    public void RestoreHealth(float restoreAmmount)
    {
        currentHealth += restoreAmmount;
    }
    
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    public bool StillAlive()
    {
        if (currentHealth <= 0)
        {
            return false;
        }

        return true;
    }

    
}
