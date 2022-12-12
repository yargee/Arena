using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : Action  //задает координаты по клику мыши в переменную направления
{
    public SharedVector2 Direction;

    public override TaskStatus OnUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Direction.Value = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return TaskStatus.Success;
        }

        return TaskStatus.Failure;
    }
}
