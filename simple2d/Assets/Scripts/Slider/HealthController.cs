using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _minHealth = 0;
    [SerializeField] private float _healthStep = 10f;

    private float _currentHealth;
    private float _targetHealth;
    bool isIncrease;
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = FindObjectOfType<PlayerHealthBar>().GetComponent<Slider>();
        _healthBar.maxValue = _maxHealth;
    }

    public void IncreaseHealthOnClick()
    {
        isIncrease = true;
        _targetHealth = _currentHealth + _healthStep;

        StartCoroutine(ChangeHealth(_targetHealth));
    }

    public void DecreaseHealthOnClick()
    {
        isIncrease = false;
        _targetHealth = _currentHealth - _healthStep;

        StartCoroutine(ChangeHealth(_targetHealth));
    }

    private IEnumerator ChangeHealth(float targetValue)
    {
        //bool isIncrease = _currentHealth - _targetHealth <= 0;
        
        while ((_currentHealth < targetValue && isIncrease == true) || (_currentHealth > targetValue && isIncrease == false))
        {
            _healthBar.value = Mathf.MoveTowards(_currentHealth, targetValue, 0.1f);
            
            _currentHealth = _healthBar.value;

            yield return null;
        }
    }
}
