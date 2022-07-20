using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    #region 싱글톤
    public static SceneLoadManager instance;

    public static SceneLoadManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<SceneLoadManager>();
                if (instance == null) {
                    Debug.LogError("There's no active SceneLoadManager Object");
                }
            }
            return instance;
        }
    }
    #endregion

    public string sceneName;
}
