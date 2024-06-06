using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    private int _maxHealth;
    public int MaxHealth => _maxHealth;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ApplyDamage(5);
        }
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        OnHitEvent?.Invoke();

        if (_currentHealth <= 0)
            OnDeadEvent?.Invoke();
    }
}
