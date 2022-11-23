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

public interface IFighter
{
    public IAttackable AttackBehaviour { get; }
    public IDefencable DefenceBehaviour { get; }
}

public interface ICombatable
{
    public Fighter Fighter { get; }
    public void InitFighter(Fighter fighter);
}

public interface IAttackable : ICombatable
{
    public int Damage { get; }
    public bool AttackSuccesfull();
    public void CalculateOutcomingDamage();
    public void SpeedBasedAttack(IDefencable defender);
}

public interface IDefencable : ICombatable
{
    public int IncomingDamage { get; }
    public void StartDefence(int damage);
    public bool DefenceSuccessful();
    public int CalculateIncomingDamage(int outcomingDamage);
    public void ApproveDamage(IDamagable target);
}
