using UnityEngine;
using UnityEngine.Events;

public class Fighter : MonoBehaviour, IDamagable, IHealable, IFighter
{
    [SerializeField] private string _name;
    [SerializeField] private Characteristics _characteristics;
    [SerializeField] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Armor _armor;
    [SerializeField] private CombatZone _combatZone;

    private IAttackable _attackBehaviour;
    private IDefencable _defenceBehabiour;

    public event UnityAction<Fighter, Fighter> TryAttack;

    public string Name => _name;
    public Weapon Weapon => _weapon;
    public Health Health => _health;
    public Characteristics Characteristics => _characteristics;
    public IAttackable AttackBehaviour => _attackBehaviour;
    public IDefencable DefenceBehaviour => _defenceBehabiour;

    private void OnEnable()
    {
        InitBehaviours();
        _combatZone.Init(_attackBehaviour, Weapon.AtackSpeed);
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void InitBehaviours()
    {
        _attackBehaviour = new AttackBehaviour();
        _attackBehaviour.InitFighter(this);

        _defenceBehabiour = new DefenceBehaviour();
        _defenceBehabiour.InitFighter(this);
    }

    public void Equip(Weapon weapon, Armor armor)
    {
        _weapon = weapon;
        _armor = armor;
    }

    private void OnDied()
    {
        _combatZone.Stop();
        Destroy(gameObject);
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
}
