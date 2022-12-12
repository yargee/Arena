using System;
using UnityEngine;

[Serializable]
public class Characteristics : MonoBehaviour
{
    [SerializeField] [Range(6, 20)] private int _strength; //add damage + armor reduction + parry chance + block chance
    [SerializeField] [Range(6, 20)] private int _dexterity;//increase speed + evade chance + hit chance
    [SerializeField] [Range(6, 20)] private int _constitution; //increase pain resistance(less attack), hp regen

    public int StrenghModifier => CalculateModifier(_strength);
    public int DexterityModifier => CalculateModifier(_dexterity);
    public int ConstitutionModifier => CalculateModifier(_constitution);

    private void OnEnable()
    {
       Randomize();
    }

    private int CalculateModifier(int value)
    {
        return Mathf.CeilToInt((value - 10) / 2);
    }

    private void Randomize()
    {
        _strength = UnityEngine.Random.Range(6, 21);
        _dexterity = UnityEngine.Random.Range(6, 21);
        _constitution = UnityEngine.Random.Range(6, 21);
    }
}
