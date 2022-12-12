using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTransition : Transition
{
    private void Update()
    {
        if (Fighter.Target == null || Fighter.Target.Defeated) return;

        if (Vector2.Distance(Fighter.transform.position, Fighter.Target.transform.position) > Fighter.Weapon.AttackRange)
        {
            NeedTransit = true;
        }
    }
}
