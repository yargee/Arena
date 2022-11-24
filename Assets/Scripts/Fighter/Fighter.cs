using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] private string _name;
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private Animator _animator;

    private float _timeAfterAttack = 0;
    private Fighter _target;
    private bool _isDead;

    public event UnityAction<Fighter, Fighter> Attacking;
    public event UnityAction<Fighter> TargetLost;

    public string Name => _name;
    public Weapon Weapon => _weapon;
    public Health Health => _health;
    public bool IsDead => _isDead;
    public Characteristics Characteristics => _characteristics;
    public Fighter Target => _target;

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    private void Start()
    {
        TargetLost?.Invoke(this);
    }

    private void Update()
    {
        TryAttack();
    }

    private void OnDied()
    {
        if (_isDead) return;
                
        _isDead = true;
        _target = null;
        transform.SetAsFirstSibling();
        SetAnimation(ConstantKeys.Animations.Death);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogError("damage < 0");
            return;
        }

        _health.TakeDamage(damage);
    }

    public void GetHeal(int heal)
    {
        if (heal < 0)
        {
            Debug.LogError("heal < 0");
            return;
        }

        _health.GetHeal(heal);
    }

    public void SetTarget(Fighter target)
    {
        _target = target;
    }

    private void TryAttack()
    {
        if (IsDead) return;

        if (_target.IsDead && !Arena.WinnerFound)
        {
            TargetLost?.Invoke(this);
            return;
        }

        if (Arena.WinnerFound)
        {
            return;
        }

        _timeAfterAttack += Time.deltaTime;

        if (TimeToAttack())
        {
            Attacking?.Invoke(this, _target);
        }
    }
    
    private bool TimeToAttack()
    {
        if (_timeAfterAttack > _weapon.AtackSpeed)
        {
            _timeAfterAttack = 0;
            return true;
        }

        return false;
    }

    public void SetAnimation(ConstantKeys.Animations key)
    {
        _animator.SetTrigger(key.ToString());
    }
}
