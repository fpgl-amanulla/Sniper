using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface ITakeDamage
{
    void TakeDamage(int damageAmount);
}

public abstract class Enemy : MonoBehaviour, ITakeDamage
{
    public int health;

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Die");
    }
}
