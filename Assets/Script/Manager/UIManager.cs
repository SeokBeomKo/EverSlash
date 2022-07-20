using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 싱글톤
    private static UIManager _instance = null;

    public static UIManager Instance {
        get {
            if(_instance == null) {
                _instance = GameObject.FindObjectOfType<UIManager>();
                if (_instance == null) {
                    Debug.LogError("There's no active UIManager Object");
                }
            }
            return _instance;
        }
    }
    #endregion

    public HealthBar healthBar;
}
