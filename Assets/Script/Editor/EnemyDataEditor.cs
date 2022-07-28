using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyData))]
public class EnemyDataEditor : Editor
{
    EnemyData data;

    SerializedProperty enemyInfo; SerializedProperty normalInfo; SerializedProperty dashInfo;
    SerializedProperty bombInfo; SerializedProperty smashInfo; SerializedProperty areaInfo;
    SerializedProperty shootInfo;
    
    public void OnEnable() {
        {
            data = (EnemyData)target;
            enemyInfo = serializedObject.FindProperty("enemyInfo");
            normalInfo = serializedObject.FindProperty("normalInfo");
            dashInfo = serializedObject.FindProperty("dashInfo");
            bombInfo = serializedObject.FindProperty("bombInfo");
            smashInfo = serializedObject.FindProperty("smashInfo");
            areaInfo = serializedObject.FindProperty("areaInfo");
            shootInfo = serializedObject.FindProperty("shootInfo");
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
            case EnemyType.Area:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(areaInfo);
                break;
            case EnemyType.Shoot:
                EditorGUILayout.PropertyField(enemyInfo);
                EditorGUILayout.PropertyField(shootInfo);
                break;
        }

        EditorUtility.SetDirty(data);
    }
}
