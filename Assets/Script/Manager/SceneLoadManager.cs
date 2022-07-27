using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager instance;
    private void Awake()
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion
    }

    public string sceneName;
}
