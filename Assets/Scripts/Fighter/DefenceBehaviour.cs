using UnityEngine;
using UnityEngine.Events;

public class DefenceBehaviour : IDefencable
{
    private int _incomingDamage;
    private Fighter _defender;
    private Log _log;

    public Log Log => _log;
    public int IncomingDamage => _incomingDamage;
    public Fighter Fighter => _defender;

    public event UnityAction<Log> LogFinished;

    public void InitFighter(Fighter fighter)
    {
        _defender = fighter;
    }

    public void ApproveDamage(IDamagable target)
    {
        target.TakeDamage(_incomingDamage);
    }

    public ConstantKeys.CombatStatus CalculateIncomingDamage(int damage)
    {
        if (Random.Range(1, 101) <= _defender.Weapon.ParryChance + _defender.Characteristics.StrenghModifier)
        {
            _incomingDamage = Mathf.RoundToInt(damage * ConstantKeys.ParryModifier);  //descrease by armor
            return ConstantKeys.CombatStatus.Parry;
        }
        else if (Random.Range(1, 101) <= _defender.Characteristics.DexterityModifier + ConstantKeys.BlockChance)
        {
            _incomingDamage = Mathf.RoundToInt(damage * ConstantKeys.BlockModifier);  //descrease by armor
            return ConstantKeys.CombatStatus.Block;
        }
        else
        {
            _incomingDamage = damage; //descrease by armor
            return ConstantKeys.CombatStatus.None;
        }
    }

    public bool DefenceSuccessful()
    {
        return Random.Range(1, 101) <= _defender.Characteristics.DexterityModifier + ConstantKeys.EvasionChance;
    }

    public void StartDefence(int damage)
    {
        Debug.Log($"{_defender.Name} try defence {damage} damage");

        if (DefenceSuccessful())
        {
            Debug.Log($"{_defender.Name} evaded {damage} damage");
            _log.UpdateLog(_defender, ConstantKeys.CombatStatus.Evade, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.None);
        }
        else
        {
            var status = CalculateIncomingDamage(damage);

            switch (status)
            {
                case ConstantKeys.CombatStatus.Parry:
                    Debug.Log($"{_defender.Name} parryed {damage} damage");
                    _log.UpdateLog(_defender, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.Parry, ConstantKeys.CombatStatus.None, _incomingDamage);
                    break;
                case ConstantKeys.CombatStatus.Block:
                    Debug.Log($"{_defender.Name} blocked {damage} damage");
                    _log.UpdateLog(_defender, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.Block, _incomingDamage);
                    break;
                case ConstantKeys.CombatStatus.None:
                    Debug.Log($"{_defender.Name} takes full {damage} damage");
                    _log.UpdateLog(_defender, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.None, ConstantKeys.CombatStatus.None, _incomingDamage);
                    break;
            }

            ApproveDamage(_defender);
        }

        LogFinished?.Invoke(_log);
    }

    public void GetLog(Log log)
    {
        _log = log;
    }
}
