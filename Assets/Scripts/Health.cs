using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public UnityEvent OnExplode;
    public UnityEvent<int> OnHealthUpdate;
    public UnityEvent<int> OnHealthInit;

    [SerializeField, Min(1)] private int _health = 1;
    [SerializeField] private int _maxHealth = 3;

    private int _healthCache = 0;

    private void Awake()
    {
        _healthCache = _health;
        OnHealthInit?.Invoke(_maxHealth);
    }

    public void Heal(int value)
    {
        _health += value;
        _health = Mathf.Clamp(_health, 0, _maxHealth);
        OnHealthUpdate?.Invoke(_health);
    }

    public void HealthReset()
    {
        _health = _healthCache;
        OnHealthUpdate?.Invoke(_health);
    }

    public void TakeDamage()
    {
        _health--;
        OnHealthUpdate?.Invoke(_health);

        if (_health <= 0)
        { 
            OnExplode?.Invoke();
            HealthReset();
        }

    }
}
