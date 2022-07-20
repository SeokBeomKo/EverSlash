using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoubleCheck : MonoBehaviour
{
    [Header("실행 버튼")]
    public GameObject Create;
    public GameObject Play;
    public GameObject Delete;
    public GameObject Exit;

    [Header("실행 코드")]
    public int code;

    [Header("슬롯 번호")]
    public int number;

    [Header("버튼 텍스트")]
    public TextMeshProUGUI action;

    
    [Header("연결 UI")]
    public SelectCharacterSlot selectCharacterSlot;
    private void OnEnable(){
        switch(code){
            case 0:
                action.text = "생성";
                Create.SetActive(true);
                Play.SetActive(false);
                Delete.SetActive(false);
                Exit.SetActive(false);
                break;
            case 1:
                action.text = "시작";
                Create.SetActive(false);
                Play.SetActive(true);
                Delete.SetActive(false);
                Exit.SetActive(false);
                break;
            case 2:
                action.text = "삭제";
                Create.SetActive(false);
                Play.SetActive(false);
                Delete.SetActive(true);
                Exit.SetActive(false);
                break;
            case 3:
                action.text = "종료";
                Create.SetActive(false);
                Play.SetActive(false);
                Delete.SetActive(false);
                Exit.SetActive(true);
                break;
        }
    }

    public void Cancle(){
        gameObject.SetActive(false);
        selectCharacterSlot.gameObject.SetActive(true);
    }
}
