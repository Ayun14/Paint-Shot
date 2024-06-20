using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitEvent;
    public UnityEvent OnDeadEvent;

    [SerializeField] private int _maxHealth = 100;
    public int MaxHealth => _maxHealth;
    private int _currentHealth;
    public int CurrentHealth => _currentHealth;

    private void Start()
    {
        HealthReset();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ApplyDamage(5);
        }
    }

    public void HealthReset()
    {
        _currentHealth = _maxHealth;
        OnHitEvent?.Invoke();
    }

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
        OnHitEvent?.Invoke();

        if (_currentHealth <= 0)
            OnDeadEvent?.Invoke();
    }
}
