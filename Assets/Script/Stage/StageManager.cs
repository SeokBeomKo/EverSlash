using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    #region 싱글톤
    private static StageManager _instance = null;

    public static StageManager instance {
        get {
            if(_instance == null) {
                _instance = GameObject.FindObjectOfType<StageManager>();
                if (_instance == null) {
                    Debug.LogError("There's no active StageManager Object");
                }
            }
            return _instance;
        }
    }
    #endregion
    [Header("로비")]
    public StageInfo lobbyStage;

    [Header("스테이지")]
    public StageInfo[] stageInfos;

    public GameObject Player;

    private int currentStage = 0;   // 이동할 스테이지의 배열번호
    private StageInfo curStageInfo; // 플레이어가 있는 스테이지

    private ArrayList list = new ArrayList();       // 랜덤으로 넣을 변수 임시 저장
    private List<int> randList = new List<int>();   // 랜덤순서로 변수 저장 (사용)

    private void Awake() {
        // 스테이지 초기화
        currentStage = 0;

        // 스테이지의 갯수 만큼
        for(int i = 0; i < stageInfos.Length; i++ ){
            list.Add(i);
        }
        System.Random random = new System.Random();
        // 랜덤 인덱스를 집어 넣고 해당 인덱스를 리스트에서 삭제함
        while(0 < list.Count){
            int index = random.Next(list.Count);
            randList.Add((int)list[index]);
            list.RemoveAt(index);
        }
    }

    public void GetPlayer(){
        // 플레이어 찾기
        // Player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Rigidbody>().gameObject;
        // 플레이어 로비에서 시작
        Player.transform.position = lobbyStage.enterFromPrev.position;
    }

    public void StartStage(){
        // 로비 스테이지에서 1번째 스테이지로 이동
        Player.transform.position = stageInfos[randList[currentStage]].enterFromPrev.position;
        
        //Debug.Log(stageInfos[(int)randList[0]].enterFromPrev.position);

        // 현재 스테이지 정보 갱신
        curStageInfo = stageInfos[randList[0]];
    }

    public void NextStage(){
        // 예외처리
        // 1. 마지막 스테이지(보스) 일 경우
        // 다음 스테이지로 이동
        currentStage++;
        Debug.Log("현재 스테이지 번호 : " + currentStage);
        Debug.Log("가야하는 스테이지 : " + randList[currentStage]);

        Player.transform.position = stageInfos[randList[currentStage]].enterFromPrev.position;

        // 현재 스테이지 정보 갱신
        curStageInfo = stageInfos[randList[currentStage]];
    }

    public void PrevStage(){
        // 예외처리
        // 1. 시작 스테이지로 돌아가는 경우 로비 스테이지로 이동
        if (currentStage == 0){
            Player.transform.position = lobbyStage.enterFromNext.position;
            return;
        }

        // 이전 스테이지로 이동
        currentStage--;
        Player.transform.position = stageInfos[randList[currentStage]].enterFromNext.position;

        // 현재 스테이지 정보 갱신
        curStageInfo = stageInfos[randList[currentStage]];
    }
}
