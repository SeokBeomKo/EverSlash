using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDataList : MonoBehaviour
{
    public ClassData[] classData;

    private void Start() {
        classData = GetComponentsInChildren<ClassData>();
        for (int i = 0; i < classData.Length;i++){
            classData[i].gameObject.SetActive(false);
        }
    }
}
