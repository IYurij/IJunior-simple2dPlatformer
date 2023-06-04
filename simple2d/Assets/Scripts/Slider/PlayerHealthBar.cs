using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _minHealth = 0;

    private Slider _healthBar;
    private bool isActive;

    public float CurrentHealth => _healthBar.value;

    private void Awake()
    {
        _healthBar = gameObject.GetComponent<Slider>();
        _healthBar.maxValue = _maxHealth;
        _healthBar.minValue = _minHealth;
    }

    public void ChangeHealth(float targetValue)
    {
        if (!isActive)
        {
            StartCoroutine(ChangeHealthCoroutine(targetValue));
        }
    }

    private IEnumerator ChangeHealthCoroutine(float targetValue)
    {
        isActive = true;

        while (Mathf.Abs(CurrentHealth - targetValue) > 0)
        {
            _healthBar.value = Mathf.MoveTowards(CurrentHealth, targetValue, 0.1f);

            yield return null;
        }

        isActive = false;
    }
}
