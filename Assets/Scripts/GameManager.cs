using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreUpdate;
    public UnityEvent OnGameStarted;

    [SerializeField] private AsteroidFactory _asteroidFactory;

    private int _score = 0;

    public int Score
    {
        get => _score;
        private set 
        {
            _score = value;
            OnScoreUpdate?.Invoke(value);
        }
    }

    private void Awake()
    {
        _asteroidFactory?.OnCrushedCountUpdate.AddListener((x) => Score = x);
    }

    public void ReastartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        OnGameStarted?.Invoke();
    }
}
