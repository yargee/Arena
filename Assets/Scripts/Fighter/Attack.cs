using UnityEngine;

public class Attack : CombatBehaviour
{
    private IAttackableTarget _target;
    private float _timeAfterAttack = 0;

    public bool HasTarget => _target != null;

    private void Update()
    {
        _timeAfterAttack += Time.deltaTime;
    }

    public void SetTarget(IAttackableTarget target)
    {
        _target = target;
    }

    public void LoseTarget()
    {
        _target = null;
    }

    public void AttackTarget()
    {
        if (_target != null && TimeToAttack())
        {
            _target.TakeAttack(CalculateDamage());
        }
    }

    public int CalculateDamage()
    {
        if (TryHit())
        {
            int damage = Random.Range(Weapon.MinDamage, Weapon.MaxDamage + 1);

            damage = Weapon.TwoHanded ? Mathf.RoundToInt((damage + Characteristics.StrenghModifier) * ConstantKeys.TwoHandedWeaponDamageModifier) : damage + Characteristics.StrenghModifier;

            if (TryCriticalStrike())
            {
                damage = Mathf.RoundToInt(damage * Weapon.CriticalModifier);
            }

            return damage;
        }
        else
        {
            return 0;
        }
    }

    private bool TryHit()
    {
        return Random.Range(1, 101) + Characteristics.DexterityModifier > Weapon.MissChance;
    }

    private bool TryCriticalStrike()
    {
        return Random.Range(1, 101) <= Weapon.CriticalChance + Characteristics.DexterityModifier;
    }
    
    private bool TimeToAttack()
    {
        if (_timeAfterAttack > Weapon.AtackSpeed - (Characteristics.DexterityModifier + Characteristics.StrenghModifier) / 10)
        {
            _timeAfterAttack = 0;
            return true;
        }

        return false;
    }
}
