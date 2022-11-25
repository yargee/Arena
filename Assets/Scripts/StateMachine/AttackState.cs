using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Fighter _fighter;
    public Fighter Fighter => _fighter;

    public void Enter(Fighter fighter)
    {
        _fighter = fighter;
    }

    public void StateLogic()
    {
        Fighter.Attack();
    }
}
