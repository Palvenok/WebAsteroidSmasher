using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField, Min(10)] private int _bulletCount = 50;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab, _bulletCount, transform);
        foreach (Bullet bullet in _pool) 
        {
            bullet.gameObject.SetActive(false);
        }
    }

    public Bullet GetNextBullet()
    {
        return _pool.GetNext();
    }
}
