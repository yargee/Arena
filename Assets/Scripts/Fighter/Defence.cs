using UnityEngine;
using UnityEngine.Events;

public class Defence : CombatBehaviour, IDamagable
{
    [SerializeField] private Health _health;

    public Health Health => _health;

    public event UnityAction Evaded;
    public event UnityAction Blocked;
    public event UnityAction Parried;
    public event UnityAction Defeated;

    public void TakeDamage(int damage)
    {
        int modifiedDamage = ModifyDamage(damage);

        if (modifiedDamage < 0)
        {
            return;
        }
        Debug.Log("FINAL " + modifiedDamage);
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
            Debug.Log("EVADED" );
            return 0;
        }

        if (TryParry())
        {
            Parried?.Invoke();
            Debug.Log("PARRIED " + damage);
            return Mathf.RoundToInt(damage * ConstantKeys.ParryModifier) - Characteristics.ConstitutionModifier;
        }

        if (TryBlock())
        {
            Blocked?.Invoke();
            Debug.Log("BLOCKED " + damage);
            return Mathf.RoundToInt(damage * ConstantKeys.BlockModifier) - Characteristics.ConstitutionModifier;
        }

        return damage;
    }

}
