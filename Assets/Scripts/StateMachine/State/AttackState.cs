using System.Collections.Generic;

public class AttackState : State
{
    private void OnEnable()
    {
        Fighter.SetAnimation(ConstantKeys.Animations.Idle);
    }

    private void Update()
    {
        Fighter.Attack();
    }
}
