using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth = 100;
    [SerializeField] private Slider _healthView;

    public event UnityAction Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
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

    public void InitView()
    {
        _healthView.maxValue = _maxHealth;
        _healthView.value = _maxHealth;
    }

    public void SetHealth(int health)
    {
        _maxHealth = health;
        _currentHealth = health;
    }
}
