using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceBehaviour : IDefencable
{
    private int _incomingDamage;
    private Fighter _defender;

    public int IncomingDamage => _incomingDamage;
    public Fighter Fighter => _defender;

    public void InitFighter(Fighter fighter)
    {
        _defender = fighter;
    }

    public void ApproveDamage(IDamagable target)
    {
        target.TakeDamage(_incomingDamage);
    }

    public int CalculateIncomingDamage(int outcomingDamage)
    {
        return outcomingDamage;
    }

    public bool DefenceSuccessful()
    {
        return Random.Range(1, 101) <= _defender.Characteristics.DexterityModifier * 10; //add evasion
    }

    public void StartDefence(int damage)
    {
        //Debug.Log("Start defence " + _defender + "--" + damage);

        if (DefenceSuccessful())
        {
            Debug.Log($"{_defender} evades from attack");
        }
        else
        {
            _incomingDamage = CalculateIncomingDamage(damage);
            ApproveDamage(_defender);
        }
    }
}
