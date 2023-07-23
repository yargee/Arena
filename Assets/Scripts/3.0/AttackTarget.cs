using BehaviorDesigner.Runtime.Tasks;
using System.Diagnostics;
using UnityEngine;

public class AttackTarget : Action
{
    public SharedFighter Fighter;

    public override TaskStatus OnUpdate()
    {
        if(Fighter.Value.CanAttack)
        {
            //UnityEngine.Debug.Log("Start attack animation from Attack Action");
                

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
