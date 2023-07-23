using UnityEngine;

public class Mover : MonoBehaviour
{
    private IAttackableTarget _target;

    public void MoveToTarget(Fighter fighter)
    {
        if(transform.position != _target.Position)
        {
           // fighter.transform.position = fighter.Target.transform.position;
        }
        //TakeDistance(fighter);
    }

    private void TakeDistance(Fighter fighter)
    {
        fighter.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
    }
}
