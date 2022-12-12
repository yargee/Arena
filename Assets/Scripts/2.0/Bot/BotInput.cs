using UnityEngine;

public class BotInput : MonoBehaviour, IMovementInputSource
{
    public Vector2 MovementInput { get; set; }

    /*
    private void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        MovementInput.Normalize();
    }*/
}
