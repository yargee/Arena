using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetMovementInput : Action  //�������� ����������� � ����� ����, � ��� �������� �������� � ����� �����
{
    public SharedBotInput SharedBotInput;
    public SharedVector2 Input;

    public override TaskStatus OnUpdate()
    {
        SharedBotInput.Value.MovementInput = Input.Value;
        return TaskStatus.Success;
    }
}