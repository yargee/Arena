using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : IAttackable
{
    private float _timeToAttack;
    private int _damage;
    private Fighter _attacker;

    public int Damage => _damage;
    public Fighter Fighter => _attacker;
    public float TimeToAttack => _timeToAttack;

    public void InitFighter(Fighter fighter)
    {
        _attacker = fighter;
    }

    public bool AttackSuccesfull()
    {
        return Random.Range(1, 101) + _attacker.Characteristics.DexterityModifier > _attacker.Weapon.MissChance;
    }

    public ConstantKeys.CombatStatus CalculateOutcomingDamage()
    {
        int weaponDamage = Random.Range(_attacker.Weapon.MinDamage, _attacker.Weapon.MaxDamage + 1);
        _damage = _attacker.Weapon.TwoHanded ? Mathf.RoundToInt((weaponDamage + _attacker.Characteristics.StrenghModifier) * ConstantKeys.TwoHandedWeaponDamageModifier)
                                             : weaponDamage + _attacker.Characteristics.StrenghModifier;

        if(Random.Range(1, 101) < _attacker.Weapon.CriticalChance)
        {
            _damage = Mathf.RoundToInt(_damage * _attacker.Weapon.CriticalModifier);
            return ConstantKeys.CombatStatus.CriticalHit;
        }
        else
        {
            return ConstantKeys.CombatStatus.Hit;
        }
    }

    public void SpeedBasedAttack(IDefencable defender)
    {
        if (AttackSuccesfull())
        {
            var status = CalculateOutcomingDamage();
            //Debug.Log($"Attack {defender} for {_damage} damage");
            defender.GetLog(new Log(_attacker, status, _damage));
            defender.StartDefence(_damage);
            
        }
        else
        {
            //Debug.Log("Attack missed");
            defender.GetLog(new Log(_attacker, ConstantKeys.CombatStatus.Miss));
            defender.StartDefence(0);
        }
    }
}
