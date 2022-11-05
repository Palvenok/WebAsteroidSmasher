using System.Collections;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private GameObject _bulletPoolParrent;
    [SerializeField] private Transform _launchPoint;
    [SerializeField, Min(0)] private int _ammoPoolLinit;
    [SerializeField, Min(0.05f)] private float _shootDelay = 1;
    
    private Bullet[] _bulletPool;
    private int _bulletIndex = 0;
    private bool _isCanFire;
    private bool _isActive;

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
        _isCanFire = true;
        _bulletPool = new Bullet[_ammoPoolLinit];

        for (int i = 0; i < _ammoPoolLinit; i++)
        {
            _bulletPool[i] = Instantiate(_bulletPrefab, _bulletPoolParrent.transform);
            _bulletPool[i].gameObject.SetActive(false);
        }
    }

    public void Fire()
    {
        if (!_isActive) return;

        if(_isCanFire)
            StartCoroutine(FireCoroutine());
    }

    private IEnumerator FireCoroutine()
    {
        _isCanFire = false;
        if (_bulletIndex >= _ammoPoolLinit) _bulletIndex = 0;

        _bulletPool[_bulletIndex].transform.position = _launchPoint.position;
        _bulletPool[_bulletIndex].transform.rotation = _launchPoint.rotation;
        _bulletPool[_bulletIndex].Launch();

        _bulletIndex++;

        yield return new WaitForSeconds(_shootDelay);

        _isCanFire = true;
    }
}
