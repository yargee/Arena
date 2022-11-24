using UnityEngine;
using UnityEngine.Events;

public static class ConstantKeys
{
    public const float TwoHandedWeaponDamageModifier = 1.5f;
    public const int EvasionChance = 15;
    public const int BlockChance = 40;
    public const float ParryModifier = 0.75f;
    public const float BlockModifier = 0.5f;

    public const string Idle = nameof(Idle);
    public const string Attack = nameof(Attack);
    public const string Block = nameof(Block);
    public const string Death = nameof(Death);

    public enum AttackStatus
    {
        Fail,
        Hit,
        CriticalHit
    }

    public enum Animations
    {
        Idle,
        Attack,
        Block,
        Death,
        Evade
    }

    public enum DefenceStatus
    {
        Evade,
        Parry,
        Block,
        FullDamage,
        Dead
    }
}
