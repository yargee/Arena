using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CombatZone : MonoBehaviour
{
    private Fighter _enemy;

    public event UnityAction<Fighter> TargetFound;
    public event UnityAction TargetLost;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Fighter enemy))
        {
            _enemy = enemy;
            TargetFound?.Invoke(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Fighter enemy) && _enemy == enemy)
        {
            TargetLost?.Invoke();
        }
    }
}
