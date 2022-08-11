using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    EnemyData data;

    SerializedProperty enemyInfo; SerializedProperty normalInfo; SerializedProperty dashInfo;
    SerializedProperty bombInfo; SerializedProperty smashInfo;
    
    public void OnEnable() {
        {
            data = (EnemyData)target;
            enemyInfo = serializedObject.FindProperty("enemyInfo");
            normalInfo = serializedObject.FindProperty("normalInfo");
            dashInfo = serializedObject.FindProperty("dashInfo");
            bombInfo = serializedObject.FindProperty("bombInfo");
            smashInfo = serializedObject.FindProperty("smashInfo");
        }
    }

    public override void OnInspectorGUI()
    {
        data.Type = (EnemyType)EditorGUILayout.EnumPopup("타입",data.Type);
        
        switch(data.Type)
        {
            case EnemyType.Normal:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(normalInfo);
                break;
            case EnemyType.Dash:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(dashInfo);
                break;
            case EnemyType.Bomb:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(bombInfo);
                break;
            case EnemyType.Smash:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(smashInfo);
                break;
        }

        serializedObject.ApplyModifiedProperties ();
    }
}
