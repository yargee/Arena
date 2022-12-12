using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]
public class Defence : CombatBehaviour, IDamagable
{
    private Health _health;

    public Health Health => _health;

    public bool IsAlive { get; private set; }

    public event UnityAction Evaded;
    public event UnityAction Blocked;
    public event UnityAction Parried;
    public event UnityAction Defeated;

    private void OnEnable()
    {
        _health = GetComponent<Health>();
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
    }

    public void TakeDamage(int damage)
    {
        int modifiedDamage = ModifyDamage(damage);

        if (modifiedDamage < 0)
        {
            return;
        }

        _health.TakeDamage(modifiedDamage);
    }

    private bool TryEvade()
    {
        return Random.Range(1, 101) <= Characteristics.DexterityModifier + ConstantKeys.EvasionChance;
    }

    private bool TryParry()
    {
        return Weapon.TwoHanded && Random.Range(1, 101) <= Weapon.ParryChance + Characteristics.StrenghModifier;
    }

    private bool TryBlock()
    {
        return !Weapon.TwoHanded && Random.Range(1, 101) <= Characteristics.StrenghModifier + ConstantKeys.BlockChance;
    }

    private int ModifyDamage(int damage)
    {
        if (TryEvade())
        {
            Evaded?.Invoke();
            return 0;
        }

        if (TryParry())
        {
            Parried?.Invoke();
            return Mathf.RoundToInt(damage * ConstantKeys.ParryModifier) - Characteristics.ConstitutionModifier;
        }

        if (TryBlock())
        {
            Blocked?.Invoke();
            return Mathf.RoundToInt(damage * ConstantKeys.BlockModifier) - Characteristics.ConstitutionModifier;
        }

        return damage;
    }

    private void OnDied()
    {
        IsAlive = false;
        Defeated?.Invoke();
    }
}
