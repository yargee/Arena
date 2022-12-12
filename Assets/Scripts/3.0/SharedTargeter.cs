using BehaviorDesigner.Runtime;

public class SharedTargeter : SharedVariable<Targeter>
{
    public static implicit operator SharedTargeter(Targeter value) => new SharedTargeter { Value = value };
}
