using BehaviorDesigner.Runtime.Tasks;

public class AttackTarget : Action
{
    public SharedFighter Fighter;

    public override TaskStatus OnUpdate()
    {
        if(Fighter.Value.CanAttack)
        {
            Fighter.Value.Animator.PlayAnimation(ConstantKeys.Animations.Attack);
            Fighter.Value.Attack();
            
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
        
    }
}
