using UnityEngine.Events;

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



