using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class AnimationBuilder : MonoBehaviour
{
    [SerializeField] private List<AnimationInfo> _data = new List<AnimationInfo>();
    [SerializeField] private List<CustomAnimation> _animations = new List<CustomAnimation>();

    private string _path = "Assets/Characters/";

    public void BuildAnimations()
    {
        foreach(var data in _data)
        {
            Directory.CreateDirectory($"Assets/Scriptable/CustomAnimations/{data.Name.Split('-')[0]}/");

            var newAnimation = ScriptableObject.CreateInstance<CustomAnimation>();
            newAnimation.name = data.Name;

            _animations.Add(newAnimation);

            foreach (var path in data.Pathes)
            {
                UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite));
                if (obj is Sprite sprite)
                {
                    newAnimation.Add(sprite);
                }
            }

            InitAnimation(newAnimation);

            AssetDatabase.CreateAsset(newAnimation, $"Assets/Scriptable/CustomAnimations/{data.Name.Split('-')[0]}/{newAnimation.name}.asset");
            EditorUtility.SetDirty(newAnimation);
        }
    }

    public void GetPathes()
    {
        _data.Clear();
        _animations.Clear();

        var catalogs = Directory.GetDirectories(_path);

        foreach (var hero in catalogs)
        {
            var heroes = Directory.GetDirectories(hero);

            foreach (var character in heroes)
            {
                var animations = Directory.GetDirectories(character);
                {
                    foreach(var anim in animations)
                    {
                        var files = Directory.GetFiles(anim);

                        List<string> sprites = files.Where(x => !x.Contains("meta")).ToList();

                        sprites = sprites.OrderBy(x => Convert.ToInt16(x.Split('\\').Last().Split('.')[0])).ToList();

                        string name = Regex.Replace(anim, _path, string.Empty);

                        name = name.Split('\\')[0] + "_" + name.Split('\\')[1] + "-" +  name.Split('\\')[2].Split('_').Last();

                        _data.Add(new AnimationInfo(name, sprites));
                    }
                }
            }
        }
    }

    private void InitAnimation(CustomAnimation animation)
    {
        if (animation.name.Contains(ConstantKeys.Animations.Attack.ToString().ToLower()))
        {
            animation.Init(ConstantKeys.Animations.Attack);
        }
        else if (animation.name.Contains(ConstantKeys.Animations.Block.ToString().ToLower()))
        {
            animation.Init(ConstantKeys.Animations.Block);
        }
        else if (animation.name.Contains(ConstantKeys.Animations.Die.ToString().ToLower()))
        {
            animation.Init(ConstantKeys.Animations.Die);
        }
        else if (animation.name.Contains(ConstantKeys.Animations.Run.ToString().ToLower()))
        {
            animation.Init(ConstantKeys.Animations.Run);
        }
        if (animation.name.Contains(ConstantKeys.Animations.Idle.ToString().ToLower()))
        {
            animation.Init(ConstantKeys.Animations.Idle);
        }
    }
}


[Serializable]
public class AnimationInfo
{
    public string Name;
    public List<string> Pathes;

    public AnimationInfo(string name, List<string> pathes)
    {
        Name = name;
        Pathes = pathes;
    }
}
