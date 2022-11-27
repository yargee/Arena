using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Targeter _targeter;
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField] private LogContainer _logContainer;

    public static bool WinnerFound { get; private set; }

    private void OnEnable()
    {
        _targeter.Init(_fighters);
        _targeter.TargetsUnavailable += OnTargetsUnavailable;

        Randomize();

        foreach (var fighter in _fighters)
        {
            fighter.Attacking += OnAttacking;
            fighter.Init(_targeter);
        }

    }

    private void OnDisable()
    {
        _targeter.TargetsUnavailable -= OnTargetsUnavailable;

        foreach (var fighter in _fighters)
        {
            fighter.Attacking -= OnAttacking;
        }
    }

    private void Start()
    {
        foreach (var fighter in _fighters)
        {
            _targeter.FindLessAttackableTarget(fighter);
        }
    }

    private void Randomize()
    {
        for (int i = 0; i < _fighters.Count; i++)
        {
            int newIndex = Random.Range(0, _fighters.Count);
            var tempotaryValue = _fighters[newIndex];
            _fighters[newIndex] = _fighters[i];
            _fighters[i] = tempotaryValue;
        }
    }

    private void OnTargetsUnavailable()
    {
        WinnerFound = true;
    }

    private void OnAttacking(Fighter attacker, Fighter defender)
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

        if (_attacker.Defeated || _defender.Defeated) return;

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
            _defender.SetAnimation(ConstantKeys.Animations.Block);
            log.UpdateAttackLog(_attacker.Name);
        }
    }

    public void DefencePhase(ref Log log, int incomingDamage, out int finalDamage)
    {
        finalDamage = 0;

        if (_defender.Defeated)
        {
            _attacker.LoseTarget();
            log.UpdateDefenceLog(_defender.Name, 0, ConstantKeys.DefenceStatus.Dead);
            return;
        }

        if (CombatCalculator.IsAttackEvaded(_defender.Characteristics.DexterityModifier))
        {
            log.UpdateDefenceLog(_defender.Name);
            finalDamage = 0;
            _defender.SetAnimation(ConstantKeys.Animations.Block);
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
        defender.TakeDamage(damage);

        if (_defender.Defeated)
        {
            _attacker.LoseTarget();
            return;
        }
    }
}
