using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidSpawner : MonoBehaviour
{
    public UnityEvent<int> OnCrushedCountUpdate;

    [SerializeField] private Asteroid[] _asteroidsPool;
    [SerializeField] private Asteroid[] _asteroidsPrefabs;
    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private int _asteroidsLimit = 20;
    [SerializeField] private Transform _target;
    [SerializeField] private int _maxAsteroidCount = 5;
    [SerializeField] private float _launchPower = 1f;
    [SerializeField] private float _launchPowerAmplifier = 0.02f;

    private int _currentAsteroidCount = 0;
    private bool _isActive;
    private int _index = 0;
    private int _crushedCount = 0;

    public bool IsActive { set { _isActive = value; } }

    private void Awake()
    {
        _asteroidsPool = new Asteroid[_asteroidsLimit];

        for (int i = 0; i < _asteroidsPool.Length; i++)
        {
            _asteroidsPool[i] = Instantiate(_asteroidsPrefabs[Random.Range(0, _asteroidsPrefabs.Length)], transform);
            _asteroidsPool[i].Instantiate(_target);
            _asteroidsPool[i].gameObject.SetActive(false);
            _asteroidsPool[i].OnExplode.AddListener(OnAsteroidExplode);
        }
    }

    private void Update()
    {
        if (!_isActive) return;

        if (_currentAsteroidCount < _maxAsteroidCount)
        {
            if (_index >= _asteroidsLimit) _index = 0;

            SpawnAsteroid(_index); 
            _currentAsteroidCount++;
            _index++;
        }
    }

    private void SpawnAsteroid(int id)
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


        _asteroidsPool[id].transform.position = point;
        _asteroidsPool[id].transform.Rotate(0, 0, Random.Range(0, 180));
        _asteroidsPool[id].gameObject.SetActive(true);
        _asteroidsPool[id].Launch(_launchPower);
    }

    private void OnAsteroidExplode(bool scored)
    {
        _currentAsteroidCount--;
        if(scored)
            _crushedCount++;
        _launchPower += _launchPowerAmplifier;

        OnCrushedCountUpdate?.Invoke(_crushedCount);
    }
}
