using System;
using UnityEngine;

[Serializable]
public class Log
{
    [SerializeField] private string _log;
    private string _attackLog;
    private string _defenceLog;

    public string Logg => _log;

    public Log(Fighter attacker, ConstantKeys.CombatStatus hit, int damage = 0)
    {
        _attackLog = $"{attacker.Name}_{hit}_{damage}";
        _defenceLog = string.Empty;
        _log = _attackLog;
    }

    public void UpdateLog(Fighter defender, ConstantKeys.CombatStatus evade, ConstantKeys.CombatStatus parry, ConstantKeys.CombatStatus block, int finishDamage = 0)
    {
        _defenceLog = $" // {defender.Name}_{evade}/{parry}/{block}_{finishDamage}";
        _log += _defenceLog;
    }

    private void DecodeLog()
    {

    }
}

