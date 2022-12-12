using UnityEngine;

public abstract class CombatBehaviour : MonoBehaviour
{
    private Armor _armor;
    private Weapon _weapon;
    private Characteristics _characteristics;

    protected Armor Armor => _armor;
    protected Weapon Weapon => _weapon;
    protected Characteristics Characteristics => _characteristics;

    public void Init(Weapon weapon, Characteristics characteristics, Armor armor = null)
    {
        _weapon = weapon;
        _characteristics = characteristics;
        _armor = armor;
    }
}
