using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] private string _name;
    [SerializeField] private CombatZone _combatZone;
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;

    private float _timeAfterAttack = 0;
    private Fighter _target;

    public event UnityAction<Fighter, Fighter> Attacking;

    public string Name => _name;
    public Weapon Weapon => _weapon;
    public Health Health => _health;
    public Characteristics Characteristics => _characteristics;

    private void OnEnable()
    {
        _combatZone.TargetFound += OnTargetFound;
        _combatZone.TargetLost += OnTargetLost;
    }

    private void OnDisable()
    {
        _combatZone.TargetFound -= OnTargetFound;
        _combatZone.TargetLost -= OnTargetLost;
    }

    private void Update()
    {
        if(_target == null)
        {
            return;
        }

        _timeAfterAttack += Time.deltaTime;

        if (TimeToAttack())
        {
            Attacking(this, _target);
        }
    }

    private void OnTargetFound(Fighter target)
    {
        _target = target;
    }

    private void OnTargetLost()
    {
        _target = null;
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

    private bool TimeToAttack()
    {
        if (_timeAfterAttack > _weapon.AtackSpeed)
        {
            _timeAfterAttack = 0;
            return true;
        }

        return false;
    }

}
