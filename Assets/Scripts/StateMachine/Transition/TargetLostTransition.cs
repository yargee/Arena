using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLostTransition : Transition
{
    private void Update()
    {
        if(Fighter.Target == null)
        {
            NeedTransit = true;
        }
    }
}
