using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Fighter : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] private string _name;
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private CustomAnimator _animator;
    [SerializeField] private FighterStateMachine _stateMachine;
    [SerializeField] private Targeter _targeter;

    private bool _defeated = false;
    private float _timeAfterAttack = 0;
    private Fighter _target;

    public event UnityAction<Fighter, Fighter> Attacking;
    public bool Defeated => _defeated;
    public Targeter Targeter => _targeter;
    public CustomAnimator Animator => _animator;
    public string Name => _name;
    public Weapon Weapon => _weapon;
    public Health Health => _health;
    public Characteristics Characteristics => _characteristics;
    public Fighter Target => _target;

    private void OnEnable()
    {
        _stateMachine.Init(this);
        SetAnimation(ConstantKeys.Animations.Idle, true);
        _health.Died += Defeat;
    }

    private void OnDisable()
    {
        _health.Died -= Defeat;
    }

    private void Update()
    {
        _timeAfterAttack += Time.deltaTime;
    }

    public void Init(Targeter targeter)
    {
        _targeter = targeter;
    }

    public void Defeat()
    {
        _target = null;
        _defeated = true;
        transform.SetAsFirstSibling();
        SetAnimation(ConstantKeys.Animations.Die);
        Debug.LogError(this.name + " DEFEATED ");
        _stateMachine.Stop();
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

    public void LoseTarget()
    {
        _target = null;
    }

    public void Attack()
    {
        if (Defeated) return;

        if (_target.Defeated && !Arena.WinnerFound)
        {
            return;
        }

        if (Arena.WinnerFound)
        {
            return;
        }

        if (TimeToAttack())
        {
            SetAnimation(ConstantKeys.Animations.Attack, false, () => SetAnimation(ConstantKeys.Animations.Idle, true));
            Attacking?.Invoke(this, _target);
        }
    }


    private bool TimeToAttack()
    {
        if (_timeAfterAttack > _weapon.AtackSpeed - (_characteristics.DexterityModifier + _characteristics.StrenghModifier)/10)
        {
            _timeAfterAttack = 0;
            return true;
        }

        return false;
    }

    public void SetAnimation(ConstantKeys.Animations name, bool loop = false, UnityAction Callback = null)
    {
        _animator.PlayAnimation(name, Callback, loop);
    }
}
