using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationBuilder))]
public class AnimationBuilderEditor : Editor
{
    private AnimationBuilder _builder;

    private void OnEnable()
    {
        _builder = (AnimationBuilder)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_data"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_animations"));

        GUILayout.Space(15);

        if (GUILayout.Button("Set pathes", GUILayout.Height(35)))
        {
            _builder.GetPathes();
        }

        if (GUILayout.Button("Build", GUILayout.Height(35)))
        {
            _builder.BuildAnimations();
        }

        GUILayout.Space(15);
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
