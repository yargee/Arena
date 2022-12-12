using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New health config", menuName = "Player/Health")]
public class HealthConfig : ScriptableObject
{
    [SerializeField] private int[] _maxHealthDeltaByLevel = {51, 61, 71, 81, 91, 101, 111, 121, 131, 141, 151 };

    public int GetBonusHealthDelta(int level)
    {
        return _maxHealthDeltaByLevel[level - 2];
    }

    public int LoadHealth()
    {
        var data = new PlayerData().Load();

        return data.Health;
    }
}
