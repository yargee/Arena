using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Health : MonoBehaviour
{
    [SerializeField] [Range(201, 800)] int _maxHealthValue;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private Slider _healthView;

    public event UnityAction Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        SetRandomHealth();
        InitView();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage;
        _healthView.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
            _healthView.gameObject.SetActive(false);
        }
    }

    public void GetHeal(int heal)
    {
        _currentHealth = _currentHealth + heal > _maxHealth ? _maxHealth : _currentHealth + heal;
        _healthView.value = _currentHealth;
    }

    private void SetRandomHealth()
    {
        _maxHealth = UnityEngine.Random.Range(200, _maxHealthValue);
        _currentHealth = _maxHealth;
    }

    private void InitView()
    {
        _healthView.maxValue = _maxHealth;
        _healthView.value = _maxHealth;
    }
}
