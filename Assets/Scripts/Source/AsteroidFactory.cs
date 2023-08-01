using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidFactory : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> OnCrushedCountUpdate;

    [SerializeField] private Asteroid[] _asteroidsPrefabs;
    [SerializeField] private SpawnArea _spawnArea;
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
        asteroid.transform.position = _spawnArea.GetPointAtBounds();
        asteroid.gameObject.SetActive(true);
        asteroid.Launch(_launchPower);
    }

    private void OnAsteroidExplode(bool scored)
    {
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
