using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region 싱글톤
    private static UIManager instance = null;

    public static UIManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<UIManager>();
                if (instance == null) {
                    Debug.LogError("There's no active UIManager Object");
                }
            }
            return instance;
        }
    }
    #endregion

    public HealthBar healthBar;
}
