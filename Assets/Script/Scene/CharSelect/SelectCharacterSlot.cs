using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class info{

}
public class SelectCharacterSlot : MonoBehaviour
{
    public CharSlot[] charSlots;
    public DoubleCheck doubleCheck;

    private void Start() {
        doubleCheck.code = 2;
        Reset();
    }

    private void Reset(){
        for (int i = 0; i < 9; i++){
            if (File.Exists(DataManager.instance.path + $"{i}")){
                charSlots[i].saveFile = true;
                charSlots[i].haveSlot.SetActive(true);
                charSlots[i].donthaveSlot.SetActive(false);
                charSlots[i].haveSlotSelect.SetActive(false);

                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();

                charSlots[i].infoText[0].text = DataManager.instance.nowPlayer.playerClassName;
                charSlots[i].infoText[1].text = DataManager.instance.nowPlayer.level.ToString();
                charSlots[i].infoText[2].text = DataManager.instance.nowPlayer.ability1;
                charSlots[i].infoText[3].text = DataManager.instance.nowPlayer.ability2;
                charSlots[i].infoText[4].text = DataManager.instance.nowPlayer.ability3;
            }
            else{
                charSlots[i].saveFile = false;
                charSlots[i].haveSlot.SetActive(false);
                charSlots[i].donthaveSlot.SetActive(true);
                charSlots[i].haveSlotSelect.SetActive(false);
            }
        }
        DataManager.instance.DataClear();
        
    }

    public void CreatButtonClick(int number){
        // 데이터매니저 나우슬롯을 현재 클릭한 슬롯으로 설정
        DataManager.instance.nowSlot = number;

        // 함수 내용 실행
        gameObject.SetActive(false);
        doubleCheck.code = 0;
        doubleCheck.number = number;
        doubleCheck.gameObject.SetActive(true);
    }
    public void PlayButtonClick(int number){
        // 데이터매니저 나우슬롯을 현재 클릭한 슬롯으로 설정
        DataManager.instance.nowSlot = number;

        // 함수 내용 실행
        gameObject.SetActive(false);
        doubleCheck.code = 1;
        doubleCheck.number = number;
        doubleCheck.gameObject.SetActive(true);
    }
    public void DeleteButtonClick(int number){
        // 데이터매니저 나우슬롯을 현재 클릭한 슬롯으로 설정
        DataManager.instance.nowSlot = number;

        // 함수 내용 실행
        gameObject.SetActive(false);
        doubleCheck.code = 2;
        doubleCheck.number = number;
        doubleCheck.gameObject.SetActive(true);
    }
    
    public void Creat(){
        SceneManager.LoadScene("02.ClassSelect");
    }
    public void Play(){
        SceneManager.LoadScene("03.GameScene");
    }
    public void Delete(){
        DataManager.instance.DeleteData();
        doubleCheck.gameObject.SetActive(false);
        gameObject.SetActive(true);
        Reset();
    }
}
