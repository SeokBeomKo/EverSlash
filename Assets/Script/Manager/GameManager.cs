using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject player;

    public CinemachineVirtualCamera cam;
    public bool isAlive = true;

    private void Awake()
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion
    }

    private void Start() 
    {
        isAlive = true;
    }

    public void Init()
    {
        cam.Follow = player.transform;
        cam.LookAt = player.transform;
    }
}
