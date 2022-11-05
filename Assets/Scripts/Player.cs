using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnExplode;
    public UnityEvent<bool> OnStatusUpdate;
    [SerializeField] private ParticleSystem _explodeParticles;

    private bool _isActive;

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
}
