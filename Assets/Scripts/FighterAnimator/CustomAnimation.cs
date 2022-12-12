using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New animation", menuName = "Custom Animation/Anim")]
public class CustomAnimation : ScriptableObject
{
    [SerializeField] private ConstantKeys.Animations _name;
    [SerializeField] private List<Sprite> _atlas = new List<Sprite>();
    [SerializeField] [Range(1, 30)] private float _frequency;
    [SerializeField] private bool _loop;

    public ConstantKeys.Animations Name => _name;
    public int Lenght => _atlas.Count;
    public float Frequency => _frequency;
    public bool Loop => _loop;

    public void Init(ConstantKeys.Animations name)
    {
        _name = name;
        _frequency = 15;
    }

    public void Add(Sprite sprite)
    {
        _atlas.Add(sprite);
    }

    public Sprite Get(int index)
    {
        if (index < Lenght)
        {
            return _atlas[index];
        }
        else
        {
            return _atlas[0];
        }
    }

    public void SetLoop(bool value)
    {
        _loop = value;
    }
}
