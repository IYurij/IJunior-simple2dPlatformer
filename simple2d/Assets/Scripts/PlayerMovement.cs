using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float _minMoveDistance = 0.001f;
    private const float _shellRadius = 0.01f;

    [SerializeField] private float MinGroundNormalY = .65f;
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField, Range(1, 3)] private int SpeedMultyplayer = 2;
    [SerializeField] private LayerMask LayerMask;

    private Vector2 Velocity;
    private Vector2 _targetVelocity;
    private Vector2 _groundNormal;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2d;
    private Animator _animator;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private bool _grounded;

    private void OnEnable()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(LayerMask);
        _contactFilter.useLayerMask = true;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _targetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (_targetVelocity.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_targetVelocity.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        if (_targetVelocity.x != 0)
        {
            _animator.SetFloat("Speed", Math.Abs(_targetVelocity.x));
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }

        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            Velocity.y = 5;

            _animator.SetTrigger("Jump");
        }
    }

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = _targetVelocity.x * SpeedMultyplayer;

        _grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }
}
