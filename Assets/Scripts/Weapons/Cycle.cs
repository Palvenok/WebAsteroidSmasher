using System.Collections;
using UnityEngine;

public class Cycle : Weapon
{
    internal override IEnumerator FireCoroutine()
    {
        _isCanFire = false;

        if (_pointIndex > _launchPoints.Length - 1) _pointIndex = 0;

        var bullet = _bulletPool.GetNextBullet();

        bullet.transform.position = _launchPoints[_pointIndex].position;
        bullet.transform.rotation = _launchPoints[_pointIndex].rotation;
        bullet.Launch();
        _pointIndex++;

        yield return new WaitForSeconds(_shootDelay);

        _isCanFire = true;
    }
}
