using TMPro;
using UnityEngine;

public class ScoreTab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lable;
    public void UpdateScore(int value)
    {
        _lable.text = "Score: " + value.ToString();
    }
}
