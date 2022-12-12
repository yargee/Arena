using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();
    [SerializeField] private List<AttackableTarget> _attackableTargets = new List<AttackableTarget>();
    
    public void FindRandomTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsActive && x != fighter);

        if (availableTargets.Count() > 0)
        {
            fighter.SetTarget(availableTargets.ToArray()[UnityEngine.Random.Range(0, availableTargets.Count())]);
        }
        else
        {
            fighter.StopFighting();
        }
    }

    public bool TryFindTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsActive && x != fighter);
        _attackableTargets.Clear();

        foreach (var target in availableTargets)
        {
            int attackers = 0;

            foreach (var attacker in availableTargets)
            {
                if (attacker.Name == target.Name)
                {
                    attackers++;
                }
            }

            _attackableTargets.Add(new AttackableTarget(target, attackers));
        }

        if (_attackableTargets.Count() % 2 != 0)
        {
            var lessAttackableTarget = _attackableTargets.OrderBy(target => target.Attackers).ToArray()[0];
            fighter.SetTarget(lessAttackableTarget.Target);
            return true;
        }
        else if (_attackableTargets.Count() > 0)
        {
            FindRandomTarget(fighter);
            return true;
        }
        else
        {
            fighter.StopFighting();
            return false;
        }
    }

    public void TakeAttackerAsTarget(Fighter fighter)
    {
        var targets = _fighters.Where(x => x.IsActive && x != fighter && x.Target.Name == fighter.Name).ToArray();

        if (targets.Count() > 0)
        {
            fighter.SetTarget(targets[UnityEngine.Random.Range(0, targets.Count())]);
        }
        else
        {
            TryFindTarget(fighter);
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