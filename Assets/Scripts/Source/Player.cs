using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health)), RequireComponent(typeof(PlayerWeaponHolder))]
public class Player : MonoBehaviour
{
    public UnityEvent OnExplode;
    public UnityEvent<bool> OnStatusUpdate;
    [SerializeField] private ParticleSystem _explodeParticles;

    private bool _isActive;
    private Health _health;
    private PlayerWeaponHolder _weaponHolder;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _weaponHolder = GetComponent<PlayerWeaponHolder>();
    }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            OnStatusUpdate?.Invoke(_isActive);
        }
    }

    public void Explode()
    {
        Instantiate(_explodeParticles, transform.position, Quaternion.identity);
        IsActive = false;
        OnExplode?.Invoke();
        gameObject.SetActive(false);
    }

    public void Upgrade()
    {
        if (_health.CurrentHealth >= _health.MaxHealth)
            _weaponHolder.NextWeapon();
        else
            _health.Heal(1);
    }
}
