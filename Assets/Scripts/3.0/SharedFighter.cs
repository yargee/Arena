using BehaviorDesigner.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedFighter : SharedVariable<Fighter>
{
    public static implicit operator SharedFighter(Fighter value) => new SharedFighter { Value = value };
}
