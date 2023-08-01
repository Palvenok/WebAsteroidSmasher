using UnityEngine;
using TMPro;

public class ScoreBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _lable;
    public void UpdateScore(int value)
    {
        _lable.text = value.ToString();
    }
}
