using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthStep = 10;
    [SerializeField] private PlayerHealthBar _healthBar;

    public event UnityAction<float> HealthChanged;

    private float _targetHealth;
    private float _currentHealth;
    private float _maxValue;
    private float _minValue;
    private Slider _slider;

    private void Start()
    {
        _slider = _healthBar.GetComponent<Slider>();
        _maxValue = _slider.maxValue;
        _minValue = _slider.minValue;
    }

    public void Heal()
    {
        _currentHealth = _healthBar.CurrentHealth;

        if (_currentHealth + _healthStep <= _maxValue)
        {
            _targetHealth = _currentHealth + _healthStep;

            HealthChanged?.Invoke(_targetHealth);
        }
    }

    public void Damage()
    {
        _currentHealth = _healthBar.CurrentHealth;
        
        if (_currentHealth - _healthStep >= _minValue)
        {
            _targetHealth = _currentHealth - _healthStep;

            HealthChanged?.Invoke(_targetHealth);
        }
    }
}
