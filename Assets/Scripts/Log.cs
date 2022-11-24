using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Log
{
    public string _textLog;

    private string _attackLog;
    private string _defenceLog;

    public void UpdateAttackLog(string name)
    {
        _attackLog = $"{name}_{ConstantKeys.AttackStatus.Fail}";
        _textLog = _attackLog;
    }
    public void UpdateAttackLog(string name, ConstantKeys.AttackStatus hit, int damage)
    {
        _attackLog = $"{name}_{hit}_{damage}";
        _textLog = _attackLog;
    }
    public void UpdateDefenceLog(string name)
    {
        _defenceLog = $" // {name}_{ConstantKeys.DefenceStatus.Evade}";
        _textLog += _defenceLog;
    }

    public void UpdateDefenceLog(string name, int finishDamage, ConstantKeys.DefenceStatus status)
    {
        _defenceLog = $" // {name}_{status}_{finishDamage}";
        _textLog += _defenceLog;
    }
}

