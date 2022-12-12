using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToTarget : Action
{
    public SharedFighter Fighter;

    public override TaskStatus OnUpdate()
    {
        if (Fighter.Value.Target != null && Fighter.Value.Target.IsActive && Vector2.Distance(Fighter.Value.transform.position, Fighter.Value.Target.Position) > 3)
        {
            var newPosition = Vector3.MoveTowards(Fighter.Value.transform.position, Fighter.Value.Target.Position, 4 * Time.deltaTime);
            Fighter.Value.Animator.PlayAnimation(ConstantKeys.Animations.Run);

            Fighter.Value.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.y);

            if (Fighter.Value.transform.position.x > Fighter.Value.Target.Position.x)
            {
                Fighter.Value.Animator.transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                Fighter.Value.Animator.transform.localScale = new Vector2(1, 1);
            }

            return TaskStatus.Running;
        }

        return TaskStatus.Failure;
    }
}
