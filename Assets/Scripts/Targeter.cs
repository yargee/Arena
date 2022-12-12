using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    [SerializeField] private List<Fighter> _targets = new List<Fighter>();

    private Queue<Fighter> _waitForTargetQueue = new Queue<Fighter>();

    public event UnityAction<Fighter> TargetsUnavailable;

    private void Update()
    {
        if (_waitForTargetQueue.Count == 0) return;

        var fighter = _waitForTargetQueue.Dequeue();
        TakeAttackerAsTarget(fighter);
    }

    public void FindRandomTarget(Fighter fighter)
    {
        var availableTargets = _targets.Where(x => !x.IsAlive && x != fighter);

        if (availableTargets.Count() > 0)
        {
            fighter.SetTarget(availableTargets.ToArray()[UnityEngine.Random.Range(0, availableTargets.Count())]);
        }
        else
        {
            TargetsUnavailable?.Invoke(fighter);
        }
    }

    public void FindLessAttackableTarget(Fighter fighter)
    {
        var availableTargets = _targets.Where(x => !x.IsAlive && x != fighter);

        var _attackableTargets = new List<AttackableTarget>();

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
        }
        else if (_attackableTargets.Count() > 0)
        {
            FindRandomTarget(fighter);
        }
        else
        {
            TargetsUnavailable?.Invoke(fighter);
        }
    }

    public void TakeAttackerAsTarget(Fighter fighter)
    {
        var targets = _targets.Where(x => !x.IsAlive && x != fighter && x.Target.Name == fighter.Name).ToArray();

        if (targets.Count() > 0)
        {
            fighter.SetTarget(targets[UnityEngine.Random.Range(0, targets.Count())]);
        }
        else
        {
            FindLessAttackableTarget(fighter);
        }
    }
}

[Serializable]
public class AttackableTarget
{
    public IAttackableTarget Target;
    public int Attackers;

    public AttackableTarget(IAttackableTarget target, int attackers)
    {
        Target = target;
        Attackers = attackers;
    }
}