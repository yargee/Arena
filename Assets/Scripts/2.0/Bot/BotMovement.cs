using UnityEngine;

public class BotMovement : Movement
{
    [SerializeField] private int _speed = 3;

    private void Update()
    {
        if (!OnPosition())
            Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, InputSource.MovementInput, Time.deltaTime * _speed);
    }

    private bool OnPosition()
    {
        return Vector2.Distance(transform.position, InputSource.MovementInput) < 0f;
    }
}
