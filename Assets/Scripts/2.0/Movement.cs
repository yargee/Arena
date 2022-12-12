using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _inputSourceBehaviour;

    private IMovementInputSource _inputSource;

    public IMovementInputSource InputSource => _inputSource;


    protected void Awake()
    {
        _inputSource = (IMovementInputSource)_inputSourceBehaviour;
    }

    private void OnValidate()
    {
        if (_inputSourceBehaviour && _inputSourceBehaviour is IMovementInputSource == false)
        {
            Debug.LogError(nameof(_inputSourceBehaviour) + " needs to implement " + nameof(IMovementInputSource));
            _inputSourceBehaviour = null;
        }
    }
}
