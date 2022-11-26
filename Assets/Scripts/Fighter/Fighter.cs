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
    [SerializeField] private FighterStateMachine _stateMachine;
    [SerializeField] private Targeter _targeter;

    private bool _defeated = false;
    private float _timeAfterAttack = 0;
    private Fighter _target;

    public event UnityAction<Fighter, Fighter> Attacking;
    public bool Defeated => _defeated;
    public Targeter Targeter => _targeter;
    public Animator Animator => _animator;
    public string Name => _name;
    public Weapon Weapon => _weapon;
    public Health Health => _health;
    public Characteristics Characteristics => _characteristics;
    public Fighter Target => _target;

    private void OnEnable()
    {
        _stateMachine.Init(this);
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
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        SetAnimation(ConstantKeys.Animations.Death);
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
