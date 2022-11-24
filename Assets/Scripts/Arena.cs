using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arena : MonoBehaviour
{
    [SerializeField] private List<Fighter> _fighters = new List<Fighter>();

    private void OnEnable()
    {
        foreach(var fighter in _fighters)
        {
            fighter.TryAttack += OnTryAttack;
        }
    }

    private void OnDisable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.TryAttack -= OnTryAttack;
        }
    }

    private void OnTryAttack(Fighter attacker, Fighter defender)
    {
        var attackSequence = new AttackSequence(attacker, defender);
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

    public void AttackPhase()
    {
      //  _attacker.AttackBehaviour.
    }
}
