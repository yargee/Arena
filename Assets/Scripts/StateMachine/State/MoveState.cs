using UnityEngine;

public class MoveState : State
{
    [SerializeField] private int _speed;

    private void OnEnable()
    {
        Fighter.SetAnimation(ConstantKeys.Animations.Move);
    }

    private void Update()
    {
        Fighter.transform.position =  Vector3.MoveTowards(Fighter.transform.position, Fighter.Target.transform.position, _speed * Time.deltaTime);
    }
}
