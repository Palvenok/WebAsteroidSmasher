using UnityEngine;

public class PlayerWeaponHolder : MonoBehaviour
{
    [SerializeField] private BulletPool _bulletPool;
    [SerializeField] private Weapon[] _weapons;

    private Weapon _activeWeapon;
    private int _weaponIndex;
    private bool _isActive;

    public int WeaponIndex
    {
        get { return _weaponIndex; }
        private set
        {
            _weaponIndex = value;
            if (value < 0) _weaponIndex = 0;
            if (value >= _weapons.Length) _weaponIndex = _weapons.Length - 1;
        }
    }

    private void Awake()
    {
        GetComponent<Player>().OnStatusUpdate.AddListener(x => _isActive = x);

        foreach (var weapon in _weapons)
            weapon.SetBulletPool(_bulletPool);

        _activeWeapon = _weapons[WeaponIndex];
    }

    public void NextWeapon()
    {
        WeaponIndex++;
        _activeWeapon = _weapons[WeaponIndex];
    }

    public void Fire()
    {
        if(_isActive)
            _activeWeapon.Fire();
    }
}
