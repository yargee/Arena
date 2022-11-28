using System;
using UnityEngine;

[Serializable]
public class PlayerData
{
    [SerializeField] private string _name;
    [SerializeField] private Fighter _view;

    public string Name => _name;
    public Fighter View => _view;

    public PlayerData(string name, Fighter view)
    {
        _name = name;
        _view = view;
    }

    public PlayerData() { }

    public void Save()
    {
        var data = new PlayerData(_name, _view);
        var dataToJson = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("Player", dataToJson);
    }

    public PlayerData Load()
    {
        var json = PlayerPrefs.GetString("Player");

        return JsonUtility.FromJson<PlayerData>(json);
    }
}
