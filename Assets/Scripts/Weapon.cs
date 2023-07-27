using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Transform[] _launchPoints;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField, Min(0.05f)] private float _shootDelay = 1;
    
    private bool _isCanFire = true;
    private bool _isActive;
    private int _weaponIndex = 0;

    public bool IsActive { set { _isActive = value; } }
    public float ShootDelay 
    { 
        set 
        { 
            _shootDelay -= value * .01f;
            if (_shootDelay < 0.05f) _shootDelay = 0.05f;
        } 
    }

    private void Awake()
    {
        if(_weaponType == WeaponType.Unknown)
            Debug.LogWarning("Weapon type not seted, used cycle by default weapon");
    }

    public void Fire()
    {
        if (!_isActive) return;
        
        ///TODO:
        /// Разделить на отдельные классы
        
        if(_isCanFire)
            switch(_weaponType)
            {
                case WeaponType.Cycle:
                    StartCoroutine(CycleFireCoroutine());
                    break;
                case WeaponType.Shotgun:
                    StartCoroutine(ShotgunFireCoroutine());
                    break;
                default:
                    StartCoroutine(CycleFireCoroutine());
                    break;
            }
    }

    private IEnumerator CycleFireCoroutine()
    {
        _isCanFire = false;

        if (_weaponIndex > _launchPoints.Length - 1) _weaponIndex = 0;

        var bullet = _bulletPool.GetNextBullet();

        bullet.transform.position = _launchPoints[_weaponIndex].position;
        bullet.transform.rotation = _launchPoints[_weaponIndex].rotation;
        bullet.Launch();
        _weaponIndex++;

        yield return new WaitForSeconds(_shootDelay);

        _isCanFire = true;
    }

    private IEnumerator ShotgunFireCoroutine()
    {
        _isCanFire = false;

        foreach (var point in _launchPoints)
        {
            var bullet = _bulletPool.GetNextBullet();

            bullet.transform.position = point.position;
            bullet.transform.rotation = point.rotation;
            bullet.Launch();
        }

        yield return new WaitForSeconds(_shootDelay);

        _isCanFire = true;
    }
}

public enum WeaponType
{
    Unknown,
    Cycle,
    Shotgun
}