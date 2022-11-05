using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 5f;
    [SerializeField] private float _trustForce = 5f;
    [SerializeField] private float _boostForceScale = 1.5f;
    [SerializeField, Range(0, 1)] private float _boostEnergyRate = .05f;
    [SerializeField, Range(0, 1)] private float _boostEnergyRestore = .05f;

    private Rigidbody2D _rb;
    private bool _boostSpeed;
    private Vector2 _moveDirection;
    private bool _isActive;
    private float _boostEnergy = 1;

    public bool IsActive { set { _isActive = value; } }
    public bool BoostSpeed { set { _boostSpeed = value; } }
    public Vector2 MoveDirection { set {_moveDirection = value;} }

    public float BoostEnergy => _boostEnergy;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;

        _boostEnergy = Mathf.Clamp01(_boostEnergy);

        Move();
        LookAt();
    }

    private void LookAt()
    {
        var mouse = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = math.atan2(mouse.y, mouse.x) * Mathf.Rad2Deg;
        _rb.rotation = angle - 90;
    }

    private void Move()
    {
        if (_moveDirection == Vector2.zero) return;

        var force = _trustForce;

        if (_rb.velocity.magnitude > _maxSpeed && !_boostSpeed) force = 1;

        if (_boostSpeed && _boostEnergy > 0) 
        { 
            force *= _boostForceScale;
            _boostEnergy -= _boostEnergyRate * Time.deltaTime;
        }

        if (!_boostSpeed) _boostEnergy += _boostEnergyRestore * Time.deltaTime;

        if (_moveDirection.y < 0) force *= .4f;

        _rb.AddForce(transform.up * _moveDirection.y * force);
        _rb.AddForce(transform.right * _moveDirection.x * force * .3f);
    }
}
