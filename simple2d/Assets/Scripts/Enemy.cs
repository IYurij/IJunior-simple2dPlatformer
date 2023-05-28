using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private PathMovie _pathMovie;

    private Animator _animator;
    private int _kickDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            _animator.Play("EnemyAttack");

            _kickDirection = player.transform.position.x > transform.position.x ? 1 : -1;

            _pathMovie.SetDirection(_kickDirection);
        }
    }
}
