using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    private float _currentHealth;

    public UnityEvent<Entity> OnTakeDamage, OnDeath;

    protected float MaxHealth { get; set; }

    protected float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Min(MaxHealth, value); }
    }

    public void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;

        OnTakeDamage?.Invoke(this);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
    }
}
