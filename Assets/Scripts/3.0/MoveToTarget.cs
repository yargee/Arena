using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToTarget : Action
{
    public SharedFighter Fighter;

    public override TaskStatus OnUpdate()
    {
        if (Fighter.Value.Target != null && Fighter.Value.Target.IsActive && (Fighter.Value.transform.position - Fighter.Value.Target.Position).sqrMagnitude > 9)
        {
            var newPosition = Vector3.MoveTowards(Fighter.Value.transform.position, Fighter.Value.Target.Position, 4 * Time.deltaTime);
            //UnityEngine.Debug.Log("Start RUN from Move Action");

            if (!Fighter.Value.Animator.Toggler.IsPlaying(ConstantKeys.Animations.Run))
            {
                Fighter.Value.Animator.PlayAnimation(ConstantKeys.Animations.Run, null, true);
            }

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
