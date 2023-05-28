using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _kickSpeed;

    private int _kickDirection;
    private bool _isKicked;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidbody2D;
    private PlayerMovement _playerMovieComponent;
    private Animator _playerAnimator;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovieComponent = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isKicked)
        {
            GetKick();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _isKicked = false;
            _playerMovieComponent.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _playerAnimator.SetTrigger("Kick");

            _isKicked = true;
            _playerMovieComponent.enabled = false;

            _kickDirection = enemy.transform.position.x > transform.position.x ? -1 : 1;
        }
    }

    private void GetKick()
    {
        _rigidbody2D.AddForce(_kickSpeed * _kickDirection * transform.right, ForceMode2D.Impulse);
    }
}
