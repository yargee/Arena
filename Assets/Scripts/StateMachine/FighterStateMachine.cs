using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;

    private State _currentState;
    private Fighter _fighter;

    public State CurrentState => _currentState;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(1);

        _currentState = _firstState;
        _currentState.Enter(_fighter);
    }

    private void Update()
    {
        if (_currentState == null || _currentState.FinishState) return;

        State nextState = _currentState.GetNextState();

        if (nextState != null)
        {
            TransitToNextState(nextState);
        }
    }

    public void Init(Fighter fighter)
    {
        _fighter = fighter;
    }

    private void TransitToNextState(State nextState)
    {
        _currentState.Exit();
        _currentState = nextState;
        _currentState.Enter(_fighter);
    }

    public void Stop()
    {
        _currentState.Exit();
        _currentState = null;
    }
}
