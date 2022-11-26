using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    private Fighter _fighter;

    public State TargetState => _targetState;
    protected Fighter Fighter => _fighter;

    public bool NeedTransit { get; protected set; }

    public virtual void Init(Fighter fighter)
    {
        _fighter = fighter;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}
