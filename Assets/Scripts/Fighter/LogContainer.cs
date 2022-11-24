using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogContainer : MonoBehaviour
{
    [SerializeField] private Fighter[] _fighters;
    [SerializeField] private List<Log> _logs = new List<Log>();

    private void Start()
    {
        foreach(var fighter in _fighters)
        {
            fighter.DefenceBehaviour.LogFinished += OnLogFinished;
        }
    }

    private void OnDisable()
    {
        foreach (var fighter in _fighters)
        {
            fighter.DefenceBehaviour.LogFinished -= OnLogFinished;
        }
    }

    private void OnLogFinished(Log log)
    {
        _logs.Add(log);
    }
}
