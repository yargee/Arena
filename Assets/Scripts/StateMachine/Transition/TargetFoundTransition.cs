using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFoundTransition : Transition
{
    private void Update()
    {
        if (Fighter.Target != null)
        {
            NeedTransit = true;
        }
    }
}
