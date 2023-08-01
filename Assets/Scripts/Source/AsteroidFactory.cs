using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidFactory : MonoBehaviour
{
    public UnityEvent<int> OnCrushedCountUpdate;

    [SerializeField] private Asteroid[] _asteroidsPrefabs;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private Transform _target;
    [Space]
    [SerializeField] private int _asteroidsCountAtStart = 5;
    [SerializeField, Min(1)] private int _increaseScorePoint = 30;
    [SerializeField] private float _launchPower = 1f;
    [SerializeField] private float _launchPowerAmplifier = 0.02f;

    private int _asteroidsLimit = 20;
    private int _crushedCount;

    private List<ObjectPool<Asteroid>> _listOfPools;

    public void SetTarget(Transform target) => _target = target;

    private void Awake()
    {        
        _listOfPools = new List<ObjectPool<Asteroid>>();

        foreach (var item in _asteroidsPrefabs)
        {
            var pool = new ObjectPool<Asteroid>(item, _asteroidsLimit, transform);
            foreach (Asteroid asteroid in pool)
            {
                asteroid.OnExplode.AddListener(OnAsteroidExplode);
                asteroid.SetTarget(_target);
                asteroid.gameObject.SetActive(false);
            }
            _listOfPools.Add(pool);
        }
    }

    public void EnableSpawner()
    {
        for (int i = 0; i < _asteroidsCountAtStart; i++)
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        int poolIndex = Random.Range(0, _listOfPools.Count);

        var asteroid = _listOfPools[poolIndex].GetNext();
        asteroid.transform.position = GetPointAtBounds();
        asteroid.transform.Rotate(0, 0, Random.Range(0, 180));
        asteroid.gameObject.SetActive(true);
        asteroid.Launch(_launchPower);
    }

    private Vector2 GetPointAtBounds()
    {
        Vector2 point = new Vector2();

        int rand = Random.Range(0, 4);

        switch (rand)
        {
            case 0:
                point.x = _boxCollider.bounds.max.x;
                point.y = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
                break;
            case 1:
                point.x = _boxCollider.bounds.min.x;
                point.y = Random.Range(_boxCollider.bounds.min.y, _boxCollider.bounds.max.y);
                break;
            case 2:
                point.y = _boxCollider.bounds.min.y;
                point.x = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
                break;
            case 3:
                point.y = _boxCollider.bounds.max.y;
                point.x = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
                break;
        }

        return point;
    }

    private void OnAsteroidExplode(bool scored)
    {
        ///TODO:
        ///Fix double picking on explode

        if(scored)
            _crushedCount++;
        _launchPower += _launchPowerAmplifier;

        OnCrushedCountUpdate?.Invoke(_crushedCount);

        SpawnAsteroid();
        if (_crushedCount % _increaseScorePoint == 0)
        {
            SpawnAsteroid();
        }
    }
}
