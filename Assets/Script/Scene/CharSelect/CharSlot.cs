using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharSlot : MonoBehaviour
{
    [Header("슬롯 번호")]
    public int count;

    [Header("슬롯의 유무")]
    public bool saveFile;
    public GameObject haveSlot;
    public GameObject donthaveSlot;
    public GameObject haveSlotSelect;

    [Header("슬롯 내 정보")]
    public TextMeshProUGUI[] infoText;


    public void trueClick(){
        haveSlot.SetActive(false);
        donthaveSlot.SetActive(false);
        haveSlotSelect.SetActive(true);
    }

    public void ExitSelect(){
        haveSlot.SetActive(true);
        donthaveSlot.SetActive(false);
        haveSlotSelect.SetActive(false);
    }

       public void DeleteSelect(){
        haveSlot.SetActive(false);
        donthaveSlot.SetActive(true);
        haveSlotSelect.SetActive(false);
    }
    
}
