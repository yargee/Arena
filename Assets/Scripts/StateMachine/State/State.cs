using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    private Fighter _fighter;
    public Fighter Fighter => _fighter;
    public bool FinishState => _transitions.Count == 0;

    public void Enter(Fighter fighter)
    {
        if(!enabled)
        {
            Debug.Log("Enter state  " + fighter);

            _fighter = fighter;
            enabled = true;

            foreach(var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(_fighter);
            }
        }
    }

    public void Exit()
    {
        if(enabled)
        {
            foreach(var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach(var transition in _transitions)
        {
            if(transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
