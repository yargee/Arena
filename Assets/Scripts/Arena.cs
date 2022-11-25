using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Targeter _targeter;
    [SerializeField] private Mover _mover;
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField] private LogContainer _logContainer;

    public static bool WinnerFound { get; private set; }

    private void OnEnable()
    {
        _targeter.Init(_fighters);
        _targeter.TargetsUnavailable += OnTargetsUnavailable;

        foreach (var fighter in _fighters)
        {
            fighter.Attacking += OnTryAttack;
            fighter.TargetLost += _targeter.TakeAttackerAsTarget;
            fighter.TargetFound += _mover.MoveToTarget;
        }
    }

    private void OnDisable()
    {
        _targeter.TargetsUnavailable -= OnTargetsUnavailable;

        foreach (var fighter in _fighters)
        {
            fighter.Attacking -= OnTryAttack;
            fighter.TargetLost -= _targeter.TakeAttackerAsTarget;
            fighter.TargetFound -= _mover.MoveToTarget;
        }
    }

    private void Start()
    {
        foreach (var fighter in _fighters)
        {
            _targeter.FindLessAttackableTarget(fighter);
        }
    }

    private void OnTargetsUnavailable()
    {
        WinnerFound = true;
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
