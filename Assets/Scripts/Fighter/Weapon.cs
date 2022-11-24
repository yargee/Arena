using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Equipment/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField][Range(1,100)] private int _minDamage;
    [SerializeField][Range(2, 200)] private int _maxDamage;
    [SerializeField][Range(1, 10)] private float _atackSpeed;
    [SerializeField][Range(1, 20)] private int _criticalChance;
    [SerializeField][Range(1.5f, 5)] private float _criticalModifier;
    [SerializeField][Range(5, 20)] private int _missChance;
    [SerializeField][Range(5, 10)] private int _parryChance;
    [SerializeField] private bool _twoHanded;

    public string Name => _name;
    public int MinDamage => _minDamage;
    public int MaxDamage => _maxDamage;
    public float AtackSpeed => _atackSpeed;
    public int CriticalChance => _criticalChance;
    public float CriticalModifier => _criticalModifier;
    public int MissChance => _missChance;
    public int ParryChance => _parryChance;
    public bool TwoHanded => _twoHanded;
}
