using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Targeter : MonoBehaviour
{
    [SerializeField] private List<AttackableTarget> _attackableTargets = new List<AttackableTarget>();

    private IReadOnlyList<Fighter> _fighters;
    private Queue<Fighter> _waitForTargetQueue = new Queue<Fighter>();

    public event UnityAction TargetsUnavailable;

    private void Update()
    {
        if (_waitForTargetQueue.Count == 0) return;

        var fighter = _waitForTargetQueue.Dequeue();
        TakeAttackerAsTarget(fighter);
    }

    public void Init(IReadOnlyList<Fighter> fighters)
    {
        _fighters = fighters;
    }

    public void FindRandomTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsDead == false && x != fighter);

        if (availableTargets.Count() > 0)
        {
            fighter.SetTarget(availableTargets.ToArray()[UnityEngine.Random.Range(0, availableTargets.Count())]);
        }
        else
        {
            TargetsUnavailable?.Invoke();
        }
    }

    public void FindLessAttackableTarget(Fighter fighter)
    {
        var availableTargets = _fighters.Where(x => x.IsDead == false && x != fighter);

        _attackableTargets = new List<AttackableTarget>();

        foreach (var target in availableTargets)
        {
            int attackers = 0;

            foreach (var attacker in availableTargets)
            {
                if (attacker.Target == target)
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
            TargetsUnavailable?.Invoke();
        }
    } 

    public void TakeAttackerAsTarget(Fighter fighter)
    {
        Debug.LogError("Take attacker as target");
        var targets = _fighters.Where(x => x != fighter && x.Target == fighter).ToArray();

        if(targets.Count() > 0)
        {
            Debug.LogError("Taken attacker");
            fighter.SetTarget(targets[UnityEngine.Random.Range(0, targets.Count())]);
        }
        else
        {
            Debug.LogError("No attackers, find random");
            FindLessAttackableTarget(fighter);
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