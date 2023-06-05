using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float _minHealth = 0;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _healthStep = 10;

    public float MinHealth { get { return _minHealth; } }
    public float MaxHealth { get { return _maxHealth; } }
    public float CurrentHealth { get { return _currentHealth; } }

    public event UnityAction<float> HealthChanged;

    private float _targetHealth;
    private float _currentHealth;
    
    public void Heal()
    {
        _targetHealth = Mathf.Clamp(_currentHealth + _healthStep, _minHealth, _maxHealth);
        
        HealthChanged?.Invoke(_targetHealth);

        _currentHealth = _targetHealth;
    }

    public void Damage()
    {
        _targetHealth = Mathf.Clamp(_currentHealth - _healthStep, _minHealth, _maxHealth);
        
        HealthChanged?.Invoke(_targetHealth);
        
        _currentHealth = _targetHealth;
    }
}
