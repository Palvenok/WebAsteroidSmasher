using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image)), RequireComponent(typeof(Toggle))]
public class ToggleSpriteSwaper : MonoBehaviour
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _toggledSprite;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateSprite(bool value)
    {
        _image.sprite = value ? _defaultSprite : _toggledSprite;
    }
}
