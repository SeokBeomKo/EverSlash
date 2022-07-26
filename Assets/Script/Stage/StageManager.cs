using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    #region 싱글톤
    private static StageManager instance = null;

    public static StageManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<StageManager>();
                if (instance == null) {
                    Debug.LogError("There's no active StageManager Object");
                }
            }
            return instance;
        }
    }
    #endregion

    GameObject player;

    Transform[] EnemySpawn;
}
