using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Equipment/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _minDamage;
    [SerializeField] private int _maxDamage;
    [SerializeField] private int _atackSpeed;
    [SerializeField] private int _criticalChance;
    [SerializeField] private int _missChance;
    [SerializeField] private int _parryChance;
    [SerializeField] private bool _twoHanded;

    public string Name => _name;
    public int MinDamage => _minDamage;
    public int MaxDamage => _maxDamage;
    public int AtackSpeed => _atackSpeed;
    public int CriticalChance => _criticalChance;
    public int MissChance => _missChance;
    public int ParryChance => _parryChance;
    public bool TwoHanded => _twoHanded;
}
