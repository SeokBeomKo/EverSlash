using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassData : MonoBehaviour
{
    [Header("직업 리스트")]
    public ClassDataList classDataList;

    [Header("직업 내용물")]
    public string className;
    public int classObject;

    [Header("직업 모습")]
    public GameObject classSee;

    [Header("선택한 직업 저장소")]
    public ClassComplete classComplete;

    public void Select(){
        for (int i = 0; i < classDataList.classData.Length; i++){
            classDataList.classData[i].gameObject.SetActive(false);
        }
        gameObject.SetActive(true);
        classComplete.className = className;
        classComplete.classObject = classObject;
    }
    
}
