using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Attack))]
[RequireComponent(typeof(Defence))]
[RequireComponent(typeof(Characteristics))]
public class Fighter : MonoBehaviour, IAttackableTarget
{
    [SerializeField] private string _name;
    [SerializeField] private Armor _armor;
    [SerializeField] private Weapon _weapon;
    
    private Characteristics _characteristics;
    private Attack _attack;
    private Defence _defence;

    public IAttackableTarget Target { get; private set; }
    public string Name => _name;

    public bool IsAlive => throw new NotImplementedException();

    public Vector2 Position { get; private set; }

    private void OnEnable()
    {
        _attack = GetComponent<Attack>();
        _defence = GetComponent<Defence>();
        _characteristics = GetComponent<Characteristics>();

        _attack.Init(_weapon, _characteristics);
        _defence.Init(_weapon, _characteristics, _armor);
    }

    private void Update()
    {
        Position = transform.position;
    }

    public void SetTarget(IAttackableTarget target)
    {
        Target = target;
        _attack.SetTarget(target);
    }

    public void LoseTarget()
    {
        Target = null;
        _attack.LoseTarget();
    }

    public void TakeAttack(int damage)
    {
        _defence.TakeDamage(damage);
    }




    /*
    public void Attack()
    {
       
    }

    public void SetAnimation(ConstantKeys.Animations name, bool loop = false, UnityAction Callback = null)
    {
       // _animator.PlayAnimation(name, Callback, loop);
    }*/
}
