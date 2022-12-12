using BehaviorDesigner.Runtime.Tasks;

public class TargetUnavailable : Conditional
{
    public SharedFighter Fighter;

    public override TaskStatus OnUpdate()
    {
        if (Fighter.Value.Target == null || !Fighter.Value.Target.IsActive)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
