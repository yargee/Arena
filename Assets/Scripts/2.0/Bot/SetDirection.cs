using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDirection : Action  //������ ���������� �� ����� ���� � ���������� �����������
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
