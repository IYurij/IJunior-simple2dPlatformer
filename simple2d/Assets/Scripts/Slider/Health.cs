using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _healthStep = 10f;

    private PlayerHealthBar _healthBar;
    private float _targetHealth;
    private float _currentHealth;
    private bool _isIncrease;
    private float _maxValue;
    private float _minValue;

    private void Awake()
    {
        _healthBar = FindObjectOfType<PlayerHealthBar>();
    }

    private void Start()
    {
        _maxValue = _healthBar.GetComponent<Slider>().maxValue;
        _minValue = _healthBar.GetComponent<Slider>().minValue;
    }

    public void IncreaseHealthOnClick()
    {
        _isIncrease = true;

        PrepareTargetHealth();

        _healthBar.ChangeHealth(_targetHealth);
    }

    public void DecreaseHealthOnClick()
    {
        _isIncrease = false;

        PrepareTargetHealth();

        _healthBar.ChangeHealth(_targetHealth);
    }

    private void PrepareTargetHealth()
    {
        _currentHealth = _healthBar.CurrentHealth;

        if (_currentHealth >= _minValue && _currentHealth <= _maxValue)
        {
            if (_isIncrease == true)
            {
                if (_currentHealth + _healthStep >= _maxValue)
                {
                    _targetHealth = _maxValue;

                    return;
                }

                _targetHealth = _currentHealth + _healthStep;
            }
            else
            {
                if (_currentHealth - _healthStep <= _minValue)
                {
                    _targetHealth = _minValue;

                    return;
                }

                _targetHealth = _currentHealth - _healthStep;
            }
        }
    }
}
