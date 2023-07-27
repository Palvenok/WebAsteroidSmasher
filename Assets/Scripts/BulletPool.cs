using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField, Min(10)] private int _bulletCount = 20;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(_bulletPrefab, _bulletCount, transform);
    }

    public Bullet GetNextBullet()
    {
        return _pool.GetNext();
    }
}
