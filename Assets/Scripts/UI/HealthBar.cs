using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Sprite _heart;
    [SerializeField] private Sprite _brockenHeart;
    [SerializeField] private GameObject _heartPrefab;

    private Image[] _heartImages;

    public void InitBar(int value)
    {
        _heartImages = new Image[value];

        for (int i = 0; i < _heartImages.Length; i++)
        {
            _heartImages[i] = Instantiate(_heartPrefab, transform).GetComponent<Image>();
        }
    }

    public void UpdateBar(int value)
    {
        for (int i = 0; i < _heartImages.Length; i++)
        {
            _heartImages[i].sprite = i < value ? _heart : _brockenHeart;
        }
    }
}
