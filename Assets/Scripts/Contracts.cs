using System.Collections.Generic;

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


