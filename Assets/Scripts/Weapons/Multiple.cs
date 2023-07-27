using System.Collections;
using UnityEngine;

public class Multiple : Weapon
{
    internal override IEnumerator FireCoroutine()
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
