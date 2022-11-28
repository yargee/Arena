using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArenaModeSelector : MonoBehaviour
{
    public void LoadArena(int mode)
    {
        PlayerPrefs.SetInt("ArenaMode", mode);
        SceneLoader.Instance.LoadArena();
    }
}
