using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public event UnityAction Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking damage " + damage);

        _currentHealth = damage >= _currentHealth ? 0 : _currentHealth - damage;

        if (_currentHealth == 0)
        {
            Died?.Invoke();
        }
    }

    public void GetHeal(int heal)
    {
        Debug.Log("Healing for " + heal);

        _currentHealth = _currentHealth + heal > _maxHealth ? _maxHealth : _currentHealth + heal;
    }
}
