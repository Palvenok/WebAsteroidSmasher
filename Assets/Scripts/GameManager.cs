using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent<int> OnScoreUpdate;
    public UnityEvent OnGameStarted;

    private int _score = 0;

    public int Score
    {
        get => _score;
        set 
        {
            _score = value;
            OnScoreUpdate?.Invoke(value);
        }
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
