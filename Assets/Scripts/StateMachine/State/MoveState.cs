using UnityEngine;

public class MoveState : State
{
    [SerializeField] private int _speed;

    private void OnEnable()
    {
        Fighter.SetAnimation(ConstantKeys.Animations.Run, true);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var newPosition = Vector3.MoveTowards(Fighter.transform.position, Fighter.Target.transform.position, _speed * Time.deltaTime);

        Fighter.transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.y);

        if(Fighter.transform.position.x > Fighter.Target.transform.position.x)
        {
            Fighter.Animator.transform.localScale = new Vector2(-1, 1);
        }    
        else
        {
            Fighter.Animator.transform.localScale = new Vector2(1, 1);
        }
    }

}
