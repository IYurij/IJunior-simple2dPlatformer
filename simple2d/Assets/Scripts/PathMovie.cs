using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PathMovie : MonoBehaviour
{
    [SerializeField, Range(0.1f, 3)] private float _speed;
    [SerializeField] private Point _movieFrom;
    [SerializeField] private Point _movieTo;

    private int _speedMultyplayer;
    private bool _isMovieRight = true;
    private SpriteRenderer _sprite;

    public void SetDirection(int direction)
    {
        _speedMultyplayer = direction;
        _isMovieRight = direction == 1;
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _speedMultyplayer = _isMovieRight == false ? -1 : 1;

        if (transform.position.x >= _movieTo.transform.position.x)
        {
            _isMovieRight = false;
        }

        if (transform.position.x <= _movieFrom.transform.position.x)
        {
            _isMovieRight = true;
        }

        _sprite.flipX = _isMovieRight;

        transform.Translate(_speed * _speedMultyplayer * Time.deltaTime * Vector2.right);
    }
}
