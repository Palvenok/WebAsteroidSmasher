using System.Collections;
using UnityEngine;

public class Truster : MonoBehaviour
{
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _boostedColor;
    [SerializeField] private float _animationDelay = 1f;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PlayerMovement _playerMovement;

    private Vector2 _moveDirection;
    private bool _flag;
    private bool _boosted;
    private int _index = 0;
    private bool _isActive;

    public bool Boosted { set => _boosted = value; }
    public bool IsActive { set => _isActive = value; }
    public Vector2 MoveDirection { set => _moveDirection = value; }


    private void Update()
    {
        if (!_isActive) return;

        _spriteRenderer.enabled = _moveDirection != Vector2.zero ? true : false;
        _spriteRenderer.color = _playerMovement.BoostEnergy > 0 && _boosted ? _boostedColor : _defaultColor;

        if (_flag) return;

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        _flag = true;

        if (_index >= _sprites.Length) _index = 0;
        _spriteRenderer.sprite = _sprites[_index];
        _index++;

        yield return new WaitForSeconds(_animationDelay);

        _flag = false;
    }

}
