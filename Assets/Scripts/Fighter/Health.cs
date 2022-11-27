using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Health : MonoBehaviour
{
    [SerializeField] [Range(201, 800)] int _maxHealthValue;
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public event UnityAction Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        SetRandomHealth();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = _currentHealth - damage;

        if (_currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public void GetHeal(int heal)
    {
        _currentHealth = _currentHealth + heal > _maxHealth ? _maxHealth : _currentHealth + heal;
    }

    private void SetRandomHealth()
    {
        _maxHealth = UnityEngine.Random.Range(200, _maxHealthValue);
        _currentHealth = _maxHealth;
    }
}
