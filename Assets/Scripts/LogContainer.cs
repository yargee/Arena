using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogContainer : MonoBehaviour
{
    [SerializeField] private List<Log> _logs = new List<Log>();

    public void AddLog(Log log)
    {
        _logs.Add(log);
    }
}
