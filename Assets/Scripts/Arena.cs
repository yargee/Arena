using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arena : MonoBehaviour
{
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField] private LogContainer _logContainer;

    private void OnEnable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.Attacking += OnTryAttack;
        }
    }

    private void OnDisable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.Attacking -= OnTryAttack;
        }
    }

    private void OnTryAttack(Fighter attacker, Fighter defender)
    {
        Log log = new Log();
        var attackSequence = new AttackSequence(attacker, defender);

        attackSequence.AttackPhase(ref log, out int damage);

        if (damage > 0)
        {
            attackSequence.DefencePhase(ref log, damage);
        }

        _logContainer.AddLog(log);

        attackSequence.ApproveDamagePhase(defender, damage);
    }
}

public class AttackSequence
{
    private Fighter _attacker;
    private Fighter _defender;

    public AttackSequence(Fighter attacker, Fighter defender)
    {
        _attacker = attacker;
        _defender = defender;
    }

    public void AttackPhase(ref Log log, out int outcomingDamage)
    {
        if (CombatCalculator.IsAttackSuccessfull(_attacker.Weapon, _attacker.Characteristics.DexterityModifier))
        {
            outcomingDamage = CombatCalculator.CalculateBaseDamage(_attacker.Weapon, _attacker.Characteristics.StrenghModifier);
            log.UpdateAttackLog(_attacker.Name, ConstantKeys.AttackStatus.Hit, outcomingDamage);

            if (CombatCalculator.IsCriticalStrike(_attacker.Weapon, _attacker.Characteristics.DexterityModifier))
            {
                outcomingDamage = CombatCalculator.CalculateCriticalDamage(_attacker.Weapon, outcomingDamage);
                log.UpdateAttackLog(_attacker.Name, ConstantKeys.AttackStatus.CriticalHit, outcomingDamage);
            }
        }
        else
        {
            outcomingDamage = 0;
            log.UpdateAttackLog(_attacker.Name);
        }
    }

    public void DefencePhase(ref Log log, int incomingDamage)
    {
        if (CombatCalculator.IsAttackEvaded(_defender.Characteristics.DexterityModifier))
        {
            log.UpdateDefenceLog(_defender.Name);
            return;
        }

        if (_defender.Weapon.TwoHanded && CombatCalculator.IsAttackParried(_defender.Weapon, _defender.Characteristics.StrenghModifier))
        {
            incomingDamage = CombatCalculator.CalculateParriedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, incomingDamage, ConstantKeys.DefenceStatus.Parry);
            return;
        }

        if(!_defender.Weapon.TwoHanded && CombatCalculator.IsAttackBlocked(_defender.Characteristics.StrenghModifier))
        {
            incomingDamage = CombatCalculator.CalculateBlockedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, incomingDamage);
            return;
        }

        log.UpdateDefenceLog(_defender.Name, incomingDamage, ConstantKeys.DefenceStatus.FullDamage);
    }

    public void ApproveDamagePhase(IDamagable defender, int damage)
    {
        defender.TakeDamage(damage);
    }
}
