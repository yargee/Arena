using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInputSource : MonoBehaviour, IMovementInputSource
{
    public Vector2 MovementInput { get; private set; }

    private void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInput.Normalize();
    }
}
