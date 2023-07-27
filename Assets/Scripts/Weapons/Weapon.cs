using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] internal Transform[] _launchPoints;
    [SerializeField, Min(0.05f)] internal float _shootDelay = 1;
    
    internal BulletPool _bulletPool;
    internal bool _isCanFire = true;
    internal int _pointIndex = 0;

    public void SetBulletPool(BulletPool bulletPool)
    {
        _bulletPool = bulletPool;
    }

    public void Fire()
    {
        if (_isCanFire)
            StartCoroutine(FireCoroutine());
    }

    internal abstract IEnumerator FireCoroutine();
}