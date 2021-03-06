using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    private float _currentHealth;

    public UnityEvent<Entity, DamageData> OnTakeDamage, OnDeath;

    public float MaxHealth => _maxHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = Mathf.Min(MaxHealth, value); }
    }

    protected virtual void Awake()
    {
        _currentHealth = MaxHealth;
    }

    public void ApplyDamage(DamageData data)
    {
        CurrentHealth -= data.amount;

        OnTakeDamage?.Invoke(this, data);

        if (CurrentHealth <= 0)
            Die(data);
    }

    public virtual void Die(DamageData data)
    {
        OnDeath?.Invoke(this, data);
    }
}
