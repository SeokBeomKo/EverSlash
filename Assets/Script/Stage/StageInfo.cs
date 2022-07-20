using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public Transform enterFromPrev;  // 이전 스테이지 에서 들어온 경우
    public Transform enterFromNext;  // 다음 스테이지 에서 들어온 경우
    public GameObject exitToPrev;     // 이전 스테이지 로 가는 곳
    public GameObject exitToNext;     // 다음 스테이지 로 가는 곳

    private bool isClear;           // 해당 스테이지가 클리어 되었는가
}
