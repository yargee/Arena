using BehaviorDesigner.Runtime.Tasks;

public class CharacterDeactivated : Conditional
{
    public SharedFighter Fighter;
    public override TaskStatus OnUpdate()
    {
        if (Fighter.Value.IsActive)
        {
            return TaskStatus.Failure;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
