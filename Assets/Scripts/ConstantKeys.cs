using UnityEngine;
using UnityEngine.Events;

public static class ConstantKeys
{
    public const float TwoHandedWeaponDamageModifier = 1.5f;
    public const int EvasionChance = 15;
    public const int BlockChance = 40;
    public const float ParryModifier = 0.75f;
    public const float BlockModifier = 0.5f;

    public enum AttackStatus
    {
        Fail,
        Hit,
        CriticalHit
    }    

    public enum DefenceStatus
    {
        Evade,
        Parry,
        Block,
        FullDamage
    }
}
