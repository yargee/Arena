using UnityEngine;

public class MoveToTargetTransition : Transition
{
    private void Update()
    {
        if (Vector2.Distance(Fighter.transform.position, Fighter.Target.transform.position) <= Fighter.Weapon.AttackRange)
        {
            NeedTransit = true;
        }
    }
}
