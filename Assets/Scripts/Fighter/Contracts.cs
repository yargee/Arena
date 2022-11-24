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
    public ConstantKeys.CombatStatus CalculateOutcomingDamage();
    public void SpeedBasedAttack(IDefencable defender);
}

public interface IDefencable : ICombatable, ILogReciever
{
    public int IncomingDamage { get; }
    public void StartDefence(int damage);
    public bool DefenceSuccessful();
    public ConstantKeys.CombatStatus CalculateIncomingDamage(int damage);
    public void ApproveDamage(IDamagable target);
}

public interface ILogReciever
{
    public Log Log { get; }
    public event UnityAction<Log> LogFinished;
    public void GetLog(Log log);
}
