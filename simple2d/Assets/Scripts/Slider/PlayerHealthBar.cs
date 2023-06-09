using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Health _player;

    private Slider _healthBar;
    private bool _isActive;
    private float _barStepMovie = 0.1f;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _healthBar.maxValue = _player.MaxHealth;
        _healthBar.minValue = _player.MinHealth;
    }

    private void OnEnable()
    {
        _player.HealthChanged += ChangeHealth;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ChangeHealth;
    }

    private void ChangeHealth(float targetValue)
    {
        if (!_isActive)
        {
            StartCoroutine(ChangeHealthCoroutine(targetValue));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float targetValue)
    {
        _isActive = true;

        while (_healthBar.value != targetValue)
        {
            _healthBar.value = Mathf.MoveTowards(_healthBar.value, targetValue, _barStepMovie);

            yield return null;
        }
        
        _isActive = false;
    }
}
