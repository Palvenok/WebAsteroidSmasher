using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SateliteSpawner : MonoBehaviour
{
    [SerializeField] private Satelite _satelitePrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private SpawnArea _spawnArea;
    [SerializeField] private MinMaxCurve _spawScoreSpread;

    private int _currentScore;
    private int _spawnScore;

    public int Score
    {
        set
        {
            _currentScore = value;
            if (_currentScore % _spawnScore == 0)
                SpawnSatelite();
            if (_currentScore % 50 == 0) //Update spawn score to next random value
                GetRandonPoint();
        }
    }

    private void Awake()
    {
        GetRandonPoint();
    }

    private void GetRandonPoint()
    {
        _spawnScore = (int) Random.Range(_spawScoreSpread.constantMin, _spawScoreSpread.constantMax);
    }

    private void SpawnSatelite()
    {
        var satelite = Instantiate(_satelitePrefab);
        satelite.transform.position = _spawnArea.GetPointAtBounds();
        satelite.SetTarget(_target);
        satelite.Launch();
    }
}
