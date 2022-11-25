using UnityEngine;

public class Mover : MonoBehaviour
{
    public void MoveToTarget(Fighter fighter)
    {
        if(fighter.transform.position != fighter.Target.transform.position)
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
