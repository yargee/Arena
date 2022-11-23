using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New armor", menuName = "Equipment/Armor")]
public class Armor : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _armor;
}
