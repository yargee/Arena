using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics : MonoBehaviour
{
    [SerializeField] private int _strength; //add damage + armor reduction + parry chance + block chance
    [SerializeField] private int _dexterity;//increase speed + evade chance + hit chance
    [SerializeField] private int _constitution; //increase pain resistance(less attack), hp regen

    public int StrenghModifier => CalculateModifier(_strength);
    public int DexterityModifier => CalculateModifier(_dexterity);
    public int ConstitutionModifier => CalculateModifier(_constitution);

    private int CalculateModifier(int value)
    {
        return Mathf.CeilToInt(Mathf.Abs(value - 10) / 2);
    }
}
