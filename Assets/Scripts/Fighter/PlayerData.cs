using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private string _name;
    [SerializeField] private Fighter _fighter;
    [SerializeField] private int _level;
    [SerializeField] private int _bonusHealth;

    public string Name => _name;
    public Fighter Fighter => _fighter;
    public int Health => _bonusHealth;

    public PlayerData(string name, Fighter fighter)
    {
        _name = name;
        _fighter = fighter;
        _level = 0;
        _bonusHealth = 0;
    }

    public PlayerData() { }

    public void Save()
    {
        var dataToJson = JsonUtility.ToJson(this);

        PlayerPrefs.SetString("Player_Data", dataToJson);
    }

    public void SaveView()
    {
        var dataToJson = JsonUtility.ToJson(this);

        PlayerPrefs.SetString("Player_View", dataToJson);
    }

    public void Update(int level, int health)
    {
        _level = level;
        _bonusHealth += health;

        Save();
    }

    public PlayerData Load()
    {
        var json = PlayerPrefs.GetString("Player_Data");

        return JsonUtility.FromJson<PlayerData>(json);
    }
}
