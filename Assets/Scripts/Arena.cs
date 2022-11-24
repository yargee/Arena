using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Arena : MonoBehaviour
{
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField] private List<Fighter> _team_1 = new List<Fighter>();
    [SerializeField] private List<Fighter> _team_2 = new List<Fighter>();
    [SerializeField] private LogContainer _logContainer;
    [SerializeField] private List<AttackableTarget> _attackableTargets = new List<AttackableTarget>();
    public static bool WinnerFound { get; private set; }

    private void OnEnable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.Attacking += OnTryAttack;
            fighter.TargetLost += OnTargetLost;
        }
    }

    private void OnDisable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.Attacking -= OnTryAttack;
            fighter.TargetLost -= OnTargetLost;
        }
    }

    private void OnTryAttack(Fighter attacker, Fighter defender)
    {
        Log log = new Log();
        var attackSequence = new AttackSequence(attacker, defender);

        attackSequence.AttackPhase(ref log, out int damage);

        if (damage > 0)
        {
            attackSequence.DefencePhase(ref log, damage, out int finalDamage);
            attackSequence.ApproveDamagePhase(defender, finalDamage);
        }

        _logContainer.AddLog(log);
    }

    private void OnTargetLost(Fighter fighter)
    {
        //FindRandomTarget(fighter);
        FindLessAttackableTarget(fighter);
    }

    private void FindRandomTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsDead == false && x != fighter);

        if (availableTargets.Count() > 0)
        {
            fighter.SetTarget(availableTargets.ToArray()[UnityEngine.Random.Range(0, availableTargets.Count())]);
        }
        else
        {
            WinnerFound = true;
        }
    }

    private void FindLessAttackableTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsDead == false && x != fighter);

        _attackableTargets = new List<AttackableTarget>();

        foreach(var target in availableTargets)
        {
            int attackers = 0;

            foreach(var attacker in availableTargets)
            {
                if(attacker.Target == target)
                {
                    attackers++;
                }
            }

            _attackableTargets.Add(new AttackableTarget(target, attackers));
        }

        if(_attackableTargets.Count() % 2 != 0)
        {
            var lessAttackableTarget = _attackableTargets.OrderBy(target => target.Attackers).ToArray()[0];
            fighter.SetTarget(lessAttackableTarget.Target);
        }
        else if(_attackableTargets.Count() > 0)
        {
            FindRandomTarget(fighter);
        }
        else
        {
            WinnerFound = true;
        }
        
    }

}
[Serializable]
public class AttackableTarget
{
    public Fighter Target;
    public int Attackers;

    public AttackableTarget(Fighter target, int attackers)
    {
        Target = target;
        Attackers = attackers;
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
        outcomingDamage = 0;

        if (_attacker.IsDead || _defender.IsDead) return;

        if (CombatCalculator.IsAttackSuccessfull(_attacker.Weapon, _attacker.Characteristics.DexterityModifier))
        {
            _attacker.SetAnimation(ConstantKeys.Animations.Attack);
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
            _defender.SetAnimation(ConstantKeys.Animations.Evade);
            log.UpdateAttackLog(_attacker.Name);
        }
    }

    public void DefencePhase(ref Log log, int incomingDamage, out int finalDamage)
    {
        finalDamage = 0;

        if (_defender.IsDead)
        {
            log.UpdateDefenceLog(_defender.Name, 0, ConstantKeys.DefenceStatus.Dead);
            return;
        }

        if (CombatCalculator.IsAttackEvaded(_defender.Characteristics.DexterityModifier))
        {
            log.UpdateDefenceLog(_defender.Name);
            finalDamage = 0;
            _defender.SetAnimation(ConstantKeys.Animations.Evade);
            return;
        }

        if (_defender.Weapon.TwoHanded && CombatCalculator.IsAttackParried(_defender.Weapon, _defender.Characteristics.StrenghModifier))
        {
            finalDamage = CombatCalculator.CalculateParriedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.Parry);
            return;
        }

        if(!_defender.Weapon.TwoHanded && CombatCalculator.IsAttackBlocked(_defender.Characteristics.StrenghModifier))
        {
            finalDamage = CombatCalculator.CalculateBlockedAttackDamage(incomingDamage);
            log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.Block);
            _defender.SetAnimation(ConstantKeys.Animations.Block);
            return;
        }

        finalDamage = incomingDamage;
        log.UpdateDefenceLog(_defender.Name, finalDamage, ConstantKeys.DefenceStatus.FullDamage);
    }

    public void ApproveDamagePhase(IDamagable defender, int damage)
    {
        if (_defender.IsDead) return;

        defender.TakeDamage(damage);
    }
}
