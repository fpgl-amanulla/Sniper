using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public override void TakeDamage(int damageAmount)
    {
        //base.TakeDamage(damageAmount);
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, 3.0f);
    }
}
