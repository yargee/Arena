using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characteristics : MonoBehaviour
{
    [SerializeField] [Range(6, 20)] private int _strength; //add damage + armor reduction + parry chance + block chance
    [SerializeField] [Range(6, 20)] private int _dexterity;//increase speed + evade chance + hit chance
    [SerializeField] [Range(6, 20)] private int _constitution; //increase pain resistance(less attack), hp regen

    public int StrenghModifier => CalculateModifier(_strength);
    public int DexterityModifier => CalculateModifier(_dexterity);
    public int ConstitutionModifier => CalculateModifier(_constitution);

    private int CalculateModifier(int value)
    {
        return Mathf.CeilToInt(Mathf.Abs(value - 10) / 2);
    }
    private void Randomize()
    {
        _strength = Random.Range(6, 21);
        _dexterity = Random.Range(6, 21);
        _constitution = Random.Range(6, 21);
    }
}
