using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour, IAttackableTarget
{
    [SerializeField] private string _name;
    [SerializeField] private Armor _armor;
    [SerializeField] private Weapon _weapon;

    [SerializeField] private string _targetName;

    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Attack _attack;
    [SerializeField] private Defence _defence;
    [SerializeField] private CustomAnimator _animator;
    
    public string Name => _name;
    public CustomAnimator Animator => _animator;
    public bool IsActive { get; private set; } = true;
    public IAttackableTarget Target { get; private set; }
    public Vector2 Position { get; private set; }

    public bool CanAttack =>IsActive && _attack.HasTarget && _attack.TimeToAttack();

    private void Awake()
    {
        _attack.Init(_weapon, _characteristics);
        _defence.Init(_weapon, _characteristics, _armor);
    }

    private void OnEnable()
    {
        _defence.Health.Died += StopFighting;
    }
    private void OnDisable()
    {
        _defence.Health.Died += StopFighting;
    }

    private void Update()
    {
        Position = transform.position;
    }

    public void SetTarget(IAttackableTarget target)
    {
        Target = target;
        _attack.SetTarget(target);
        _targetName = target.Name;
    }

    public void LoseTarget()
    {
        Target = null;
        _attack.LoseTarget();
    }

    public void TakeAttack(int damage)
    {
        _defence.TakeDamage(damage);
    }

    public void Attack()
    {
        _attack.AttackTarget();
    }

    public void StopFighting()
    {
        StartCoroutine(Stop());
    }

    private IEnumerator Stop()
    {
        Target = null;
        IsActive = false;
        _attack.LoseTarget();
        _targetName = "";

        if (_defence.Health.CurrentHealth <= 0)
        {
            transform.SetAsFirstSibling();
            _animator.PlayAnimation(ConstantKeys.Animations.Die);
            yield break;
        }

        yield return new WaitForSeconds(1);

        _animator.PlayAnimation(ConstantKeys.Animations.Idle, null, true);
    }
}
