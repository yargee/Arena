using UnityEngine;

public class CombatCalculator
{
    public static bool IsAttackSuccessfull(Weapon weapon, int dexterity)
    {
        return Random.Range(1, 101) + dexterity > weapon.MissChance;
    }

    public static bool IsCriticalStrike(Weapon weapon, int dexterity)
    {
        return Random.Range(1, 101) + dexterity <= weapon.CriticalChance;
    }

    public static int CalculateBaseDamage(Weapon weapon, int strength)
    {
        int baseDamage = Random.Range(weapon.MinDamage, weapon.MaxDamage + 1);

        return weapon.TwoHanded ? Mathf.RoundToInt((baseDamage + strength) * ConstantKeys.TwoHandedWeaponDamageModifier) : baseDamage + strength;
    }

    public static int CalculateCriticalDamage(Weapon weapon, int damage)
    {
        return Mathf.RoundToInt(damage * weapon.CriticalModifier);
    }

    public static bool IsAttackEvaded(int dexterity)
    {
        return Random.Range(1, 101) <= dexterity + ConstantKeys.EvasionChance;
    }

    public static bool IsAttackParried(Weapon weapon, int strength)
    {
        return Random.Range(1, 101) <= weapon.ParryChance + strength;
    }

    public static bool IsAttackBlocked(int strength)
    {
        return Random.Range(1, 101) <= strength + ConstantKeys.BlockChance;
    }

    public static int CalculateParriedAttackDamage(int damage)
    {
        return Mathf.RoundToInt(damage * ConstantKeys.ParryModifier);
    }

    public static int CalculateBlockedAttackDamage(int damage)
    {
        return Mathf.RoundToInt(damage * ConstantKeys.BlockModifier);
    }
}
