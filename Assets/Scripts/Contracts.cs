using UnityEngine;

public interface IDamagable
{
    public Health Health { get; }
    public void TakeDamage(int damage);
}

public interface IHealable
{
    public Health Health { get; }
    public void GetHeal(int heal);
}

public interface IMovementInputSource
{
    public Vector2 MovementInput { get; }
}

public interface IAttackableTarget
{
    public IAttackableTarget Target { get; }
    public string Name { get; }
    public bool IsActive { get; }
    public Vector2 Position { get; }

    public void TakeAttack(int damage);
}

public interface IWeapon
{
    public int CalculateDamage();
}

