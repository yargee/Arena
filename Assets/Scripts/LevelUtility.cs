using UnityEngine;

public class LevelUtility : MonoBehaviour
{
    [SerializeField][Range(1,10)] private int _level;

    public int Level => _level;
    /*
    public void LevelUp()
    {
        Debug.Log("LEVEL UP");

        _level++;

        var data = new PlayerData().Load();

        int maxBonusHealth = data.Fighter.Health.Config.GetBonusHealthDelta(_level);
        int health = data.Fighter.Health.MaxHealth + Random.Range(0, maxBonusHealth);

        data.Update(_level, health);
    }*/
}
