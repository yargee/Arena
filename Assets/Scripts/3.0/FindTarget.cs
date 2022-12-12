using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTarget : Action
{
    public SharedFighter Fighter;
    public SharedTargeter Targeter;

    public override TaskStatus OnUpdate()
    {
        bool result = Targeter.Value.TryFindTarget(Fighter.Value);

        return result ? TaskStatus.Success : TaskStatus.Failure;
    }
}
