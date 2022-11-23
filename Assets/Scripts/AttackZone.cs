using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AttackZone : MonoBehaviour
{
    private float _attackSpeed;
    private float _timeAfterAttack = 0;
    private IAttackable _attacker;
    private IDefencable _defender;

    private void Update()
    {
        if(_attacker == null || _defender == null)
        {
            return;
        }

        _timeAfterAttack += Time.deltaTime;

        if(TimeToAttack())
        {
            _attacker.SpeedBasedAttack(_defender);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);

        if(collision.TryGetComponent(out Fighter defender))
        {
            //Debug.Log(defender.name);
            _defender = defender.DefenceBehaviour;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDefencable defender))
        {
            _defender = null;
        }
    }

    public void Init(IAttackable attack, float _speed)
    {
        _attacker = attack;
        _attackSpeed = _speed;
    }

    private bool TimeToAttack()
    {
        if(_timeAfterAttack > _attackSpeed)
        {
            _timeAfterAttack = 0;
            return true;
        }

        return false;
    }
}
